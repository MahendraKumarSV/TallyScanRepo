using System;
using System.Collections.Generic;
using Plugin.Messaging;
using Xamarin.Forms;
using TallySoftShared.Model;

namespace TallySoftShared
{
    /// <summary>
    /// Help page
    /// </summary>
	public partial class HelpPage : ContentPage
	{
        /// <summary>
        /// Help page constructor
        /// </summary>
		public HelpPage ()
		{
			InitializeComponent ();
			Title = "Help";
			NavigationPage.SetBackButtonTitle(this, "");

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
        /// Hardware back button override method for andorid and windows
        /// </summary>
        /// <returns></returns>
		protected override bool OnBackButtonPressed()
		{
			if (Device.OS == TargetPlatform.Android)
			{
				if (Device.OS == TargetPlatform.Android)
				{
					Navigation.PushAsync(new MainMenu());
				}

				else if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
				{
					Navigation.PushModalAsync(new MainMenu());
				}

				Navigation.RemovePage(this);
			}

			return true;
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

			Navigation.PushAsync(detailPage);
		}

		/// <summary>
		/// For Email
		/// </summary>
		async void emailTapped(Object o,EventArgs e)
		{
			var EmailTask = MessagingPlugin.EmailMessenger;

			if (EmailTask.CanSendEmail)
				EmailTask.SendEmail ("Support@TallySoft.com", "Hi There", "Congrats!!!");
			else {
				await DisplayAlert ("Cannot send mail", "Please configure email in device settings", "OK");
				//moveToRootPage ();
			}
		}

		/// <summary>
		/// For Phone call
		/// </summary>
		void phoneTapped(Object o, EventArgs e)
		{
			var PhoneCallTask = MessagingPlugin.PhoneDialer;
			if (PhoneCallTask.CanMakePhoneCall)
				PhoneCallTask.MakePhoneCall("724-873-5264");
		}

		/// <summary>
		/// Move back to root page
		/// </summary>
		public void moveToRootPage()
		{
			if (Device.OS == TargetPlatform.iOS)
			{
				Navigation.PushAsync(new MainMenu(), false);
			}

			else if (Device.OS == TargetPlatform.Android)
			{
				Navigation.PushAsync(new MainMenu());
			}

			else if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
			{
				Navigation.PushModalAsync(new MainMenu());
			}

			Navigation.RemovePage(this);
		}
	}
}