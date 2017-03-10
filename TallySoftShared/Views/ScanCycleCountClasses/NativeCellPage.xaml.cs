using System;
using System.Globalization;
using TallySoftShared.Model;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
#if __ANDROID__
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Android.OS;
using Android.Content;
using Android.Views.InputMethods;
using ZXing.Mobile;
using Android.App;
using SQLite.Net.Platform.XamarinAndroid;
using Android;
using Android.Content.Res;
using Java.Util;
using System.IO;
using TallySoftShared.Droid;
#endif
#if __IOS__
using Foundation;
using UIKit;
using AVFoundation;
#endif

namespace TallySoftShared
{
	public partial class NativeCellPage : ContentPage
	{
		string currentTime;
		string selectedQuantity;
		string selectedSku;
		string fetchedSku;
		public static Entry barcodeField;

		public bool scannerOrDeviceCheckBoxBool;
		public bool qtyCheckboxBool;
		public bool duplicateBarcodeCheckBoxBool;
		public bool testBoxBool;
		List<ScanDataTable> scanRecordsList;

		bool showQtyPopup;

		private static NativeCellPage nativeCellPage = null;

		public string firstScanTime;

		public static NativeCellPage getNativeCellPageInstance()
		{
			if (nativeCellPage == null)
			{
				return new NativeCellPage();
			}
			else
				return nativeCellPage;
		}

		private NativeCellPage()
		{
			InitializeComponent();

			App.data[0].ToList()[0].TitleColor = Color.Black;
			App.data[0].ToList()[0].IconSource = "main_menu.png";
			App.data[0].ToList()[0].Active = false;

			barcodeField = BarcodeField;

			Title = "Scan List";

			NavigationPage.SetBackButtonTitle(this, "");
			NavigationPage.SetHasBackButton(this, false);

			this.BackgroundColor = Color.White;

			fileNameLabel.Text = App.fileName;

			barcodeField.WidthRequest = App.ScreenWidth - 75;

			if (Device.OS == TargetPlatform.Android)
			{
				cameraButton.TranslationY = -50;
				cameraButton.HeightRequest = 60;
				cameraButton.WidthRequest = 60;
				headerStack.TranslationY = -50;
				headerStack.HeightRequest = 45;
				listView.TranslationY = -45;
				listStack.Padding = new Thickness(0, 15, 0, 0);
				listView.RowHeight = 61;
			}

			else if (Device.OS == TargetPlatform.iOS)
			{
				headerStack.TranslationY = -30;
				listView.TranslationY = -35;
				quantity.TranslationY = 13;
				scanLabel.TranslationY = -18;
				scanLabel.TranslationX = 20;
			}

			databaseCalls();
		}

		protected void databaseCalls()
		{
			scanRecordsList = App.DatabaseRepo.GetAllScanDataRecords(App.fileName);

			List<LocalStrings> localStringsList = App.DatabaseRepo.GetLocalStringRecord();

			if (localStringsList.Count > 0)
			{
				scannerOrDeviceCheckBoxBool = localStringsList[0].scannerOrDeviceCheckBoxBool;
				qtyCheckboxBool = localStringsList[0].qtyCheckboxBool;
				duplicateBarcodeCheckBoxBool = localStringsList[0].barcodeCheckBoxBool;
				testBoxBool = localStringsList[0].testCheckBoxBool;
			}
		}

		protected override bool OnBackButtonPressed()
		{
			if (Device.OS == TargetPlatform.Android)
			{
				Navigation.PopToRootAsync();
			}

			return true;
		}

		protected override void OnDisappearing()
		{
			App.currentScreen = "";
			barcodeField.Unfocus();
		}

		protected async override void OnAppearing()
		{
			App.currentScreen = "ScanCycleCount";

			if (Device.OS == TargetPlatform.iOS)
			{
				//Console.WriteLine("Called");
				barcodeField.Focus();
			}

			else if (Device.OS == TargetPlatform.Android)
			{
				this.Padding = new Thickness(0, 0, 0, -30);
				await showKeypad();
			}

			showQtyPopup = App.showQtyPopup;

			reloadList();

			base.OnAppearing();

			//Console.WriteLine("values: {0}, {1}, {2}, {3}", scannerOrDeviceCheckBoxBool,qtyCheckboxBool, duplicateBarcodeCheckBoxBool, testBoxBool);
		}

		protected async Task showKeypad()
		{
			await Task.Delay(500);
			barcodeField.Focus();
		}

		//Handle entry focus
		protected void focussed(object sender, EventArgs e)
		{
			barcodeField.Text = "";

			if (Device.OS == TargetPlatform.iOS)
			{
				checkKeyboard();
			}
		}

		protected void checkKeyboard()
		{
			barcodeField.Focus();
			showQtyPopup = App.showQtyPopup;
		}

		//Handle entry completion
		protected void entry_Completed(object sender, EventArgs e)
		{
			if (barcodeField.Text != null && barcodeField.Text != String.Empty && barcodeField.Text.Trim().Length != 0)
			{
				if (Device.OS == TargetPlatform.iOS)
				{
					barcodeField.Unfocus();
				}

				string trimmedText = ((Entry)sender).Text.Replace(" ", string.Empty);
				updateListWithText(trimmedText);
			}
		}

		///<summary>
		///This method updates the database table and listview
		/// </summary>
		/// <param name="text">takes one value</param>
		protected async void updateListWithText(string text)
		{
			if (text != null && text != String.Empty && text.Trim().Length != 0)
			{
				if (scanRecordsList.Count > 0)
				{
					firstScanTime = scanRecordsList[0].firstTimeScan;
					App.firstScanTime = firstScanTime;
				}

				//rows are already present in scandatatable
				if (scanRecordsList.Count >= 0)
				{
					//test barcode checkbox value is true
					if (testBoxBool == true)
					{
						await DisplayAlert("Barcode Scanned", text, "OK");
					}
					else {

						//both quantity and duplicatebarcode checkbox values are false
						if (qtyCheckboxBool == false && duplicateBarcodeCheckBoxBool == false)
						{
							addNewRecordToScanDataTable(text);//Add new record to scan data table
						}
						//quantity checkbox value is true and duplicatebarcode checkbox value is false
						else if (qtyCheckboxBool == true && duplicateBarcodeCheckBoxBool == false)
						{
							if (showQtyPopup == true)
							{
								App.popupPageTitle = "Enter Quantity";
								var newFilePage = PopUpPage.getPopUpPageInstance();
								newFilePage.skuText = text;
								if (Device.OS == TargetPlatform.iOS)
								{
									await Navigation.PushAsync(newFilePage, true);
								}

								else if (Device.OS == TargetPlatform.Android)
								{
									await Navigation.PushAsync(newFilePage);
								}
							}
							else if (showQtyPopup == false)
							{
								addNewRecordToScanDataTable(text);
							}
						}
						//quantity checkbox value is false and duplicatebarcode checkbox value is true
						else if (qtyCheckboxBool == false && duplicateBarcodeCheckBoxBool == true)
						{
							var allSkuRecords = from str in scanRecordsList where str.sku == text orderby str.rowid select str;

							foreach (var skuRecord in allSkuRecords)
							{
								if (allSkuRecords.Contains(skuRecord))
								{
									fetchedSku = skuRecord.sku;
								}
							}

							if (text.Equals(fetchedSku))
							{
								int fetchedQuantity = 0;
								int lastrowId = 0;

								foreach (var aRecord in allSkuRecords)
								{
									if (allSkuRecords.Contains(aRecord))
									{
										fetchedQuantity = Convert.ToInt32(aRecord.quantity);
										lastrowId = aRecord.rowid;
									}
								}

								if (fetchedQuantity > 0)
								{ //Quantity Exists
									DateTime now = DateTime.Now.ToLocalTime();
									String date = now.ToString("yyMMddHHmm", DateTimeFormatInfo.InvariantInfo);

									currentTime = String.Concat(date);
									App.DatabaseRepo.AppendQuantityForOldScanRecord(fetchedQuantity + 1, currentTime, text, Convert.ToString(lastrowId), App.fileName); //Update quantity for sku
									//barcodeField.Focus();
									reloadList();
								}
								else { //Quantity Not Exists
									addNewRecordToScanDataTable(text);//Add new record to scan data table
								}
							}
							else {
								addNewRecordToScanDataTable(text);//Add new record to scan data table
							}
						}
						//both quantity and duplicatebarcode checkbox values are true
						else if (qtyCheckboxBool == true && duplicateBarcodeCheckBoxBool == true)
						{
							var allSkuRecords = from str in scanRecordsList where str.sku == text orderby str.rowid select str;

							foreach (var skuRecord in allSkuRecords)
							{
								if (allSkuRecords.Contains(skuRecord))
								{
									fetchedSku = skuRecord.sku;
								}
							}

							if (text.Equals(fetchedSku))
							{
								App.popupPageTitle = "New Quantity";
								var newFilePage = PopUpPage.getPopUpPageInstance();
								int fetchedQuantity = 0;
								int lastrowId = 0;

								foreach (var aRecord in allSkuRecords)
								{
									if (allSkuRecords.Contains(aRecord))
									{
										fetchedQuantity = Convert.ToInt32(aRecord.quantity);
										lastrowId = aRecord.rowid;
									}
								}

								if (fetchedQuantity > 0)
								{ //Quantity Exists
									if (showQtyPopup == true)
									{
										newFilePage.existingQty = Convert.ToString(fetchedQuantity);
										newFilePage.currentRowNo = Convert.ToString(lastrowId);
										newFilePage.qtyCheckboxBool = qtyCheckboxBool;
										newFilePage.duplicateBarcodeCheckBoxBool = duplicateBarcodeCheckBoxBool;
										newFilePage.skuText = text;
										if (Device.OS == TargetPlatform.iOS)
										{
											await Navigation.PushAsync(newFilePage, true);
										}

										else if (Device.OS == TargetPlatform.Android)
										{
											await Navigation.PushAsync(newFilePage);
										}
									}
									else if (showQtyPopup == false)
									{
										DateTime now = DateTime.Now.ToLocalTime();
										String date = now.ToString("yyMMddHHmm", DateTimeFormatInfo.InvariantInfo);
										currentTime = String.Concat(date);
										int newQty = fetchedQuantity + 1;
										App.DatabaseRepo.UpdateQuantityForOneScanRecord(Convert.ToString(newQty), currentTime, Convert.ToString(fetchedQuantity), text, Convert.ToString(lastrowId), App.fileName);
										//barcodeField.Focus();
										reloadList();
									}
								}
								else {
									DateTime now = DateTime.Now.ToLocalTime();
									String date = now.ToString("yyMMddHHmm", DateTimeFormatInfo.InvariantInfo);
									currentTime = String.Concat(date);
									App.DatabaseRepo.AppendQuantityForOldScanRecord(fetchedQuantity + 1, currentTime, text, Convert.ToString(lastrowId), App.fileName); //Update quantity for sku
									//barcodeField.Focus();
									reloadList();
								}
							}
							else {
								if (showQtyPopup == true)
								{
									App.popupPageTitle = "Enter Quantity";
									var newFilePage = PopUpPage.getPopUpPageInstance();
									newFilePage.skuText = text;
									if (Device.OS == TargetPlatform.iOS)
									{
										await Navigation.PushAsync(newFilePage, true);
									}

									else if (Device.OS == TargetPlatform.Android)
									{
										await Navigation.PushAsync(newFilePage);
									}
								}

								else if (showQtyPopup == false)
								{
									addNewRecordToScanDataTable(text);
								}
							}
						}
					}
				}

				barcodeField.Text = "";
			}
		}

		//Refresh listview
		protected async void reloadList()
		{
			//List<ScanDataTable> scanRecordsList = await App.DatabaseRepo.GetAllScanDataRecords(App.currentFileID);

			scanRecordsList.Clear();
			scanRecordsList = App.DatabaseRepo.GetAllScanDataRecords(App.fileName);

			if (scanRecordsList.Count > 0)
			{
				listView.ItemsSource = scanRecordsList;

				if (Device.OS == TargetPlatform.iOS)
				{
					listView.ItemTemplate = new DataTemplate(typeof(ScanCycleCountCustomCell));

					if (barcodeField.IsFocused == false)
					{
						barcodeField.Focus();
					}
				}

				else if (Device.OS == TargetPlatform.Android)
				{
					listView.SeparatorVisibility = SeparatorVisibility.Default;
					listView.SeparatorColor = Color.Black;
					await reShowKeypad();
				}
			}
		}

		protected async Task reShowKeypad()
		{
			await Task.Delay(100);
			barcodeField.Focus();
		}

		#if __IOS__
		protected async Task checkCameraAccess()
		{
			AVAuthorizationStatus authStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);
			if (authStatus == AVAuthorizationStatus.Authorized)
			{
				// do your logic
				var scanner = new ZXing.Mobile.MobileBarcodeScanner();
				var result = await scanner.Scan();

				if (result != null)
				{
					updateListWithText(result.Text);
				}
			}

			else if (authStatus == AVAuthorizationStatus.Denied)
			{
				// denied
				await DisplayAlert("Access Denied", "App needs camera permission, turn on camera option through settings", "OK");
			}

			else if (authStatus == AVAuthorizationStatus.Restricted)
			{
				// restricted, normally won't happen
				await DisplayAlert("Access Denied", "App needs camera permission, turn on camera option through settings", "OK");
			}

			else if (authStatus == AVAuthorizationStatus.NotDetermined)
			{
				AVCaptureDevice.RequestAccessForMediaType(AVMediaType.Video, async (bool isAccessGranted) =>
			   {
					//if has access              
					if (isAccessGranted)
				   {
						//do something
						var scanner = new ZXing.Mobile.MobileBarcodeScanner();
					   var result = await scanner.Scan();

					   if (result != null)
					   {
						   updateListWithText(result.Text);
					   }
				   }

					//if has no access
					else
				   {
						//show an alert
						await DisplayAlert("Access Denied", "App needs camera permission, turn on camera option through settings", "OK");
				   }
			   });
				// not determined?!
			}

			else {
				// impossible, unknown authorization status
			}
		}

#endif

		//Handle Camera button click
		protected async void cameraBtnClick(object sender, EventArgs e)
		{
#if __ANDROID__
			var status = PermissionStatus.Unknown;
			if ((int)Build.VERSION.SdkInt >= 23) {
				if (status != PermissionStatus.Granted) {
					status = (await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera))[Permission.Camera];
					if (status.ToString ().Equals ("Granted")) {
						//barcodeField.Unfocus ();					
			App.cameraClick = true;
			App.scanner = new MobileBarcodeScanner();
			App.options = new MobileBarcodeScanningOptions();
			App.options.AutoRotate = false;
			App.options.TryHarder = true;
			App.scanner.TopText = "Hold for scanning few seconds";
			App.scanner.BottomText = "Scanning...";

			var result = await App.scanner.Scan(App.options);

			if (result != null)
			{
				App.currentScreen = "ScanCycleCount";
				updateListWithText(result.Text);
			}
		} else if (status.ToString ().Equals("Denied")) {
						await DisplayAlert("Access Denied", "App needs camera permission", "OK");
	}
		}
			} else {
				//barcodeField.Unfocus ();
				App.cameraClick = true;
				App.scanner = new MobileBarcodeScanner();
				App.options = new MobileBarcodeScanningOptions();
				App.options.AutoRotate = false;
				App.options.TryHarder = true;
				App.scanner.TopText = "Hold for scanning few seconds";
				App.scanner.BottomText = "Scanning...";

				var result = await App.scanner.Scan(App.options);

				if (result != null) {
					App.currentScreen = "ScanCycleCount";
					updateListWithText(result.Text);
				}
			}
			#endif

			#if __IOS__
			int SystemVersion = Convert.ToInt16(UIDevice.CurrentDevice.SystemVersion.Split('.')[0]);
			if (SystemVersion >= 10)
			{
				CameraAccess();
			}

			else
			{
				/*var status = PermissionStatus.Unknown;
				if (status != PermissionStatus.Granted)
				{
					status = (await CrossPermissions.Current.RequestPermissions
					          Async(Permission.Camera))[Permission.Camera];
					if (status.ToString().Equals("Granted"))
					{
						var scanner = new ZXing.Mobile.MobileBarcodeScanner();
						var result = await scanner.Scan();

						if (result != null)
						{
							//barcodeField.Unfocus();
							updateListWithText(result.Text);
						}
					}

					else if (status.ToString().Equals("Denied"))
					{
						await DisplayAlert("Access Denied", "App needs camera permission, turn on camera option through settings", "OK");
					}
				}*/

				CameraAccess();
			}
			#endif
		}

		/// <summary>
		/// Camera button action.
		/// </summary>
		protected async void cameraBtnAction()
		{
			var scanner = new ZXing.Mobile.MobileBarcodeScanner();
			var result = await scanner.Scan();

			if (result != null)
			{
				//barcodeField.Unfocus();
				updateListWithText(result.Text);
			}
		}
	
		/// <summary>
		/// Checks the camera access.
		/// </summary>
		protected async void CameraAccess()
		{
			AVAuthorizationStatus authStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);
			if (authStatus == AVAuthorizationStatus.Authorized)
			{
				cameraBtnAction();
			}

			else if (authStatus == AVAuthorizationStatus.Denied)
			{
				// denied
				await DisplayAlert("Access Denied", "App needs camera permission, turn on camera option through settings", "OK");
			}

			else if (authStatus == AVAuthorizationStatus.Restricted)
			{
				// restricted, normally won't happen
				await DisplayAlert("Access Denied", "App needs camera permission, turn on camera option through settings", "OK");
			}

			else if (authStatus == AVAuthorizationStatus.NotDetermined)
			{
				//do something
				cameraBtnAction();
			}

			else {
				// impossible, unknown authorization status
			}
		}

		//Add a record to scandatatable
		protected void addNewRecordToScanDataTable(string text)
		{
			DateTime now = DateTime.Now.ToLocalTime();
			String date = now.ToString("yyMMddHHmm", DateTimeFormatInfo.InvariantInfo);
			currentTime = String.Concat(date);
			App.DatabaseRepo.AddNewScanDataRecord("1", text, currentTime, currentTime, App.fileName);
			//barcodeField.Focus();
			reloadList();
		}

		//Handle row selection
		void onItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
			{
				return;
			}

			this.showActionSheet(sender, e);
			var items = (ScanDataTable)e.SelectedItem;

			// Deselect row
			listView.SelectedItem = null;
		}

		//Show and handle ActionSheet
		protected async void showActionSheet(object sender, SelectedItemChangedEventArgs e)
		{
			var action = await DisplayActionSheet("Choose One", "Cancel", null, "Change Quantity", "Delete Record");

			switch (action)
			{
				case "Change Quantity":
					checkKeyboard();
					if (showQtyPopup == false)
					{
						await DisplayAlert("Cannot Change Quantity", "Turn off bluetooth to change the quantity", "OK");
					}
					else if (showQtyPopup == true)
					{
						App.popupPageTitle = "New Quantity";
						var detailPage = PopUpPage.getPopUpPageInstance();
						selectedQuantity = (e.SelectedItem as ScanDataTable).quantity;
						selectedSku = (e.SelectedItem as ScanDataTable).sku;
						detailPage.qtyCheckboxBool = qtyCheckboxBool;
						detailPage.duplicateBarcodeCheckBoxBool = duplicateBarcodeCheckBoxBool;
						detailPage.qty = selectedQuantity;
						detailPage.currentRowNo = (e.SelectedItem as ScanDataTable).rowid.ToString();
						detailPage.skuText = selectedSku;

						if (Device.OS == TargetPlatform.iOS)
						{
							await Navigation.PushAsync(detailPage, true);
						}

						else if (Device.OS == TargetPlatform.Android)
						{
							await Navigation.PushAsync(detailPage);
						}
					}

					break;

				case "Delete Record":
					selectedQuantity = (e.SelectedItem as ScanDataTable).quantity;
					selectedSku = (e.SelectedItem as ScanDataTable).sku;
					App.DatabaseRepo.DeleteOneScanRecord((e.SelectedItem as ScanDataTable).rowid.ToString());
					//List<ScanDataTable> scanRecordsList = await App.DatabaseRepo.GetAllScanDataRecords(App.currentFileID);
					//listView.ItemsSource = null;
					//listView.ItemsSource = DataSource.GetList(scanRecordsList);
					scanRecordsList.Clear();
					scanRecordsList = App.DatabaseRepo.GetAllScanDataRecords(App.fileName);
					listView.ItemsSource = scanRecordsList;
					listView.ItemTemplate = new DataTemplate(typeof(ScanCycleCountCustomCell));
					break;
			}
		}
	}
}