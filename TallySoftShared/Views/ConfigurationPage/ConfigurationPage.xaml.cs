using System;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using TallySoftShared.Model;

namespace TallySoftShared
{
    /// <summary>
    /// configration page
    /// </summary>
    public partial class ConfigurationPage : ContentPage
    {
        #region global variables
        public bool scannerOrDeviceCheckBoxBool;
        public bool qtyCheckboxBool;
        public bool duplicateBarcodeCheckBoxBool;
        public string emailID;
        public bool testBoxBool;
        public static int localStringsCount;
        #endregion

        /// <summary>
        /// static instance for configuration page
        /// </summary>
        private static ConfigurationPage configurationPage = null;

        public static ConfigurationPage getConfigurationPageInstance()
        {
            if (configurationPage == null)
            {
                return new ConfigurationPage();
            }
            else
                return configurationPage;
        }

        /// <summary>
        /// configuration page constructor
        /// </summary>
        private ConfigurationPage()
        {
            InitializeComponent();
            if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
            {
                NavigationPage.SetHasNavigationBar(this, false);
                SetControllSettings();
            }

            else
            {
                Title = "Configuration";
                NavigationPage.SetBackButtonTitle(this, "");
                this.Padding = new Thickness(0, 0, 0, -55);
                if (Device.OS == TargetPlatform.iOS)
                {
                    TallySoftLogo.TranslationY = -90;
                }

				lblHeader.IsVisible = false;
				BackButton.IsVisible = false;
				BackImg.IsVisible = false;
				ConfigurationLabel.IsVisible = false;
            }
            this.BackgroundColor = Color.White;
        }

        /// <summary>
        /// back button tap event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void BackTap(object sender, EventArgs e)
        {
            updateDB();
            await UpdateConfigurationSettingTable();
        }

        /// <summary>
        /// update configuration setting into table
        /// </summary>
        /// <returns></returns>
        private async Task UpdateConfigurationSettingTable()
        {
            App.scannerOrDeviceCheckBoxBool = scannerOrDeviceCheckBoxBool;
            App.qtyCheckboxBool = qtyCheckboxBool;
            App.duplicateBarcodeCheckBoxBool = duplicateBarcodeCheckBoxBool;
            App.testBoxBool = testBoxBool;
            List<LocalStrings> localStringsList = App.DatabaseRepo.GetLocalStringRecord();
            App.DatabaseRepo.AddNewLocalStringsRecord(scannerOrDeviceCheckBoxBool, qtyCheckboxBool, duplicateBarcodeCheckBoxBool, DefaultEmailField.Text, testBoxBool);
            await Navigation.PopAsync(true);
        }

        /// <summary>
        /// Handle hardware back button for android and windows
        /// </summary>
        protected override bool OnBackButtonPressed()
        {
            if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
            {
                updateDB();
                Navigation.PopAsync(true);
            }
            if (Device.OS == TargetPlatform.Android)
            {
                Navigation.PopAsync();
            }

            return true;
        }

        /// <summary>
        /// Page ondisappearing
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            updateDB();
        }

        /// <summary>
        /// Page appearing
        /// </summary>
        protected override void OnAppearing()
        {
            if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
            {
                NavigationPage.SetHasNavigationBar(this, false);
                NavigationPage.SetHasBackButton(this, false);
            }
            //Checkbox 1
            if (scannerOrDeviceCheckBoxBool == true)
            {
                scannerOrDeviceCheckBoxBool = true;
                ScannerOrDeviceCheckBox.Source = "check.png";
            }
            else
            {
                scannerOrDeviceCheckBoxBool = false;
                ScannerOrDeviceCheckBox.Source = "uncheck.png";
            }

            //Checkbox 2
            if (qtyCheckboxBool == true)
            {
                qtyCheckboxBool = true;
                QtyCheckBox.Source = "check.png";
            }
            else
            {
                qtyCheckboxBool = false;
                QtyCheckBox.Source = "uncheck.png";
            }

            //Checkbox 3
            if (duplicateBarcodeCheckBoxBool == true)
            {
                duplicateBarcodeCheckBoxBool = true;
                BarcodeCheckBox.Source = "check.png";
            }
            else
            {
                duplicateBarcodeCheckBoxBool = false;
                BarcodeCheckBox.Source = "uncheck.png";
            }

            //Email ID
            DefaultEmailField.Text = emailID;

            //Checkbox 4
            if (testBoxBool == true)
            {
                testBoxBool = true;
                TestScanCheckBox.Source = "check.png";
            }
            else
            {
                testBoxBool = false;
                TestScanCheckBox.Source = "uncheck.png";
            }

            base.OnAppearing();
        }

        /// <summary>
        /// Email Validation
        /// </summary>
        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        /// <summary>
        /// Update the local strings talbe whenever user changes the values to check boxes or email id
        /// </summary>
        protected void updateDB()
        {
            App.DatabaseRepo.AddNewLocalStringsRecord(scannerOrDeviceCheckBoxBool, qtyCheckboxBool, duplicateBarcodeCheckBoxBool, DefaultEmailField.Text, testBoxBool);
        }

        /// <summary>
        /// Handle text box done button click 
        /// </summary>
        protected void Entry_Finished(object sender, EventArgs e)
        {
            Scroller.Unfocused += scrollView_Unfocused;

            if (isValidEmail(DefaultEmailField.Text))
            {
                this.DefaultEmailField.Unfocus();
            }

            else
            {
                DisplayAlert("Error", "Please add valid email!", "Ok");
                DefaultEmailField.Text = "";
            }
        }

        /// <summary>
        /// set scroll view postion to normal
        /// </summary>
        private void scrollView_Unfocused(object sender, FocusEventArgs e)
        {
            Scroller.ScrollToAsync(0, 0, true);
        }

        /// <summary>
        /// Handle custom back button click
        /// </summary>
        protected void OnBackButon_click(object sender, EventArgs e)
        {
            App.DatabaseRepo.AddNewLocalStringsRecord(scannerOrDeviceCheckBoxBool, qtyCheckboxBool, duplicateBarcodeCheckBoxBool, DefaultEmailField.Text, testBoxBool);

            if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
            {
                Navigation.PopAsync(true);
            }

            else if (Device.OS == TargetPlatform.Android)
            {
                Navigation.PopAsync();
            }
        }

        /// <summary>
        /// Handle first checkbox action
        /// </summary>
        void ScannerOrDeviceCheckBoxTapped(object sender, EventArgs args)
        {
            if (scannerOrDeviceCheckBoxBool == false)
            {
                ScannerOrDeviceCheckBox.Source = "check.png";
                scannerOrDeviceCheckBoxBool = true;
            }

            else
            {
                ScannerOrDeviceCheckBox.Source = "uncheck.png";
                scannerOrDeviceCheckBoxBool = false;
            }
        }

        /// <summary>
        /// Handle second checkbox action
        /// </summary>
        void QtyCheckBoxTapped(object sender, EventArgs args)
        {
            if (qtyCheckboxBool == false)
            {
                QtyCheckBox.Source = "check.png";
                qtyCheckboxBool = true;
            }

            else
            {
                QtyCheckBox.Source = "uncheck.png";
                qtyCheckboxBool = false;
            }
        }

        /// <summary>
        /// Handle third checkbox action
        /// </summary>
        void BarcodeCheckBoxTapped(object sender, EventArgs args)
        {
            if (duplicateBarcodeCheckBoxBool == false)
            {
                BarcodeCheckBox.Source = "check.png";
                duplicateBarcodeCheckBoxBool = true;
            }

            else
            {
                BarcodeCheckBox.Source = "uncheck.png";
                duplicateBarcodeCheckBoxBool = false;
            }
        }

        /// <summary>
        /// Handle final checkbox action
        /// </summary>
        void TestScanCheckBoxTapped(object sender, EventArgs args)
        {
            if (testBoxBool == false)
            {
                TestScanCheckBox.Source = "check.png";
                testBoxBool = true;
            }

            else
            {
                TestScanCheckBox.Source = "uncheck.png";
                testBoxBool = false;
            }
        }

        private void SetControllSettings()
        {
			if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
			{
				MainStackLayout.Padding = new Thickness(0, 0, 0, 0);

				Scroller.TranslationY = -170;
				Scroller.BackgroundColor = Color.Transparent;

				lblHeader.HeightRequest = 50;
				TallySoftLogo.TranslationY = -75;

				TallySoftLogo.HorizontalOptions = LayoutOptions.CenterAndExpand;
				TallySoftLogo.VerticalOptions = LayoutOptions.Start;
				TallySoftLogo.WidthRequest = 200;
				TallySoftLogo.HeightRequest = 150;

				BackButton.IsVisible = false;
				Container.Padding = new Thickness(0, 15, 70, 0);

#if WINDOWS_UWP
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

				DefaultEmailLabel.TranslateTo(15, -25);
				DefaultEmailField.TranslateTo(120, -60);
				TestScanCheckBox.TranslateTo(25, -45);
				TestScanInfoLabel.TranslateTo(120, -75);

				ConfigurationLabel.FontSize = 25;
				ConfigurationLabel.TranslationY = -82;
				ConfigurationLabel.TranslationX = 30;
				ConfigurationLabel.HorizontalOptions = LayoutOptions.Start;
				ConfigurationLabel.VerticalOptions = LayoutOptions.FillAndExpand;

				BackImg.TranslateTo(5, -40);
				BackImg.HeightRequest = 25;
				BackImg.WidthRequest = 20;
				DefaultEmailLabel.Text = "Default Email";
				ScannerInfoLabel.Text = "Use Scanner other than device camera";
				TestScanInfoLabel.Text = "Test Scan";
				QTYInfoLabel.Text = "Prompt from & \"QTY\" (Quantity), Otherwise the QTY will default to 1 for continuous add row on scan";
				BarcodeInfoLabel.Text = "Append - If an item with the Same barcode is scanned, then append the QTY (quantity) of the duplicate scan";
				ScannerOrDeviceCheckBox.HorizontalOptions = LayoutOptions.Start;
				ScannerOrDeviceCheckBox.VerticalOptions = LayoutOptions.Start;
				BarcodeCheckBox.HorizontalOptions = LayoutOptions.Start;
				ScannerInfoLabel.HorizontalOptions = LayoutOptions.FillAndExpand;
				ScannerInfoLabel.LineBreakMode = LineBreakMode.WordWrap;
				QtyCheckBox.HorizontalOptions = LayoutOptions.Start;
				QTYInfoLabel.HorizontalOptions = LayoutOptions.FillAndExpand;
				QTYInfoLabel.LineBreakMode = LineBreakMode.WordWrap;
			}
        }
    }
}