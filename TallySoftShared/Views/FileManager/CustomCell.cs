using System;
using Xamarin.Forms;
using Plugin.Messaging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using TallySoftShared.Model;
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
    /// <summary>
    /// Listview custom cell class
    /// </summary>
	public class CustomCell : ViewCell
	{
		List<string> errorArgsList = new List<string>();

		public CustomCell()
		{
			/// <summary>
			/// initialize the check box image and set visiblity state and corresponding image
			/// </summary>

			var vetProfileImage = new Image
			{
				HeightRequest = 25,
				WidthRequest = 25,
				Aspect = Aspect.AspectFit,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
			};

			vetProfileImage.SetBinding(Image.IsVisibleProperty, "IsHide", BindingMode.TwoWay); // Based on the user selection the image will show and hide
			vetProfileImage.SetBinding(Image.SourceProperty, "DetailImage", BindingMode.TwoWay); // Based on the image selection the user will see the corresponding checkmark image

			/// <summary>
			/// initialize the filename label and based on position bind the corresponding array value
			/// </summary>
			var nameLabel = new Label() 
			{
				FontSize = 15,
				WidthRequest = 315,
				LineBreakMode = LineBreakMode.CharacterWrap,
				TextColor = Color.Black
			};

			nameLabel.SetBinding(Label.TextProperty, "Text", BindingMode.TwoWay);

			/// <summary>
			/// add file name label to seperate stacklayout
			/// </summary>
			var vetDetailsLayout = new StackLayout
			{
				Padding = new Thickness(10, 5, 10, 10),
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = { nameLabel }
			};

			/// <summary>
			/// initialize the boxview which is used as a seperator in between the file names
			/// </summary>
			var box = new BoxView()
			{
				Color = Color.Black,
				HeightRequest = 1,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Opacity = 0.5
			};

			/// <summary>
			/// add checkmark image and a stacklayout(which contains label in it) to seperate stacklayout
			/// </summary>
			var cellLayout = new StackLayout
			{
				BackgroundColor = Color.Transparent,
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = { vetProfileImage, vetDetailsLayout }
			};

			/// <summary>
			/// add boxview and a stacklayout(which contains checkmark image and stack layout (which contains label in it)) to seperate stacklayout
			/// </summary>
			var cellLayout1 = new StackLayout
			{
				Spacing = 10,
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = { cellLayout, box }
			};

			/// <summary>
			/// initialize first swipe action and set action to it
			/// </summary>
			var emailAction = new MenuItem { Text = "Email" };
			emailAction.SetBinding(MenuItem.CommandParameterProperty, ".");
			emailAction.Clicked += (object sender, EventArgs e) =>
			{
				var mi = ((MenuItem)sender);
				var fileName = (ListViewModel)mi.CommandParameter;
				App.fileName = fileName.Text;
				App.currentFileID = fileName.fileID;
				SaveDataToFileAndEmail();
			};

			/// <summary>
			/// initialize second swipe action and set action to it
			/// </summary>
			var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; // red background
			deleteAction.SetBinding(MenuItem.CommandParameterProperty, ".");
			deleteAction.Clicked += (object sender, EventArgs e) =>
			{
				errorArgsList.Add("Delete File?");
				errorArgsList.Add("Are you sure you want to delete the selected file?");
				/// <summary>
				/// pass the delgate to file manager page since this is view cell we cannot show the display alert
				/// </summary>
					if (Device.OS == TargetPlatform.Android)
					{
						#if __ANDROID__
							App.checkStoragePermission();
						#endif

						if (App.storageAccess == true)
						{
							var mi = ((MenuItem)sender);
							var fileName = (ListViewModel)mi.CommandParameter;
							FileManagerPage.allFileNames.Remove((ListViewModel)mi.CommandParameter);
							App.fileIDToBeDeleted = fileName.fileID;
							App.fileName = fileName.Text;
							//await App.DatabaseRepo.DeleteCurrentFileNameRecord(fileName.fileID);
							DeleteSingleFileFromMainFolder(fileName.Text);
							DeleteSingleFileFromEmailFolder(fileName.Text);
						}

						else
						{
							errorArgsList.Add("Access Denied");
							errorArgsList.Add("App needs storage permission");
							MessagingCenter.Send<CustomCell, List<string>>(this, "showAlert", errorArgsList);
						}
					}

					else
					{
						var mi = ((MenuItem)sender);
						var fileName = (ListViewModel)mi.CommandParameter;
						App.fileIDToBeDeleted = fileName.fileID;
						App.fileName = fileName.Text;
						FileManagerPage.allFileNames.Remove((ListViewModel)mi.CommandParameter);
						//await App.DatabaseRepo.DeleteCurrentFileNameRecord(fileName.fileID);
						DeleteSingleFileFromMainFolder(fileName.Text);
						DeleteSingleFileFromEmailFolder(fileName.Text);
					}

					deleteFunctionality();
					MessagingCenter.Unsubscribe<CustomCell>(this, "showAlertWithTwoOptions");
					MessagingCenter.Unsubscribe<FileManagerPage>(this, "deleteFile");
			};

			if (App.currentlyIsEditing == true)
			{
				/// <summary>
				/// add to the ViewCell's ContextActions property
				/// </summary>

				ContextActions.Add(emailAction);
				ContextActions.Add(deleteAction);
			}

			else
			{
				/// <summary>
				/// remove from ViewCell's ContextActions property
				/// </summary>

				ContextActions.Remove(emailAction);
				ContextActions.Remove(deleteAction);
			}

			/// <summary>
			/// assign the cell layout to view cell
			/// </summary>
			this.View = cellLayout1;
		}

        /// <summary>
        /// delete the current file from table
        /// </summary>
		protected void deleteFunctionality()
		{
			App.DatabaseRepo.DeleteCurrentFileNameRecord(App.fileName);

			List<CurrentFileName> fileNames = App.DatabaseRepo.GetCurrentFileName();

			if (fileNames.Count == 0)
			{
				MessagingCenter.Send<CustomCell>(this, "removeEditButton");
			}
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
		/// delete the single file from device storage from user created path
		/// </summary>
		protected void DeleteSingleFileFromEmailFolder(string filename)
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

			string path = string.Concat(dataFolder, "/ScanData");

			if (Directory.Exists(path))
			{
				string file = string.Concat(path + "/" + filename, ".tsx");
				File.Delete(file);
			}
		}

		/// <summary>
		/// save the updated file with data to local storage and attach the same in email
		/// </summary>
		async void SaveDataToFileAndEmail()
		{
			List<LocalStrings> localStringsList = App.DatabaseRepo.GetLocalStringRecord();

			if (localStringsList.Count > 0)
			{
				App.configuredEmail = localStringsList[0].emailId;
			}

			#if __ANDROID__
				App.checkStoragePermission();

				if (App.storageAccess == true)
				{
					await saveAndEmail();
				}

				else
				{
					errorArgsList.Add("Access Denied");
					errorArgsList.Add("App needs storage permission");
					MessagingCenter.Send<CustomCell, List<string>>(this, "showAlert", errorArgsList);
				}
			#endif

			#if __IOS__
					await saveAndEmail();
			#endif

			#if WINDOWS_UWP
                await  saveAndEmail();
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
								errorArgsList.Add("Cannot send mail");
								errorArgsList.Add("Please configure email in device settings");
								/// <summary>
								/// pass the delgate to file manager page since this is view cell we cannot show the display alert
								/// </summary>
								MessagingCenter.Send<CustomCell, List<string>>(this, "showAlert", errorArgsList);
							}
						}
					#endif

					#if __ANDROID__
						var path = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;

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
								errorArgsList.Add("Cannot send mail");
								errorArgsList.Add("Please configure email in device settings");
								/// <summary>
								/// pass the delgate to file manager page since this is view cell we cannot show the display alert
								/// </summary>
								MessagingCenter.Send<CustomCell, List<string>>(this, "showAlert", errorArgsList);
							}
						}
					#endif

					#if WINDOWS_UWP

                    var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                    var file = await localFolder.GetFileAsync(fileName);
                    var EmailTask = MessagingPlugin.EmailMessenger;
                    if (EmailTask.CanSendEmailAttachments)
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
                        errorArgsList.Add("Cannot send mail");
                        errorArgsList.Add("Please configure email in device settings");
                        MessagingCenter.Send<CustomCell, List<string>>(this, "showAlert", errorArgsList);
                    }
					#endif
				}
			}

			else
			{
				errorArgsList.Add("Error");
				errorArgsList.Add("No Data");
				/// <summary>
				/// pass the delgate to file manager page since this is view cell we cannot show the display alert
				/// </summary>
				MessagingCenter.Send<CustomCell, List<string>>(this, "showAlert", errorArgsList);
			}
		}
	}
}