using System;
using Xamarin.Forms;
using SQLite.Net.Interop;
using TallySoftShared.Model;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
#if __IOS__
using UIKit;
using Foundation;
#endif
#if __ANDROID__
using Android.Views.InputMethods;
using Android.App;
using Android.Bluetooth;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Java.Util;
using Android.OS;
#endif

namespace TallySoftShared
{
	public class App : Xamarin.Forms.Application
	{
		/// <summary>
		/// Declaring Notification objects
		/// </summary>
		#if __IOS__
			static NSObject showNotification;
			static NSObject hideNotification;
		#endif

		public static int ScreenWidth;
		public static int ScreenHeight;
		public static ObservableCollection<ScanDataTable> globalScanRecordsCollection;
		public static String fileName;
		public static int currentFileID;
		public static Dictionary<string, string> fileDictionary = new Dictionary<string, string>();
		public static String configuredEmail;
		public static bool scannerOrDeviceCheckBoxBool;
		public static bool qtyCheckboxBool;
		public static bool duplicateBarcodeCheckBoxBool;
		public static bool testBoxBool;
		public static bool showQtyPopup;
		public static string popupPageTitle;
		public static string currentScreen;
		public static bool currentlyIsEditing;
		public static bool dataModifiedAndSaved;
		public static bool dataModifiedAndSaveAndEnd;
		public static bool storagePermission;
		public static bool storageAccess;
		public static bool contextActionDelete;
		public static int fileIDToBeDeleted;
		public static string firstScanTime;
		public static ObservableCollection<List<SliderMenuItem>> data;
		public static ZXing.Mobile.MobileBarcodeScanner scanner;
		public static ZXing.Mobile.MobileBarcodeScanningOptions options;
		public static bool cameraClick;
		public static string filePath;

		static DatabaseRepository database;
		public static DatabaseRepository DatabaseRepo
		{
			get
			{
				database = database ?? new DatabaseRepository();
				return database;
			}
		}

		public App()
		{
			/// <summary>
			/// set database path
			/// </summary> 
			//DatabaseRepo = new DatabaseRepository(sqlitePlatform, dbPath);

			if (Device.OS == TargetPlatform.Android || Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
			{
				//showQtyPopup = true;
			}

			/// <summary>
			/// Set the root page and then navigate
			/// </summary> 
			this.MainPage = new RootPage();
		}

		#if __IOS__
		public static void showCallback (object sender, UIKeyboardEventArgs args)
		{
			showQtyPopup = true;
		}

		public static void ShowSetup ()
		{
			showNotification = UIKeyboard.Notifications.ObserveWillShow (showCallback);
		}

		public static void ShowTeardown ()
		{
			showNotification.Dispose ();
		}

		public static void hideCallback (object sender, UIKeyboardEventArgs args)
		{
			showQtyPopup = false;
		}

		public static void HideSetup ()
		{
			hideNotification = UIKeyboard.Notifications.ObserveWillHide (hideCallback);
		}

		public static void HideTeardown ()
		{
			hideNotification.Dispose ();
		}
		#endif

		/// <summary>
		/// This method will fire when the app is starting
		/// </summary>
		protected override void OnStart ()
		{
			dataModifiedAndSaved = true;

			/// <summary>
			/// Initialize the notification objects to observe keyboard changes
			/// </summary> 
			#if __IOS__
				ShowSetup ();
				HideSetup();
			#endif

			if (Device.OS == TargetPlatform.Android)
			{
				/// <summary>
				/// Prompt storage permission for android devices whose version is 6.0 or later
				/// </summary>

				promptStoragePermission();
			}

			else if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
			{
				/// <summary>
				/// Delete all the files(if any) from email folder and also remove the zip file if exists
				/// </summary>

				DeleteAllFilesFromEmailFolder();
				removeZipFile();
			}
		}

		protected async void promptStoragePermission()
		{
			#if __ANDROID__
			var status = PermissionStatus.Unknown;

			/// <summary>
			/// OS version is marshmallow or later
			/// </summary>

			if ((int)Build.VERSION.SdkInt >= 23)
			{
				/// <summary>
				/// Delete all the files(if any) from email folder and also remove the zip file if exists
				/// </summary>

				if (status != PermissionStatus.Granted)
				{
					status = (await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage))[Permission.Storage];
					if (status.ToString().Equals("Granted"))
					{
						/// <summary>
						/// Storage permission is granted so delete all the files(if any) from email folder and also remove the zip file if exists
						/// </summary>

						DeleteAllFilesFromEmailFolder();
						removeZipFile();
					}

					else if (status.ToString().Equals("Denied"))
					{
						/// <summary>
						/// If permission is denied by user try to show prompt again
						/// </summary>

						promptStoragePermission();
					}
				}
			}

			/// <summary>
			/// OS version is prior to marshmallow
			/// </summary>

			else
			{
				/// <summary>
				/// Delete all the files(if any) from email folder and also remove the zip file if exists
				/// </summary>
				DeleteAllFilesFromEmailFolder();
				removeZipFile();
			}
			#endif
		}

		/// <summary>
		/// Get the file path from storage
		/// </summary>
		public static void GetFilePath()
		{
			#if __ANDROID__
				var folderPath = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;

			var fileService = DependencyService.Get<ISaveAndLoad>();
			string fileNameWithFormat = string.Concat(App.fileName, ".tsx");

			if (fileService.FileExists(string.Concat(App.fileName, ".tsx")) == true)
			{

				foreach (string file in Directory.EnumerateFiles(folderPath, string.Concat(App.fileName, ".tsx")))
				{
					int index = file.LastIndexOf('/');
					if (file.Substring(index + 1) != null || file.Substring(index + 1) != String.Empty || file.Substring(index + 1).Trim().Length != 0)
					{
						File.Delete(file.Substring(index + 1));
					}
				}
			}

			filePath = Path.Combine(folderPath, fileNameWithFormat);
			#endif

			#if __IOS__
			var	folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var fileService = DependencyService.Get<ISaveAndLoad>();
			string fileNameWithFormat = string.Concat(App.fileName, ".tsx");

			if (fileService.FileExists(string.Concat(App.fileName, ".tsx")) == true)
			{
				foreach (string file in Directory.EnumerateFiles(folderPath, string.Concat(App.fileName, ".tsx")))
				{
					int index = file.LastIndexOf('/');
					if (file.Substring(index + 1) != null || file.Substring(index + 1) != String.Empty || file.Substring(index + 1).Trim().Length != 0)
					{
						File.Delete(file.Substring(index + 1));
					}
				}
			}

			filePath = Path.Combine(folderPath, fileNameWithFormat);
			#endif

			#if WINDOWS_UWP
			 var af = Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(string.Concat(App.fileName, ".tsx")).AsTask().GetAwaiter().GetResult();
                var dotindex = af.Name.IndexOf(".");
                var name = af.Name;
                var extension = name.Substring(dotindex, af.Name.Length - dotindex);
                if (extension == ".tsx")
                {
                    await af.DeleteAsync(Windows.Storage.StorageDeleteOption.PermanentDelete);
                }
            filePath= Path.Combine(Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path), filename);
			#endif


		}

		/// <summary>
		/// Check for storage permission for android devices whose version is 6.0 or later and set bool value accordingly
		/// </summary>
		#if __ANDROID__
		public static async Task checkStoragePermission()
			{
				var status = PermissionStatus.Unknown;
				if ((int)Build.VERSION.SdkInt >= 23)
				{
					if (status != PermissionStatus.Granted)
					{
						status = (await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage));
						if (status.ToString().Equals("Granted"))
						{
							storageAccess = true;
						}

						else if (status.ToString().Equals("Denied"))
						{
							storageAccess = false;
						}
					}
				}

				else {
					storageAccess = true;
				}
			}
			#endif

		/// <summary>
		/// Deletes the existing .tsx files from respective folders from respective devices
		/// </summary>
		protected void DeleteAllFilesFromEmailFolder()
		{
			#if __IOS__
				var dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string path = string.Concat(dataFolder, "/ScanData");

			     if (Directory.Exists(path))
				 {
					var files = Directory.GetFiles(path, "*.tsx");
					for (int i = 0; i < files.Count(); i++)
					{
						File.Delete(files[i]);
					}
				}
			#endif

			#if __ANDROID__

				var dataFolder = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
				string path = string.Concat(dataFolder, "/ScanData");
				if (Directory.Exists(path))
				{
					var files = Directory.GetFiles(path, "*.tsx");
					for (int i = 0; i < files.Count(); i++)
					{
						File.Delete(files[i]);
					}
				}
			#endif
		}

		/// <summary>
		/// Deletes the existing zip file from respective folders from respective devices
		/// </summary>
		protected void removeZipFile()
		{
			#if __IOS__
			var dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			if (Directory.Exists(dataFolder))
						{
							var files = Directory.GetFiles(dataFolder, "*.zip");
							for (int i = 0; i < files.Count(); i++)
							{
								File.Delete(files[i]);
							}
						}
			#endif

			#if __ANDROID__

			var dataFolder = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
			if (Directory.Exists(dataFolder))
			{
				var files = Directory.GetFiles(dataFolder, "*.zip");
				for (int i = 0; i < files.Count(); i++)
				{
					File.Delete(files[i]);
				}
			}
			#endif
		}

		/// <summary>
		/// Calls when app is went to background or suspended state
		/// </summary>
		protected override void OnSleep ()
		{
			//dataModifiedAndSaved = false;

			/// <summary>
			/// Hide keyboard and dispose the notification objects 
			/// </summary>
			#if __IOS__
				DismissKeyboardAsync();
				ShowTeardown();
				HideTeardown();
			#endif

			/// <summary>
			/// Hide keyboard
			/// </summary>
			#if __ANDROID__
			HideKeyboard();
			#endif
		}

		/// <summary>
		/// Calls when app is came from background to foreground state
		/// </summary> 
		protected override void OnResume ()
		{
			/// <summary>
			/// Initialize the notification objects to observe keyboard changes
			/// </summary> 
			#if __IOS__
				if (currentScreen != null && currentScreen.Equals("ScanCycleCount"))
				{
					NativeCellPage.barcodeField.Focus();
				}
				ShowSetup();
				HideSetup();
			#endif

			#if __ANDROID__
				if (currentScreen != null && currentScreen.Equals("ScanCycleCount"))
				{
					NativeCellPage.barcodeField.Focus();
				}
			#endif
		}

		/// <summary>
		/// Hide keyboard for iOS Devices
		/// </summary> 
		#if __IOS__
			public Task<bool> DismissKeyboardAsync() {
				TaskCompletionSource<bool> result = new TaskCompletionSource<bool>();
				try {
					bool dismissalResult = UIApplication.SharedApplication.KeyWindow.EndEditing(true);
					result.SetResult(dismissalResult);
				} catch(Exception exception) {
					Console.WriteLine("\nIn NativeHelper_iOS.DismissKeyboardAsync() - Exception:\n{0}\n", exception);
					result.SetResult(false);
				}

				return result.Task;
			}
		#endif

		/// <summary>
		/// Hide keyboard for Android Devices
		/// </summary> 
		#if __ANDROID__
			public static void HideKeyboard()
			{
				var context = Forms.Context;
				var inputMethodManager = (InputMethodManager)Forms.Context.GetSystemService(Android.Content.Context.InputMethodService);
				if (inputMethodManager != null && context is Activity)
				{
					var activity = context as Activity;
					var token = activity.CurrentFocus?.WindowToken;
					inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);
					activity.Window.DecorView.ClearFocus();
				}
			}			
		#endif
	}
}