using System;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;

namespace TallySoftShared
{
    public partial class ClearCurrentPopup : PopupPage
    {
        bool imageTapped = false;       
        public ClearCurrentPopup()
        {
            InitializeComponent();
            Animation = new UserAnimation();

            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
        }

		protected override void OnAppearing()
		{
			if (Device.OS == TargetPlatform.Android) {
				CheckOrUnCheckImg.TranslationX = 90;
				ClearFileInfoLabel.FontSize = 30;
				ClearFileInfoLabel.TranslationX = 120;
			}
            if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
            {
                OKButton.IsEnabled = false;
                if (OKButton.IsEnabled)
                {
                    btnLayout.BackgroundColor = Color.Green;
                }
                OKButton.WidthRequest = 100;
                CancelButton1.WidthRequest = 100;
                ClearFileInfoLabel.TranslationX = 130;
                CheckOrUnCheckImg.TranslationX = 100;
            }
            NavigationPage.SetHasNavigationBar (this, false);
			NavigationPage.SetHasBackButton (this, false);

			base.OnAppearing ();
		}

		private async void OnOk(object sender, EventArgs e)
		{

            if (imageTapped == true)
            {
                App.SaveAndEnd = false;
                //await App.DatabaseRepo.DeleteCurrentFileRecord();
                //await App.DatabaseRepo.DeleteAllScanRecords();
                await App.DatabaseRepo.DeleteCurrentFileRecord(App.currentFileID);
                MainMenu.openBtn.Source = ImageSource.FromFile("open_current.png");
                //MainMenu.firstGesture.CommandParameter = "disable";
                MainMenu.openBtnGesture.CommandParameter = "enable";
                MainMenu.clearBtn.Source = ImageSource.FromFile("disableclear_current.png");
                MainMenu.clearBtnGesture.CommandParameter = "disable";
                MainMenu.newBtn.Source = ImageSource.FromFile("newbtn.png");
                MainMenu.newBtnGesture.CommandParameter = "enable";
            }

            if (Device.OS == TargetPlatform.iOS)
            {
                await PopupNavigation.PopAsync(false);
            }
            else
            {
                await PopupNavigation.PopAsync(false);
            }
            
           
		}

		private async void OnCancel(object sender, EventArgs e)
		{
            if (Device.OS == TargetPlatform.iOS)
            {
                await PopupNavigation.PopAsync(false);
            }
            else
            {
                await PopupNavigation.PopAsync(false);
            }
        }
        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }
        void CheckMarkTapped(object sender, EventArgs args)
        {
            if (imageTapped == false)
            {
                CheckOrUnCheckImg.Source = "check.png";
                imageTapped = true;
                OKButton.BackgroundColor = Color.FromHex("70B856");
                OKButton.IsEnabled = true;
            }

            else
            {
                OKButton.IsEnabled = false;
                OKButton.BackgroundColor = Color.Silver;
                CheckOrUnCheckImg.Source = "uncheck.png";
                imageTapped = false;
            }
        }        
	}
}