using Xamarin.Forms;
using System;
using System.Text.RegularExpressions;
using TallySoftShared.Model;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections.Generic;

#if __IOS__
using Foundation;
using UIKit;
#endif

#if WINDOWS_UWP
using Windows.Networking.Proximity;
#endif

namespace TallySoftShared
{
    /// <summary>
    /// Popup page
    /// </summary>
	public partial class PopUpPage : ContentPage
	{
        #region global variables
        string currentTime;
		public string skuText;
		public string qty;
		public string existingQty;
		public string currentRowNo;
		public bool qtyCheckboxBool;
		public bool duplicateBarcodeCheckBoxBool;		
		public bool showNewFilePopup;
        #endregion 

        /// <summary>
        /// static instance for popup page
        /// </summary>
        private static PopUpPage popUpPage = null;


        public static PopUpPage getPopUpPageInstance()
		{
			if (popUpPage == null)
			{
				return new PopUpPage();
			}
			else
				return popUpPage;
		}

        /// <summary>
        /// popup page constructor
        /// </summary>
		private PopUpPage()
		{
			InitializeComponent();
			this.BackgroundColor = Color.White;
			NavigationPage.SetBackButtonTitle(this, "");

			if (App.popupPageTitle != null)
			{
				headingLabel.Text = App.popupPageTitle;
				if (App.popupPageTitle.Equals("Enter new file name"))
				{
					Title = "File Name";
					headingLabel.TextColor = Color.Black;
				}
				else if (App.popupPageTitle.Equals("Enter Quantity"))
				{
					Title = "Enter Quantity";
					headingLabel.TextColor = Color.Transparent;
				}
				else if (App.popupPageTitle.Equals("New Quantity"))
				{
					Title = "New Quantity";
					headingLabel.TextColor = Color.Transparent;
				}
			}
		}


        
		#if WINDOWS_UWP
        /// <summary>
        /// Find the connected device with bluetooth
        /// </summary>
        private async void RefreshPairedDevicesList()
        {
            try
            {
                var disc1 = Windows.Devices.Bluetooth.BluetoothDevice.GetDeviceSelectorFromConnectionStatus(Windows.Devices.Bluetooth.BluetoothConnectionStatus.Disconnected);
                var connect1 = Windows.Devices.Bluetooth.BluetoothDevice.GetDeviceSelectorFromConnectionStatus(Windows.Devices.Bluetooth.BluetoothConnectionStatus.Connected);
                // Search for all paired devices
                PeerFinder.AlternateIdentities["Bluetooth:Paired"] = "";
                var peers = await PeerFinder.FindAllPeersAsync();
                if (peers.Count > 0)
                {
                    foreach (var peer in peers)
                    {
                        var connecteddev = Windows.Devices.Bluetooth.BluetoothDevice.GetDeviceSelectorFromConnectionStatus(Windows.Devices.Bluetooth.BluetoothConnectionStatus.Connected);
                        var selector = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(connecteddev);
                        if (selector != null && selector.Count == 1)
                        {
                            showQtyPopup = false;
                            Windows.UI.ViewManagement.InputPane.GetForCurrentView().TryHide();
                            await DisplayAlert("Cannot Add New File", "Turn off bluetooth to add the new file", "OK");
                            await Navigation.PopAsync(true);
                            break;
                        }
                        else
                        {
                            showQtyPopup = true;
                            Windows.UI.ViewManagement.InputPane.GetForCurrentView().TryShow();
                            break;
                        }
                    }
                }
                else
                {
                    showQtyPopup = true;
                    Windows.UI.ViewManagement.InputPane.GetForCurrentView().TryShow();
                }


            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x8007048F)
                {
                    showQtyPopup = true;
                    Windows.UI.ViewManagement.InputPane.GetForCurrentView().TryShow();

                }
                else if ((uint)ex.HResult == 0x80070005)
                {
                    showQtyPopup = true;
                    Windows.UI.ViewManagement.InputPane.GetForCurrentView().TryShow();
                }
                else
                {
                    showQtyPopup = true;
                    Windows.UI.ViewManagement.InputPane.GetForCurrentView().TryShow();

                }
            }
        }
#endif

		/// <summary>
        /// Hardware Back Press
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
		{
			#if __ANDROID__
				 Navigation.PopToRootAsync(false);            
			#endif
			#if WINDOWS_UWP
            	var page = Navigation.NavigationStack.First();
            	if (page.Navigation.NavigationStack.Count > 1)
            	{
                	//page.Navigation.PopModalAsync(false);
                	page.Navigation.PopAsync(true);
            	}
            	else
            	{
                	//page.Navigation.PopModalAsync(false);
                	page.Navigation.PopAsync(true);
            	}
			#endif
			return true;
		}

        /// <summary>
        /// Page on appearing
        /// </summary>
		protected async override void OnAppearing()
		{
			//entry.Keyboard = Keyboard.Numeric;
			//entry.Focus();

			#if WINDOWS_UWP           
            	// Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TryEnterFullScreen‌​Mode();
            	if (headingLabel.Text.Equals("Enter new file name") && !IsHardwareBackClick)
            	{
                	RefreshPairedDevicesList();
            	}
			#endif

			App.currentScreen = "PopUpPage";

			if (App.popupPageTitle.Equals("Enter new file name"))
			{
				entry.Keyboard = Keyboard.Default;
			}

			else if (App.popupPageTitle.Equals("Enter Quantity"))
			{
				entry.Keyboard = Keyboard.Numeric;
			}

			else if (App.popupPageTitle.Equals("New Quantity"))
			{
				if (qty != null && qty.Trim().Length > 0)
				{
					entry.Text = qty;
				}

				entry.Keyboard = Keyboard.Numeric;
			}

			if (Device.OS == TargetPlatform.iOS)
			{
				entry.Focus();

				showNewFilePopup = App.showQtyPopup;

				if (showNewFilePopup == false)
				{
					if (headingLabel.Text.Equals("Enter new file name"))
					{
						if (Device.OS == TargetPlatform.iOS)
						{
							await DisplayAlert("Cannot Add New File", "Turn off bluetooth to add new file", "OK");
							await Navigation.PopAsync(true);
						}
					}
				}
			}

			else if (Device.OS == TargetPlatform.Android)
			{
				this.Padding = new Thickness(0, 0, 0, -30);
				await showKeypad();
			}

			base.OnAppearing();
		}

        /// <summary>
        /// input focused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		protected async void EntryFocussed(object sender, EventArgs e)
		{
			if (Device.OS == TargetPlatform.Android)
			{
				if (App.showQtyPopup == false)
				{
					if (headingLabel.Text.Equals("Enter new file name"))
					{
						await DisplayAlert("Cannot Add New File", "Turn off bluetooth to add new file", "OK");
						await Navigation.PopAsync(true);
					}
				}
			}
		}

        /// <summary>
        /// virtual keyboard show on input focus
        /// </summary>
        /// <returns></returns>
		protected async Task showKeypad()
		{
			await Task.Delay(500);
			entry.Focus();
		}

        /// <summary>
        /// page on disappearing
        /// </summary>
		protected override void OnDisappearing()
		{
			App.currentScreen = "";
			entry.Unfocus();
		}

		//Check whether quantity field contains leading zero's
		bool IsZeroOnly(string str)
		{
			bool flag = false;
			var counter = 0;
			var strlength = str.Length;

			foreach (char c in str)
			{
				if (c == '0')
					counter += 1;

				if (counter == strlength)
				{
					flag = true;
				}
			}

			return flag;
		}

		//Handle ok button click
		protected async void okBtnClick(object sender, EventArgs e)
		{
			string result = entry.Text;

			if (headingLabel.Text.Equals("Enter new file name"))
			{
				if (result == null || result == String.Empty || result.Trim().Length == 0)
				{
					await DisplayAlert("Alert", "File name should not be empty", "OK");
				}
				else {
					Match match = Regex.Match(result, "[^a-z0-9 ]", RegexOptions.IgnoreCase);
					if (match.Success)
					{
						await DisplayAlert("Alert", "The file name should not contain special character", "OK");
					}
					else if (match.Success == false && result != null && result != String.Empty && result.Trim().Length != 0)
					{
						string fileName = result.Trim();

						var fileExists = App.DatabaseRepo.IsfileExist(fileName);

						if (fileExists.Count > 0)
						{
							await DisplayAlert("File Name Exists", "The file name already exists, Choose different file name", "OK");
						}

						else 
						{
							App.DatabaseRepo.AddNewFilenameToTable(fileName);

							App.fileName = fileName;

							//Get all files
							List<CurrentFileName> currentFile = App.DatabaseRepo.GetCurrentFileName();
							App.currentFileID = currentFile[currentFile.Count - 1].id;
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
						}
					}
				}
			}

			else if (headingLabel.Text.Equals("Enter Quantity") || headingLabel.Text.Equals("New Quantity"))
			{
				if (result == null || result == String.Empty || result.Trim().Length == 0)
				{
					await DisplayAlert("Alert", "The quantity should not be empty", "OK");
				}
				else {
					result = result.TrimStart('0').Length > 0 ? result.TrimStart('0') : "0";
					Match match = Regex.Match(result, "[^0-9]", RegexOptions.IgnoreCase);

					if (match.Success)
					{
						await DisplayAlert("Alert", "Quantity should be numeric", "OK");
					}
					else if (match.Success == false && result != null && result != String.Empty && result.Trim().Length != 0)
					{
						if (result.Equals ("0") || IsZeroOnly (result)) {
							await DisplayAlert ("Alert", "Quantity should be non zero", "OK");
						} else {
							DateTime now = DateTime.Now.ToLocalTime ();
							String date = now.ToString ("yyMMddHHmm", DateTimeFormatInfo.InvariantInfo);
							currentTime = String.Concat (date);

							if (headingLabel.Text.Equals ("Enter Quantity")) {
								
								if (App.firstScanTime != null && App.firstScanTime != String.Empty)
								{
									App.DatabaseRepo.AddNewScanDataRecord(result, skuText, App.firstScanTime, currentTime, App.fileName);
								}
								else {
									App.DatabaseRepo.AddNewScanDataRecord(result, skuText, currentTime, currentTime, App.fileName);
								}

								if (Device.OS == TargetPlatform.iOS) {
									await Navigation.PopAsync (true);
								} else if (Device.OS == TargetPlatform.Android) {
									await Navigation.PopAsync ();
								}
							}
							else{
								List<ScanDataTable> scanRecordsList = App.DatabaseRepo.GetAllScanDataRecords(App.fileName);

								if (scanRecordsList.Count > 0)
								{
									if (qtyCheckboxBool == true && duplicateBarcodeCheckBoxBool == true)
									{
										if (qty != null && qty.Trim().Length > 0)
										{
											App.DatabaseRepo.UpdateQuantityForOneScanRecord(result, currentTime, qty, skuText, currentRowNo, App.fileName);
										}
										else if (existingQty != null && existingQty.Trim().Length > 0)
										{
											int currentQtyValue = Convert.ToInt32(result);
											int oldQtyValue = Convert.ToInt32(existingQty);
											int finalQtyValue = currentQtyValue + oldQtyValue;
											string updatedQtyValue = Convert.ToString(finalQtyValue);
											App.DatabaseRepo.UpdateQuantityForOneScanRecord(updatedQtyValue, currentTime, existingQty, skuText, currentRowNo, App.fileName);
										}
									}

									else
									{
										App.DatabaseRepo.UpdateQuantityForOneScanRecord(result, currentTime, qty, skuText, currentRowNo, App.fileName);
									}
								}

								else
								{
									App.DatabaseRepo.AddNewScanDataRecord(result, skuText, currentTime, currentTime, App.fileName);
								}

								if (Device.OS == TargetPlatform.iOS)
								{
									await Navigation.PopAsync(true);
								}

								else if (Device.OS == TargetPlatform.Android)
								{
									await Navigation.PopAsync();
								}
							}
						}
					}
				}
			}
		}

		//Handle cancel button click
		protected void cancelBtnClick(object sender, EventArgs e)
		{
			if (headingLabel.Text.Equals("Enter new file name"))
			{
				if (Device.OS == TargetPlatform.iOS)
				{
					Navigation.PopAsync(true);
				}

				else if (Device.OS == TargetPlatform.Android)
				{
					Navigation.PopAsync();
				}
			}

			else if (headingLabel.Text.Equals("Enter Quantity") || headingLabel.Text.Equals("New Quantity"))
			{
				if (Device.OS == TargetPlatform.iOS)
				{
					Navigation.PopAsync(true);
				}

				else if (Device.OS == TargetPlatform.Android)
				{
					Navigation.PopAsync();
				}
			}
		}
	}
}