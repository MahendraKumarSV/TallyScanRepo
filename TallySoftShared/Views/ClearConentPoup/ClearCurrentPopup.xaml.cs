using System;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace TallySoftShared
{
    /// <summary>
    /// ClearCurrentPopup Page
    /// </summary>
	public partial class ClearCurrentPopup : PopupPage
	{
		bool imageTapped = false;

        /// <summary>
        /// ClearCurrentPopup constructor
        /// </summary>
		public ClearCurrentPopup()
		{
			InitializeComponent();
			Animation = new UserAnimation();
			NavigationPage.SetHasBackButton(this, false);
			NavigationPage.SetHasNavigationBar(this, false);
		}


        /// <summary>
        /// OnAppearing
        /// </summary>
		protected override void OnAppearing()
		{
			if (Device.OS == TargetPlatform.Android) // check for andriod os 
			{
				CheckOrUnCheckImg.TranslationX = 85;
				//clearHeadingLabel.TranslationX = 115;
			}
			else if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone) //check for windows os
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

			base.OnAppearing();
		}

        /// <summary>
        /// Ok button Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void OnOk(object sender, EventArgs e)
		{
			if (imageTapped == true)
			{
				/// <summary>
				/// Set bool value for a variable
				/// </summary> 
				App.DatabaseRepo.ClearCurrentFileRecords(App.fileName);
				App.dataModifiedAndSaved = false;
				App.dataModifiedAndSaveAndEnd = false;
				/// <summary>
				/// Set bool value for a variable
				/// </summary> 
				MainMenu.openBtn.Source = ImageSource.FromFile("open_current.png");
				MainMenu.openBtnGesture.CommandParameter = "enable";
				MainMenu.clearBtn.Source = ImageSource.FromFile("disableclear_current.png");
				MainMenu.clearBtnGesture.CommandParameter = "disable";
				MainMenu.fileManagerBtn.Source = ImageSource.FromFile("newbtn.png");
				MainMenu.fileManagerBtnGesture.CommandParameter = "enable";
			}

			PopupNavigation.PopAsync(false);
		}

        /// <summary>
        /// Cancel button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void OnCancel(object sender, EventArgs e)
		{
			PopupNavigation.PopAsync(false);
		}

        /// <summary>
        /// Onclose event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void OnClose(object sender, EventArgs e)
		{
            if(Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
            {
                 PopupNavigation.PopAsync();
            }
            else
            {
                PopupNavigation.PopAsync(false);
            }
			
		}

        /// <summary>
        /// Checkbox tap event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
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