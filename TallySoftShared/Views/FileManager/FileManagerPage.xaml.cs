using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TallySoftShared.Model;
using Xamarin.Forms;
using System.IO.Compression;
#if __IOS__
using TallySoftShared.iOS;
#endif

#if __ANDROID__
using Android.OS;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using TallySoftShared.Droid;
#endif

namespace TallySoftShared
{
	public partial class FileManagerPage : ContentPage
	{
		public string filenameToAttach;

		public static List<int> checkedObjects = new List<int>();
		public static List<string> checkedFileNames = new List<string>();
		public static LoaderModel bindingcontextobj = new LoaderModel();
		public static List<ListViewModel> selectedFiles = new List<ListViewModel>();
		public static ObservableCollection<ListViewModel> allFileNames = new ObservableCollection<ListViewModel>();
		public static ListViewModel listModel = new ListViewModel();
		private static FileManagerPage fileManagerPage = null;

		public ListView ListView { get; set; }
		#if __MOBILE__
			ToolbarItem editBtn;
			ToolbarItem addNewFileBtn;
		#endif

		/// <summary>
		/// Creating single instance
		/// </summary>

		public static FileManagerPage getfileManagerPageInstance()
		{
			if (fileManagerPage == null)
			{
				return new FileManagerPage();
			}

			else
				return fileManagerPage;
		}

		public FileManagerPage()
		{
			InitializeComponent();

			this.BindingContext = bindingcontextobj;

			/// <summary>
			/// set back button title to nil and set page background color to white
			/// </summary>

			NavigationPage.SetBackButtonTitle(this, "");
            if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }

            this.BackgroundColor = Color.White;

			/// <summary>
			/// initializing loader
			/// </summary>

			this.BindingContext = bindingcontextobj;

			App.currentlyIsEditing = true; // used for disabling and enabling swiping feature for list
			bottomStackLayout.IsVisible = false; // Initially hide the bottom stack layout

			/// <summary>
			/// Initialize list view and set tapped event
			/// </summary>

			ListView = listView;
            if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
            {
                ListView.ItemTemplate = new DataTemplate(typeof(CustomCell));
            }

            ListView.ItemTapped += ListView_ItemTapped;

			/// <summary>
			/// set button actions for email and delete buttons
			/// </summary>

			EmailBtn.Clicked += EmailBtn_Clicked;
			DeleteBtn.Clicked += DeleteBtn_Clicked;

			/// <summary>
			/// Handling UI changes for respective platforms
			/// </summary>

			ControlSetting();

			/// <summary>
			/// Observing the recieved notifications/delegates
			/// </summary>

			MessagingCenter.Subscribe<CustomCell>(this, "removeEditButton", (sender) =>
			{
				ToolbarItems.Remove(editBtn);
				reloadList();
				MessagingCenter.Unsubscribe<CustomCell>(this, "removeEditButton");
			});

			MessagingCenter.Subscribe<CustomCell, List<string>>(this, "showAlert", (sender, arrayObj) =>
			{
				DisplayAlert(arrayObj[0], arrayObj[1], "OK");
				MessagingCenter.Unsubscribe<CustomCell>(this, "showAlert");
			});

			MessagingCenter.Subscribe<CustomCell, List<string>>(this, "showAlertWithTwoOptions", (sender, arrayObj) =>
			{
				showCustomAlert(arrayObj);
				MessagingCenter.Unsubscribe<CustomCell>(this, "showAlertWithTwoOptions");
			});
		}

		protected async void showCustomAlert(List<string> errorArgsList)
		{
			var answer = await DisplayAlert(errorArgsList[0], errorArgsList[1], "Yes", "No");
			Console.WriteLine(answer);

			if (answer == true)
			{
				App.contextActionDelete = true;
				MessagingCenter.Send<FileManagerPage>(this, "deleteFile");
			}

			else
			{
				App.contextActionDelete = false;
				MessagingCenter.Unsubscribe<FileManagerPage>(this, "deleteFile");
			}
		}

		public void reloadList()
		{
			App.currentlyIsEditing = true;
			bottomStackLayout.IsVisible = false;

			/// <summary>
			/// Get all the stored/created filenames to show in listview
			/// </summary>

			List<CurrentFileName> fileNames = App.DatabaseRepo.GetCurrentFileName();

			/// <summary>
			/// If filenames table has data
			/// </summary>

			if (fileNames.Count > 0)
			{
				/// <summary>
				/// Before adding the file names to array first clear the array elements
				/// </summary>

				allFileNames.Clear();

				ListView.IsVisible = true;
				NoFilesAvailableLabel.IsVisible = false;

				/// <summary>
				/// Pass text and file ID as per the view model syntax
				/// </summary>

				for(int i = 0; i < fileNames.Count; i++)
				{
					allFileNames.Add(new ListViewModel { Text = fileNames[i].filename, fileID = fileNames[i].id});
				}

				/// <summary>
				/// Assign the file names to listview item source and load the listview Item Template
				/// </summary>

				ListView.ItemsSource = allFileNames;
				ListView.ItemTemplate = new DataTemplate(typeof(CustomCell));

				/// <summary>
				/// Before setting the toolbar items first clear all the items in toolbar
				/// </summary>

				ToolbarItems.Clear();
				editBtn = new ToolbarItem
				{
					Text = "Edit",
					CommandParameter = "Edit"
				};

				editBtn.Clicked += editBtnClicked;

				ToolbarItems.Add(editBtn);

				addNewFileBtn = new ToolbarItem
				{
					Text = "New",
				};

				addNewFileBtn.Clicked += addNewFileBtnClicked;
				ToolbarItems.Add(addNewFileBtn);

#if WINDOWS_UWP
                	editBtn1.CommandParameter = "Edit";
                	editBtn1.Text = "Edit";
                	editBtn1.IsVisible = true;
                	newBtn.IsVisible = true;
#endif
			}

			/// <summary>
			/// If filenames table is empty
			/// </summary>
			else
			{
				ListView.IsVisible = false;
				NoFilesAvailableLabel.IsVisible = true;

				/// <summary>
				/// listview is empty and add only new toolbar button to page
				/// </summary>

				ListView.ItemsSource = null;

				ToolbarItems.Clear();
				addNewFileBtn = new ToolbarItem
				{
					Text = "New",
				};

				addNewFileBtn.Clicked += addNewFileBtnClicked;
				ToolbarItems.Add(addNewFileBtn);
			}
		}

		/// <summary>
		/// calls everytime when page is going to appear
		/// </summary>
		protected override void OnAppearing()
		{
			/// <summary>
			/// refresh the items in listview
			/// </summary>
			reloadList();

			base.OnAppearing();
		}

		/// <summary>
		/// Handle toolbar edit button click
		/// </summary>
		private void editBtnClicked(object sender, EventArgs e)
		{
			if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
			{
				switch (editBtn1.CommandParameter.ToString())
				{
					case "Cancel":
						editBtn1.CommandParameter = "Edit";
						editBtn1.Text = "Edit";
						foreach (var item in allFileNames)
						{
							item.IsHide = false;
							item.IsSelected = false;
						}

						checkedObjects.Clear();
						checkedFileNames.Clear();
						App.currentlyIsEditing = true;
						DeleteAllFilesFromEmailFolder();
						removeZipFile();
						ListView.ItemTemplate = new DataTemplate(typeof(CustomCell));
						bottomStackLayout.IsVisible = false;
						break;

					case "Edit":

						foreach (var item in allFileNames)
						{
							item.IsHide = true;
						}

						checkedObjects.Clear();
						checkedFileNames.Clear();
						App.currentlyIsEditing = false;
						ListView.ItemTemplate = new DataTemplate(typeof(CustomCell));
						editBtn1.CommandParameter = "Cancel";
						editBtn1.Text = "Cancel";
						bottomStackLayout.IsVisible = true;
						disableControls();
						break;
				}
			}

			else
			{
				switch (editBtn.CommandParameter.ToString())
				{
					case "Cancel":
						editBtn.CommandParameter = "Edit";
						editBtn.Text = "Edit";
						foreach (var item in allFileNames)
						{
							item.IsHide = false;
							item.IsSelected = false;
						}

						checkedObjects.Clear();
						checkedFileNames.Clear();
						App.currentlyIsEditing = true;
						DeleteAllFilesFromEmailFolder();
						removeZipFile();
						ListView.ItemTemplate = new DataTemplate(typeof(CustomCell));
						bottomStackLayout.IsVisible = false;
						break;

					case "Edit":

						foreach (var item in allFileNames)
						{
							item.IsHide = true;
						}

						checkedObjects.Clear();
						checkedFileNames.Clear();
						App.currentlyIsEditing = false;
						ListView.ItemTemplate = new DataTemplate(typeof(CustomCell));
						editBtn.CommandParameter = "Cancel";
						editBtn.Text = "Cancel";
						bottomStackLayout.IsVisible = true;
						disableControls();
						break;
				}
			}
		}

		/// <summary>
		/// If user chooses edit button then change the controls accordingly
		/// </summary>
		private void enableControls()
		{
			EmailBtn.IsEnabled = true;
			DeleteBtn.IsEnabled = true;
			EmailBtn.TextColor = Color.White;
			DeleteBtn.TextColor = Color.White;
			bottomStackLayout.BackgroundColor = Color.FromHex("#1966AF");
		}

		/// <summary>
		/// If user chooses cancel button then change the controls accordingly
		/// </summary>
		private void disableControls()
		{
			if (Device.OS == TargetPlatform.Android)
			{
				EmailBtn.IsEnabled = false;
				DeleteBtn.IsEnabled = false;
				bottomStackLayout.BackgroundColor = Color.FromHex("#1966AF");
				EmailBtn.TextColor = Color.Gray;
				DeleteBtn.TextColor = Color.Gray;
			}

			else if (Device.OS == TargetPlatform.iOS)
			{
				EmailBtn.TextColor = Color.Gray;
				DeleteBtn.TextColor = Color.Gray;
				bottomStackLayout.BackgroundColor = Color.FromHex("#1966AF");
			}
		}

		/// <summary>
		/// Handle toolbar item new button click
		/// </summary>
		private void addNewFileBtnClicked(object sender, EventArgs e)
		{
			App.popupPageTitle = "Enter new file name";
			var popUpPage = PopUpPage.getPopUpPageInstance();

			if (Device.OS == TargetPlatform.iOS)
			{
				Navigation.PushAsync(popUpPage, true);
			}

			else if (Device.OS == TargetPlatform.Android)
			{
				Navigation.PushAsync(popUpPage);
			}

			else
			{
				Navigation.PushAsync(popUpPage, true);
			}
		}

		/// <summary>
		/// Handle delete button when user opts for multi selet option
		/// </summary>
		private async void DeleteBtn_Clicked(object sender, EventArgs e)
		{
			if (checkedObjects.Count > 0)
			{
					if (checkedObjects.Count == 1)
					{
						var deleteOption = await DisplayAlert("Delete File?", "Are you sure you want to delete the selected file?", "Yes", "No");

						if (deleteOption == true)
						{
							if (Device.OS == TargetPlatform.Android)
							{
#if __ANDROID__
									App.checkStoragePermission();
#endif

								if (App.storageAccess == true)
								{
									//App.DatabaseRepo.DeleteCurrentFileNameRecord(checkedObjects[0]);
									App.DatabaseRepo.DeleteCurrentFileNameRecord(App.fileName);
									DeleteSingleFileFromMainFolder(checkedFileNames[0]);
									DeleteSingleFileFromEmailFolder(checkedFileNames[0]);
									bottomStackLayout.IsVisible = false;
									reloadList();
								}

								else
								{
									await DisplayAlert("Access Denied", "App needs storage permission", "OK");
								}
							}

							else
							{
								//App.DatabaseRepo.DeleteCurrentFileNameRecord(checkedObjects[0]);
								App.DatabaseRepo.DeleteCurrentFileNameRecord(App.fileName);
								DeleteSingleFileFromMainFolder(checkedFileNames[0]);
								DeleteSingleFileFromEmailFolder(checkedFileNames[0]);
								bottomStackLayout.IsVisible = false;
								reloadList();
							}
						}

						else
						{
							/*await App.DatabaseRepo.DeleteCurrentFileNameRecord(checkedObjects[0]);
							DeleteSingleFileFromMainFolder(checkedFileNames[0]);
							DeleteSingleFileFromEmailFolder(checkedFileNames[0]);
							bottomStackLayout.IsVisible = false;
							reloadList();*/

							bottomStackLayout.IsVisible = false;
							reloadList();
						}
					}

					else
					{
						var deleteOption = await DisplayAlert("Delete Files?", "Are you sure you want to delete the selected files?", "Yes", "No");

						if (deleteOption == true)
						{
							if (Device.OS == TargetPlatform.Android)
							{
#if __ANDROID__
									App.checkStoragePermission();
#endif

								if (App.storageAccess == true)
								{
									App.DatabaseRepo.DeleteMultipleFilesAndRecord(checkedObjects.ToArray());
									DeleteFilesFromMainFolder(checkedFileNames);
									DeleteFilesFromEmailFolder(checkedFileNames);
									bottomStackLayout.IsVisible = false;
									reloadList();
								}

								else
								{
									await DisplayAlert("Access Denied", "App needs storage permission", "OK");
								}
							}

							else
							{
								App.DatabaseRepo.DeleteMultipleFilesAndRecord(checkedObjects.ToArray());
								DeleteFilesFromMainFolder(checkedFileNames);
								DeleteFilesFromEmailFolder(checkedFileNames);
								bottomStackLayout.IsVisible = false;
								reloadList();
							}
						}

						else
						{
							bottomStackLayout.IsVisible = false;
							reloadList();
						}
					}
				}
			}

		/// <summary>
		/// Handle email button when user opts for multi selet option
		/// </summary>
		private async void EmailBtn_Clicked(object sender, EventArgs e)
		{
			bindingcontextobj.IsLoading = true;

			if (checkedObjects.Count > 0)
			{
				

                if (checkedObjects.Count == 1)
                {
                    App.currentFileID = checkedObjects[0];
                    SaveDataToFileAndEmail();
                }
                

				

				else
				{
                    List<LocalStrings> localStringsList = App.DatabaseRepo.GetLocalStringRecord();
                    if (localStringsList.Count > 0)
                    {
                        App.configuredEmail = localStringsList[0].emailId;
                    }
#if __IOS__
					var dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

					var foldername = "ScanData";
					removeZipFile();
					ZipToFiles(foldername);
					foreach (string file in Directory.EnumerateFiles(dataFolder, string.Concat("ScanData", ".zip")))
					{
						var EmailTask = MessagingPlugin.EmailMessenger;
						if (EmailTask.CanSendEmail)
						{
							var email = new EmailMessageBuilder()
							.To(App.configuredEmail)
							.Subject("TallyScan File")
							.WithAttachment(file, ".zip")
							.Build();
							EmailTask.SendEmail(email);
						}

						else
						{
							await DisplayAlert("Cannot send mail", "Please configure email in device settings", "OK");
						}
					}
#endif

#if __ANDROID__
						App.checkStoragePermission();

						if (App.storageAccess == true)
						{
							var dataFolder = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
							var foldername = "ScanData";
							removeZipFile();
							ZipToFiles(foldername);
							foreach (string file in Directory.EnumerateFiles(dataFolder, string.Concat("ScanData", ".zip")))
							{
								var EmailTask = MessagingPlugin.EmailMessenger;
								if (EmailTask.CanSendEmail)
								{
									var email = new EmailMessageBuilder()
									.To(App.configuredEmail)
									.Subject("TallyScan File")
									.WithAttachment(file, ".zip")
									.Build();
									EmailTask.SendEmail(email);
								}

								else
								{
									await DisplayAlert("Cannot send mail", "Please configure email in device settings", "OK");

								}
							}
						}

						else
						{
							await DisplayAlert("Access Denied", "App needs storage permission", "OK");
						}
#endif

#if WINDOWS_UWP
                    var foldername = "ScanData";
					 	bindingcontextobj.IsLoading = true;
                        var dataFolder = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFolderAsync("ScanData");
                        RemoveZipFiles();
                        ZipToFiles(foldername, dataFolder);
					var af = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync("ScanData.zip");
                        if (af != null)
                        {
                            var EmailTask = MessagingPlugin.EmailMessenger;
                            if (EmailTask.CanSendEmail)
                            {
                                var email = new EmailMessageBuilder()
                                    .To(App.configuredEmail)
                                    .Subject("TallyScan File")
                                    .WithAttachment(af)
                                    .Build();
                                EmailTask.SendEmail(email);

                            }
                            else
                            {
                                displayAlert("Cannot send mail", "Please configure email in device settings");

                            }
                        }


#endif
                }
            }
		}

			/// <summary>
			/// Zip to selected files
			/// </summary>
			/// <param name="foldername"></param>
			/// <param name="dataFolder"></param>
#if WINDOWS_UWP
        private static void ZipToFiles(string foldername, Windows.Storage.StorageFolder dataFolder)
        {

            string GuidPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, foldername) + ".zip";
            using (FileStream zipToOpen = new FileStream(GuidPath, FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                {
                    String[] files = Directory.GetFiles(dataFolder.Path);
                    foreach (String echFile in files)
                    {
                        ZipArchiveEntry readmeEntry = archive.CreateEntry(Path.GetFileName(echFile));
                        using (BinaryWriter writer = new BinaryWriter(readmeEntry.Open()))
                        {
                            using (Stream source = File.OpenRead(echFile))
                            {
                                byte[] buffer = new byte[4096];
                                int bytesRead;
                                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    writer.Write(buffer, 0, bytesRead);
                                }
                            }
                        }
                    }
                }
            }
        }
#endif
			/// <summary>
			/// remove the zip files
			/// </summary>

		private static void RemoveZipFiles()
		{
			//var zipfolders = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFilesAsync();
			//for (int i = 0; i < zipfolders.Count; i++)
			//{
			//    if (zipfolders[i].Name.Contains(".zip"))
			//    {
			//        await zipfolders[i].DeleteAsync();
			//    }
			//}

#if IOS
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

#if ANDROID
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

#if WINDOWS_UWP
            if (Directory.Exists(Windows.Storage.ApplicationData.Current.LocalFolder.Path))
            {
                string[] zipfile = Directory.GetFiles(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "*.zip");
                for (int i = 0; i < zipfile.Count(); i++)
                {
                    File.Delete(zipfile[i]);
                }
            }
#endif
		}

		/// <summary>
		/// File zipping functionality
		/// </summary>

		private static void ZipToFiles(string foldername)
		{
#if __IOS__
				var dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        		string GuidPath = string.Concat(dataFolder, "/ScanData.zip");
#endif

#if __ANDROID__
				var dataFolder = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
				string GuidPath = string.Concat(dataFolder, "/ScanData.zip");
#endif

			using (FileStream zipToOpen = new FileStream(GuidPath, FileMode.Create))
			{
				using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
				{
					string[] files = Directory.GetFiles(string.Concat(dataFolder, "/ScanData"), "*.tsx");
					foreach (String echFile in files)
					{
						if (File.ReadAllBytes(echFile).Length == 0)
						{

						}

						else
						{
							ZipArchiveEntry readmeEntry = archive.CreateEntry(Path.GetFileName(echFile));
							using (BinaryWriter writer = new BinaryWriter(readmeEntry.Open()))
							{
								using (Stream source = File.Open(echFile, FileMode.Open, FileAccess.Read))
								{
									byte[] buffer = new byte[4096];
									int bytesRead;
									while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
									{
										writer.Write(buffer, 0, bytesRead);
									}
								}
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Remove zip file functionality
		/// </summary>
		private static void removeZipFile()
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
				App.checkStoragePermission();

				if (App.storageAccess == true)
				{
					var dataFolder = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
					if (Directory.Exists(dataFolder))
					{
						var files = Directory.GetFiles(dataFolder, "*.zip");
						for (int i = 0; i < files.Count(); i++)
						{
							File.Delete(files[i]);
						}
					}
				}	
#endif

#if WINDOWS_UWP
            	if (Directory.Exists(Windows.Storage.ApplicationData.Current.LocalFolder.Path))
            	{
                	string[] zipfile = Directory.GetFiles(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "*.zip");
                	for (int i = 0; i < zipfile.Count(); i++)
                	{
                    	File.Delete(zipfile[i]);
                	}
            	}
#endif
        }

		/// <summary>
		/// save the updated file with data to local storage and attach the same in email
		/// </summary>
		async void SaveDataToFileAndEmail()
		{
#if __ANDROID__
				App.checkStoragePermission();

				if (App.storageAccess == true)
				{
					await saveAndEmail();
				}

				else
				{
					await DisplayAlert("Access Denied", "App needs storage permission", "OK");
				}
#endif

#if __IOS__
					await saveAndEmail();
#endif

#if WINDOWS_UWP
                await saveAndEmail();
#endif
		}

		/// <summary>
		/// email with single file attachment
		/// </summary>
		protected async Task saveAndEmail()
		{
			List<ScanDataTable> scanRecordsList = App.DatabaseRepo.GetAllScanDataRecords(App.fileName);

			if (scanRecordsList.Count > 0)
			{
				/// <summary>
				/// from the respective platform get the file storage path
				/// </summary>
				var fileService = DependencyService.Get<ISaveAndLoad>();

				App.GetFilePath();

				var writer = new SchemaWriter(App.filePath);
				writer.Write();

				if (fileService.FileExists(string.Concat(App.fileName, ".tsx")))
				{
#if __IOS__
						var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
					foreach (string file in Directory.EnumerateFiles(path, string.Concat(App.fileName, ".tsx")))
					{
						var EmailTask = MessagingPlugin.EmailMessenger;

						if (EmailTask.CanSendEmail)
						{
							var email = new EmailMessageBuilder()
								.To(App.configuredEmail)
								.Subject("TallyScan File")
								.WithAttachment(file, ".tsx")
								.Build();
							EmailTask.SendEmail(email);
						}

						else
						{
							displayAlert("Cannot send mail", "Please configure email in device settings");
						}
					}
#endif

#if __ANDROID__
						var path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
					foreach (string file in Directory.EnumerateFiles(path, string.Concat(App.fileName, ".tsx")))
					{
						var EmailTask = MessagingPlugin.EmailMessenger;

						if (EmailTask.CanSendEmail)
						{
							var email = new EmailMessageBuilder()
								.To(App.configuredEmail)
								.Subject("TallyScan File")
								.WithAttachment(file, ".tsx")
								.Build();
							EmailTask.SendEmail(email);
						}

						else
						{
							displayAlert("Cannot send mail", "Please configure email in device settings");
						}
					}
#endif

#if WINDOWS_UWP
                    var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                    var file = await localFolder.GetFileAsync(fileName);
                    var EmailTask = MessagingPlugin.EmailMessenger;
                    if (EmailTask.CanSendEmail)
                    {

                        var email = new EmailMessageBuilder()
                            .To(App.configuredEmail)
                            .Subject("TallyScan File")
                            .WithAttachment(file)
                            .Build();
                        EmailTask.SendEmail(email);
                    }
                    else
                    {
                        displayAlert("Cannot send mail", "Please configure email in device settings");

                    }
#endif
				}
			}

			else
			{
				displayAlert("Error", "No Data");
			}
		}

		/// <summary>
		/// Handle the hardware back button for windows and android
		/// </summary>
		protected override bool OnBackButtonPressed()
		{
#if __ANDROID__
				 Navigation.PopToRootAsync(false);            
#endif

#if WINDOWS_UWP
            	Navigation.PopModalAsync(false);
#endif
			return true;
		}

		/// <summary>
		/// Handle back buton action for windows
		/// </summary>
		protected async void BackTap(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		/// <summary>
		/// Calls everytime when the page is about to disappear
		/// </summary>
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			if (Device.OS == TargetPlatform.Android)
			{
				MessagingCenter.Unsubscribe<CustomCell, List<string>>(this, "showAlert");
			}
			DeleteAllFilesFromEmailFolder();
			removeZipFile();
		}

		/// <summary>
		/// show the error alert with corresponding title and message
		/// </summary>
		public void displayAlert(string title, string messsage)
		{
			DisplayAlert(title, messsage, "OK");
		}

		/// <summary>
		/// show the error alert with corresponding title and message when user opts for listivew swipe options
		/// </summary>
		public static void showAlert(string title, string messsage)
		{
			var page = new FileManagerPage();
			page.DisplayAlert(title, messsage, "OK");
		}

		/// <summary>
		/// Handle listview item tapped event
		/// </summary>
		private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if ((sender as ListView).SelectedItem == null)
				return;
			(sender as ListView).SelectedItem = null;

			switch (editBtn.CommandParameter.ToString())
			{
				case "Edit":
					var selectedItem = (ListViewModel)e.Item;
					App.fileName = selectedItem.Text;
					App.currentFileID = selectedItem.fileID;
					App.DatabaseRepo.AddNewFilenameToStoredFileNameTable(App.fileName, App.currentFileID);

					var page = NativeCellPage.getNativeCellPageInstance();

					if (Device.OS == TargetPlatform.iOS)
					{
						await Navigation.PushAsync(page, true);
					}

					else if (Device.OS == TargetPlatform.Android)
					{
						await Navigation.PushAsync(page);
					}

					else if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
					{
						await Navigation.PushModalAsync(page);
					}
					break;
					
				default:
					var item = e.Item as ListViewModel;

					if (item.IsSelected)
					{
						if (Device.OS == TargetPlatform.Android)
						{
#if __ANDROID__
								App.checkStoragePermission();
#endif

							if (App.storageAccess == true)
							{
								item.IsSelected = false;
								var unSelectedItems = (ListViewModel)e.Item;
								checkedObjects.Remove(unSelectedItems.fileID);
								checkedFileNames.Remove(unSelectedItems.Text);
								DeleteSingleFileFromMainFolder(unSelectedItems.Text);
								DeleteSingleFileFromEmailFolder(unSelectedItems.Text);
							}

							else
							{
								await DisplayAlert("Access Denied", "App needs storage permission", "OK");
							}
						}

						else
						{
							item.IsSelected = false;
							var unSelectedItems = (ListViewModel)e.Item;
							checkedObjects.Remove(unSelectedItems.fileID);
							checkedFileNames.Remove(unSelectedItems.Text);
							DeleteSingleFileFromMainFolder(unSelectedItems.Text);
							DeleteSingleFileFromEmailFolder(unSelectedItems.Text);
						}
					}

					else
					{
						item.IsSelected = true;
						var selectedItems = (ListViewModel)e.Item;
						selectedFiles.Clear();
						selectedFiles.Add(item);
						checkedObjects.Add(selectedItems.fileID);
						checkedFileNames.Add(selectedItems.Text);

						if (checkedObjects.Count == 1)
						{
							filenameToAttach = selectedItems.Text;
							App.fileName = filenameToAttach;
						}

						await SaveFile();
					}

					if (checkedObjects.Count == 0)
					{
						disableControls();
					}

					else
					{
						enableControls();
					}

				break;
			}
		}

		/// <summary>
		/// on every selection of listview item save the file to device storage
		/// </summary>
		protected async Task SaveFile()
		{
			List<ScanDataTable> scanDataRecords = App.DatabaseRepo.GetAllScanDataRecords(selectedFiles[0].Text);
										
			List<String> strList = new List<String>();
			for (int i = 0; i < scanDataRecords.Count; i++)
			{
				if (i == 0)
				{
					string textString = string.Format("{0},{1},{2},{3}", scanDataRecords[i].quantity, scanDataRecords[i].sku, scanDataRecords[i].firstTimeScan, scanDataRecords[i].lastTimeScan);
					strList.Add(textString);
				}

				else
				{
					string qunatityValue = String.Concat("\n", scanDataRecords[i].quantity);
					string textString = string.Format("{0},{1},{2},{3}", qunatityValue, scanDataRecords[i].sku, scanDataRecords[i].firstTimeScan, scanDataRecords[i].lastTimeScan);
					strList.Add(textString);
				}
			}

			var fileService = DependencyService.Get<ISaveAndLoad>();
			string fileName = string.Concat(selectedFiles[0].Text, ".tsx");
			string finalText = "";
			selectedFiles[0].FileData = strList;
			if (selectedFiles[0].FileData != null)
			{
				finalText = String.Join(" ", selectedFiles[0].FileData);
			}

			await fileService.SaveFileToFolderAsync(fileName, finalText);
		}

		/// <summary>
		/// delete the single file from device storage from default storage path
		/// </summary>
		protected void DeleteSingleFileFromMainFolder(string filename)
		{
#if __IOS__
				var dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
#endif

#if __ANDROID__
				var dataFolder = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
#endif

#if WINDOWS_UWP
            	var dataFolder = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
#endif
			if (Directory.Exists(dataFolder))
			{
				string file = string.Concat(dataFolder + "/" + filename, ".tsx");
				File.Delete(file);
			}
		}

		/// <summary>
		/// delete the multiple files from device storage from user created folder (for email zipping)
		/// </summary>
		protected void DeleteAllFilesFromEmailFolder()
		{
#if __IOS__
				var dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string path = string.Concat(dataFolder, "/ScanData");
					if (Directory.Exists(path))
					{
						var files=	Directory.GetFiles(path, "*.tsx");
						for (int i = 0; i < files.Count()  ; i++)
						{
							File.Delete(files[i]);
						}
					}
#endif

#if __ANDROID__
				App.checkStoragePermission();
				var dataFolder = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
				string path = string.Concat(dataFolder, "/ScanData");

				if (App.storageAccess == true)
				{
					if (Directory.Exists(path))
					{
						var files = Directory.GetFiles(path, "*.tsx");
						for (int i = 0; i < files.Count(); i++)
						{
							File.Delete(files[i]);
						}
					}
				}
#endif

#if WINDOWS_UWP
            	var dataFolder = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
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
		/// delete the multiple files from device storage from default storage path
		/// </summary>
		protected void DeleteFilesFromMainFolder(List<string> filesObj)
		{
#if __IOS__
				var dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
#endif

#if __ANDROID__
				var dataFolder = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
#endif

#if WINDOWS_UWP
           	 	var dataFolder = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
#endif

			if (Directory.Exists(dataFolder))
			{
				foreach (string filename in filesObj)
				{
					string file = string.Concat(dataFolder + "/" + filename, ".tsx");
					File.Delete(file);
				}
			}
		}

		/// <summary>
		/// delete the single file from device storage from user created path
		/// </summary>
		protected void DeleteSingleFileFromEmailFolder(string filename)
		{
#if __IOS__
				var dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string path = string.Concat(dataFolder, "/ScanData");
#endif

#if __ANDROID__
				var dataFolder = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
				string path = string.Concat(dataFolder, "/ScanData");
#endif

#if WINDOWS_UWP
            	var dataFolder = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
           	 	string path = string.Concat(dataFolder, "/ScanData");
#endif

			if (Directory.Exists(path))
			{
				string file = string.Concat(path + "/" + filename, ".tsx");
				File.Delete(file);
			}
		}

		/// <summary>
		/// delete the multiple files from device storage from user created path
		/// </summary>
		protected void DeleteFilesFromEmailFolder(List<string> filesObj)
		{
#if __IOS__
				var dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string path = string.Concat(dataFolder, "/ScanData");				
#endif

#if __ANDROID__
				var dataFolder = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
				string path = string.Concat(dataFolder, "/ScanData");
#endif

#if WINDOWS_UWP
            	var dataFolder = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            	string path = string.Concat(dataFolder, "/ScanData");
#endif

			if (Directory.Exists(path))
			{
				foreach (string filename in filesObj)
				{
					string file = string.Concat(path + "/" + filename, ".tsx");
					File.Delete(file);
				}
			}
		}

		private void ControlSetting()
		{
			if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
			{
				stLayoutHeader.IsVisible = false;
				Title = "File Manager";
			}

			else
			{
				NoFilesAvailableLabel.IsVisible = false;
				ListViewLayout.Padding = new Thickness(10, 20);
				editBtn1.CommandParameter = "Edit";
				editBtn1.TranslationX = 70;
				newBtn.TranslationX = 70;
				editBtn1.Clicked += editBtnClicked;
				newBtn.Clicked += addNewFileBtnClicked;
			}

			if (Device.OS == TargetPlatform.Android)
			{
				ListViewLayout.Padding = new Thickness(10, 20);

				if (App.ScreenHeight > 600)
				{
					bottomStackLayout.TranslationY = 80;
					bottomStackLayout.HeightRequest = 10;
					listView.HeightRequest = 550;
				}

				else
				{
					bottomStackLayout.TranslationY = 0;
					listView.HeightRequest = 500;
				}
			}

			if (Device.OS == TargetPlatform.iOS)
			{
				ListViewLayout.Padding = new Thickness(10, 20);
				bottomStackLayout.TranslationY = 0;
			}
		}
	}
}