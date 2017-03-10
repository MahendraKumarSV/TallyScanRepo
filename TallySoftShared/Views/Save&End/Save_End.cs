using System;
using Xamarin.Forms;
using TallySoftShared.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
#if __IOS__
using TallySoftShared.iOS;
#endif

#if __ANDROID__
using Android;
using Android.OS;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using TallySoftShared.Droid;
#endif

namespace TallySoftShared
{
	/// <summary>
	/// save_end page
	/// </summary>
	public partial class Save_End : ContentPage
	{
		public Save_End()
		{
			Title = "Save & End";
			NavigationPage.SetBackButtonTitle(this, "");

			var configuration = new ToolbarItem
			{
				Icon = "settings.png",
			};

			ToolbarItems.Add(configuration);

			saveDataToFile();
		}

		/// <summary>
		/// save data to file and return to main page
		/// </summary>
		protected async void saveDataToFile()
		{
			List<StoredFileName> currentStoredFile = App.DatabaseRepo.GetCurrentStoredFileName();

			if (currentStoredFile.Count > 0)
			{
				App.fileName = currentStoredFile[0].name;
				App.currentFileID = currentStoredFile[0].fileid;
			}

			else if (currentStoredFile.Count == 0)
			{
				App.currentFileID = 0;
				App.fileName = "";
			}

			if (String.IsNullOrEmpty(App.fileName) && App.currentFileID == 0)
			{
				App.dataModifiedAndSaveAndEnd = false;
				await App.Current.MainPage.DisplayAlert("Error", "No File available", "OK");
				moveToRootPage();
			}

			else
			{
#if __ANDROID__
					App.checkStoragePermission();

					if (App.storageAccess == true)
					{
						await saveAndEnd();
					}

					else
					{
						await App.Current.MainPage.DisplayAlert("Access Denied", "App needs storage permission", "OK");
					}
#endif

#if __IOS__
				await saveAndEnd();
#endif

#if WINDOWS_UWP
                	await saveAndEnd();
#endif
			}
		}

		/// <summary>
		/// saveandend method 
		/// </summary>
		/// <returns></returns>
		protected async Task saveAndEnd()
		{
			List<ScanDataTable> scanRecordsList = App.DatabaseRepo.GetAllScanDataRecords(App.fileName);

			if (scanRecordsList.Count > 0)
			{
				App.GetFilePath();

				var writer = new SchemaWriter(App.filePath);
				writer.Write();

				App.dataModifiedAndSaved = false;
				App.dataModifiedAndSaveAndEnd = true;
				App.DatabaseRepo.DeleteSotredFileNameRecord(App.fileName);
				await App.Current.MainPage.DisplayAlert("Success", "Data is saved to a file", "OK");
				moveToRootPage();
			}
			else {
				App.dataModifiedAndSaveAndEnd = false;
				await App.Current.MainPage.DisplayAlert("Error", "No Data to save", "OK");
				moveToRootPage();
			}
		}


		/// <summary>
		/// moveto rootpage
		/// </summary>
		public void moveToRootPage()
		{
			if (Device.OS == TargetPlatform.iOS)
			{
				Navigation.PushAsync(new MainMenu(), false);
			}
			else
			{
				Navigation.PushAsync(new MainMenu());
			}

			Navigation.RemovePage(this);
		}
	}
}