using System;
using Xamarin.Forms;
using System.Collections.Generic;
using TallySoftShared.Model;
using Rg.Plugins.Popup.Extensions;

#if __ANDROID__
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Android.OS;
#endif

namespace TallySoftShared
{
    /// <summary>
    /// MainMenu Page
    /// </summary>
	public partial class MainMenu : ContentPage
	{
		public static Image openBtn;
		public static Image clearBtn;
		public static Image fileManagerBtn;
		public static TapGestureRecognizer openBtnGesture;
		public static TapGestureRecognizer clearBtnGesture;
		public static TapGestureRecognizer fileManagerBtnGesture;

        /// <summary>
        /// Main menu constructor
        /// </summary>
		public MainMenu ()
		{
			InitializeComponent();
			Title = "Main Menu"; // Page title
			NavigationPage.SetBackButtonTitle(this, "");
			NavigationPage.SetHasBackButton(this, false);

			openBtn = openCurrentImage;
			openBtnGesture = openCurrentGesture;
			clearBtn = clearCurrentImage;
			clearBtnGesture = clearCurrentGesture;
			fileManagerBtn = fileManagerButtonImage;
			fileManagerBtnGesture = fileManagerButtonGesture;

            this.BackgroundColor = Color.White; // page background color
            #if WINDOWS_UWP
                this.Title = "Main Menu";
                SetControllSettings();
            #endif

            /// <summary>
            /// set right button for page and declare action to it
            /// </summary>
            var configuration = new ToolbarItem
			{
				Icon = "settings.png",
			};

			configuration.Clicked += configurationButtonClicked;
			ToolbarItems.Add(configuration);
		}

		/// <summary>
		/// Handle Configuration button click
		/// </summary>
		protected void configurationButtonClicked(object sender, EventArgs e)
		{
			List<LocalStrings> localStringsList = App.DatabaseRepo.GetLocalStringRecord();

			if (localStringsList.Count > 0)
			{
				ConfigurationPage.localStringsCount = localStringsList.Count;
			}

			var detailPage = ConfigurationPage.getConfigurationPageInstance();

			if (localStringsList.Count > 0)
			{
				detailPage.scannerOrDeviceCheckBoxBool = localStringsList[0].scannerOrDeviceCheckBoxBool;
				detailPage.qtyCheckboxBool = localStringsList[0].qtyCheckboxBool;
				detailPage.duplicateBarcodeCheckBoxBool = localStringsList[0].barcodeCheckBoxBool;
				detailPage.emailID = localStringsList[0].emailId;
				detailPage.testBoxBool = localStringsList[0].testCheckBoxBool;
			}

			else
			{
				detailPage.scannerOrDeviceCheckBoxBool = false;
				detailPage.qtyCheckboxBool = false;
				detailPage.duplicateBarcodeCheckBoxBool = false;
				detailPage.emailID = "";
				detailPage.testBoxBool = false;
			}

			Navigation.PushAsync(detailPage,true);
		}

		/// <summary>
		/// Handle default hardware back button for android and close the app
		/// </summary>
		protected override bool OnBackButtonPressed()
		{
#if __ANDROID__
				Android.OS.Process.KillProcess (Android.OS.Process.MyPid ());
#endif
#if WINDOWS_UWP
            	Windows.UI.Xaml.Application.Current.Exit();
#endif
			return true;
		}

        /// <summary>
        /// Page on appearing
        /// </summary>
		protected override void OnAppearing ()
		{
			#if WINDOWS_UWP
            	Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().ExitFullScreenMode();
			#endif

			if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android) {
				titleLabel.IsVisible = false;
			}

			//Get stored current file name count
			List<StoredFileName> currentStoredFileCount = App.DatabaseRepo.GetCurrentStoredFileName();

			if (currentStoredFileCount.Count > 0)
			{
				App.fileName = currentStoredFileCount[0].name;

				if (App.currentFileID == 0)
				{
					App.currentFileID = currentStoredFileCount[0].fileid;
				}
			}

			else if (currentStoredFileCount.Count == 0)
			{
				App.currentFileID = 0;
				App.fileName = "";
			}

			List<ScanDataTable> scanRecordsList = App.DatabaseRepo.GetAllScanDataRecords(App.fileName);

			if (String.IsNullOrEmpty(App.fileName) && App.currentFileID == 0)
			{
				openCurrentGesture.CommandParameter = "disable";
				clearCurrentGesture.CommandParameter = "disable";
				fileManagerButtonGesture.CommandParameter = "enable";

				openCurrentImage.Source = ImageSource.FromFile("disableopen_current.png");
				clearCurrentImage.Source = ImageSource.FromFile("disableclear_current.png");
				fileManagerButtonImage.Source = ImageSource.FromFile("newbtn.png");
			}

			else if (!String.IsNullOrEmpty(App.fileName) && App.currentFileID > 0 && scanRecordsList.Count == 0)
			{
				if ((App.dataModifiedAndSaved == true || App.dataModifiedAndSaved == false) && App.dataModifiedAndSaveAndEnd == false)
				{
					openCurrentGesture.CommandParameter = "enable";
					clearCurrentGesture.CommandParameter = "disable";
					fileManagerButtonGesture.CommandParameter = "enable";

					openCurrentImage.Source = ImageSource.FromFile("open_current.png");
					clearCurrentImage.Source = ImageSource.FromFile("disableclear_current.png");
					fileManagerButtonImage.Source = ImageSource.FromFile("newbtn.png");
				}

				else if (App.dataModifiedAndSaved == false && App.dataModifiedAndSaveAndEnd == true)
				{
					openCurrentGesture.CommandParameter = "disable";
					clearCurrentGesture.CommandParameter = "disable";
					fileManagerButtonGesture.CommandParameter = "enable";

					openCurrentImage.Source = ImageSource.FromFile("disableopen_current.png");
					clearCurrentImage.Source = ImageSource.FromFile("disableclear_current.png");
					fileManagerButtonImage.Source = ImageSource.FromFile("newbtn.png");
				}
			}

			else if (!String.IsNullOrEmpty(App.fileName) && App.currentFileID > 0 && scanRecordsList.Count > 0)
			{
				if (App.dataModifiedAndSaved == false && App.dataModifiedAndSaveAndEnd == false)
				{
					openCurrentGesture.CommandParameter = "enable";
					clearCurrentGesture.CommandParameter = "enable";
					fileManagerButtonGesture.CommandParameter = "disable";

					openCurrentImage.Source = ImageSource.FromFile("open_current.png");
					clearCurrentImage.Source = ImageSource.FromFile("clear_current.png");
					fileManagerButtonImage.Source = ImageSource.FromFile("disablenew.png");
				}

				else if (App.dataModifiedAndSaved == true && App.dataModifiedAndSaveAndEnd == false)
				{
					openCurrentGesture.CommandParameter = "enable";
					clearCurrentGesture.CommandParameter = "enable";
					fileManagerButtonGesture.CommandParameter = "enable";

					openCurrentImage.Source = ImageSource.FromFile("open_current.png");
					clearCurrentImage.Source = ImageSource.FromFile("clear_current.png");
					fileManagerButtonImage.Source = ImageSource.FromFile("newbtn.png");
				}

				else if (App.dataModifiedAndSaved == false && App.dataModifiedAndSaveAndEnd == true)
				{
					openCurrentGesture.CommandParameter = "disable";
					clearCurrentGesture.CommandParameter = "disable";
					fileManagerButtonGesture.CommandParameter = "enable";

					openCurrentImage.Source = ImageSource.FromFile("disableopen_current.png");
					clearCurrentImage.Source = ImageSource.FromFile("disableclear_current.png");
					fileManagerButtonImage.Source = ImageSource.FromFile("newbtn.png");
				}
			}

			App.currentScreen = "MainMenu";
			base.OnAppearing ();
		}

		/// <summary>
        /// Page on disappearing
        /// </summary>
        protected override void OnDisappearing()
		{
			App.currentScreen = "";
            base.OnDisappearing();
        }

		/// <summary>
		/// Handle Open button click
		/// </summary>
		protected async void openTap(object sender, EventArgs args) {

			var d = (TappedEventArgs)args;

			if (d.Parameter.ToString() == "enable")
			{
				var page = NativeCellPage.getNativeCellPageInstance();
                if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
				{
					await Navigation.PushAsync(page, true);
				}

				else if (Device.OS == TargetPlatform.Android)
				{
					await Navigation.PushAsync(page);
				}
			}
		}

		/// <summary>
		/// Handle Clear button click
		/// </summary>
		protected void clearTap(object sender, EventArgs args)
		{
			var d = (TappedEventArgs)args;

			if (d.Parameter.ToString() == "enable")
			{
                #if WINDOWS_UWP
                {
                    await Rg.Plugins.Popup.Services.PopupNavigation.PushAsync(new ClearCurrentPopup());
                }
				#endif
                if (Device.OS != TargetPlatform.Windows || Device.OS != TargetPlatform.WinPhone)
                {
                    var page = new ClearCurrentPopup();
                    Navigation.PushPopupAsync(page);
                }
			}
		}

		/// <summary>
		/// Handle file manager button click
		/// </summary>
		protected async void fileManagerTap(object sender, EventArgs args)
		{
			var d = (TappedEventArgs)args;

			if (d.Parameter.ToString() == "enable")
			{
				var fileManager = FileManagerPage.getfileManagerPageInstance();

				if (Device.OS == TargetPlatform.iOS)
				{
					await Navigation.PushAsync(fileManager, true);
				}
                else if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
                {
                    var popUpPage = FileManagerPage.getfileManagerPageInstance();
                    await Navigation.PushAsync(popUpPage, true);
                }
                else if (Device.OS == TargetPlatform.Android)
				{
#if __ANDROID__
						App.checkStoragePermission();
#endif

					if (App.storageAccess == true)
					{
						await Navigation.PushAsync(fileManager);
					}

					else
					{
						await DisplayAlert("Access Denied", "App needs storage permission", "OK");
					}
				}
			}
		}

        /// <summary>
        /// dynmically page(s) control settings
        /// </summary>
        private void SetControllSettings()
        {
            this.BackgroundColor = Color.White;
            if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
            {
                openBtn.HeightRequest = 60;
                clearBtn.HeightRequest = 60;
                fileManagerBtn.HeightRequest = 60;
                ImgLogo.TranslationX = 20;
                ImgLogo.HorizontalOptions = LayoutOptions.Center;
                ImgLogo.VerticalOptions = LayoutOptions.Start;
                ImgLogo.WidthRequest = 200;
                ImgLogo.HeightRequest = 150;

            }
        }
    }
}