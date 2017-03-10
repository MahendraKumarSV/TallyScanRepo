using System;
using System.Collections.Generic;
using Xamarin.Forms;
using TallySoftShared.Model;
using Plugin.Messaging;
using System.IO;
using System.Threading.Tasks;
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
	/// save and email page
	/// </summary>
	public partial class Save_Email : ContentPage
	{
		public Save_Email()
		{
			Title = "Save & Email";
			NavigationPage.SetBackButtonTitle(this, "");

			var configuration = new ToolbarItem
			{
				Icon = "settings.png",
			};

			ToolbarItems.Add(configuration);

			SaveDataToFileAndEmail();
		}

		/// <summary>
		/// save data to file and attach for email to that file
		/// </summary>
		async void SaveDataToFileAndEmail()
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
				App.dataModifiedAndSaved = false;
				await App.Current.MainPage.DisplayAlert("Error", "No File available", "OK");
				moveToRootPage();
			}

			else {
#if __ANDROID__
					App.checkStoragePermission();

					if (App.storageAccess == true)
					{
						await saveAndEmail();
					}

					else
					{
						await App.Current.MainPage.DisplayAlert("Access Denied", "App needs storage permission", "OK");
					}
#endif

#if __IOS__
				await saveAndEmail();
#if WINDOWS_UWP
            		await saveAndEmail();
#endif
#endif
			}
		}
		/// <summary>
		/// save and email
		/// </summary>
		/// <returns></returns>
		protected async Task saveAndEmail()
		{
			List<ScanDataTable> scanRecordsList = App.DatabaseRepo.GetAllScanDataRecords(App.fileName);
			var fileService = DependencyService.Get<ISaveAndLoad>();

			if (scanRecordsList.Count > 0)
			{
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
							List<LocalStrings> localStringsList = App.DatabaseRepo.GetLocalStringRecord();

							if (localStringsList.Count > 0)
							{
								App.configuredEmail = localStringsList[0].emailId;
							}

							moveToRootPage();
							App.dataModifiedAndSaved = true;
							App.dataModifiedAndSaveAndEnd = false;
							var email = new EmailMessageBuilder()
								.To(App.configuredEmail)
								.Subject("TallyScan File")
								.WithAttachment(file, ".tsx")
								.Build();
							EmailTask.SendEmail(email);
						}

						else
						{
							App.dataModifiedAndSaved = true;
							App.dataModifiedAndSaveAndEnd = false;
							await App.Current.MainPage.DisplayAlert("Cannot send mail", "Please configure email in device settings", "OK");
							moveToRootPage();
						}
					}
#endif

#if __ANDROID__
					var path = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
					foreach (string file in Directory.EnumerateFiles(path, string.Concat (App.fileName, ".tsx")))
					{
						var EmailTask = MessagingPlugin.EmailMessenger;

						if (EmailTask.CanSendEmail)
						{
							List<LocalStrings> localStringsList = App.DatabaseRepo.GetLocalStringRecord();

							if (localStringsList.Count > 0)
							{
								App.configuredEmail = localStringsList[0].emailId;
							}

							moveToRootPage();
							App.dataModifiedAndSaved = true;
							App.dataModifiedAndSaveAndEnd = false;
							var email = new EmailMessageBuilder()
								.To(App.configuredEmail)
								.Subject("TallyScan File")
								.WithAttachment(file, ".tsx")
								.Build();
							EmailTask.SendEmail(email);
						}

						else
						{
							App.dataModifiedAndSaved = true;
							App.dataModifiedAndSaveAndEnd = false;
							await App.Current.MainPage.DisplayAlert("Cannot send mail", "Please configure email in device settings", "OK");
							moveToRootPage ();
						}
					}
#endif

#if WINDOWS_UWP
                    var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                    var file = await localFolder.GetFileAsync(fileName);
                    var EmailTask = MessagingPlugin.EmailMessenger;
                    
                    if (EmailTask.CanSendEmail)
                    {
                        try
                        {
                            List<LocalStrings> localStringsList = App.DatabaseRepo.GetLocalStringRecord();

                            if (localStringsList.Count > 0)
                            {
                                App.configuredEmail = localStringsList[0].emailId;
                            }
                            App.dataModifiedAndSaved = true;
                            App.dataModifiedAndSaveAndEnd = false;
                            moveToRootPage();                            
                            var email = new EmailMessageBuilder()
                                .To(App.configuredEmail)
                                .Subject("TallyScan File")
                                .WithAttachment(file)
                                .Build();
                            EmailTask.SendEmail(email);
                        }
                        catch
                        {
                            App.dataModifiedAndSaved = true;
                            App.dataModifiedAndSaveAndEnd = false;
                            await DisplayAlert("Cannot send mail", "Please configure email in settings", "OK");
                            moveToRootPage();
                        }

                    }
                    else
                    {
                        App.dataModifiedAndSaved = true;
                        App.dataModifiedAndSaveAndEnd = false;
                        await DisplayAlert("Cannot send mail", "Please configure email in settings", "OK");
                        moveToRootPage();
                    }
#endif
				}
			}

			else
			{
				App.dataModifiedAndSaved = false;
				await App.Current.MainPage.DisplayAlert("Error", "No Data to save", "OK");
				moveToRootPage();
			}
		}

		/// <summary>
		/// Move to rootpage method
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