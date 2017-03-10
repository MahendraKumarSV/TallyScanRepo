using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using SQLite.Net.Platform.XamarinAndroid;
using Android;
using Plugin.Permissions;
using Android.Bluetooth;
using System.Threading.Tasks;
using Android.Content.Res;
using Java.Util;
using System.IO;
using System.Collections.Generic;
using Android.Views;
using Android.Content;
using ZXing.Mobile;
using Android.Views.InputMethods;
using Xamarin.Forms.Platform.Android;

namespace TallySoftShared.Droid
{
	[Activity(Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden, ScreenOrientation = ScreenOrientation.Portrait)]

	public class MainActivity : FormsApplicationActivity
	{
		BroadcastReceiver receiver = null;

		protected override void OnCreate(Bundle bundle)
		{
			FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;

			base.OnCreate(bundle);

			//ZXing.Net.Mobile.Forms.Android.Platform.Init();
			MobileBarcodeScanner.Initialize(Application);

			App.ScreenWidth = (int)Resources.DisplayMetrics.WidthPixels; // real pixels
			App.ScreenHeight = (int)Resources.DisplayMetrics.HeightPixels; // real pixels

			App.ScreenWidth = (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density); // device independent pixels
			App.ScreenHeight = (int)(Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density); // device independent pixels

			ActionBar.SetIcon(null);
			//SupportActionBar.SetIcon(null);
			//ActionBar.SetDisplayHomeAsUpEnabled(true);
			//ActionBar.SetHomeAsUpIndicator(Resource.Drawable.back);
			global::Xamarin.Forms.Forms.Init(this, bundle);

			//string dbPath = FileAccessHelper.GetLocalFilePath("FileDataAndStrings.sqlite");

			//LoadApplication(new App(dbPath, new SQLitePlatformAndroid()));
			LoadApplication(new App());
			App.showQtyPopup = true;

			//getAllPairedDevices();
		}

		public override void OnWindowFocusChanged(bool hasFocus)
		{
			base.OnWindowFocusChanged(hasFocus);

			/*if (App.currentScreen != null)
			{
				if (App.currentScreen.Equals("ScanCycleCount") || App.currentScreen.Equals("PopUpPage"))
				{
					
				}
			}*/

			if (!hasFocus)
			{
				var inputMethodManager = (InputMethodManager)this.GetSystemService(InputMethodService);
				var token = this.CurrentFocus?.WindowToken;
				inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);
				this.Window.DecorView.ClearFocus();
			}
		}

		public override void OnConfigurationChanged(Configuration newConfig)
		{
			base.OnConfigurationChanged(newConfig);
			if (newConfig.HardKeyboardHidden == HardKeyboardHidden.No)
			{
				//((InputMethodManager)this.GetSystemService(Context.InputMethodService)).ShowInputMethodPicker();
				Console.WriteLine("Barcode Scanner detected. Please turn OFF Hardware/Physical keyboard to enable softkeyboard to function.");
				App.showQtyPopup = false;
			}

			else if (newConfig.HardKeyboardHidden == HardKeyboardHidden.Yes)
			{
				Console.WriteLine("Barcode Scanner disconnected");
				App.showQtyPopup = true;
			}
		}

		[BroadcastReceiver]
		//[IntentFilter(new[] { Android.Provider.Settings.ActionBluetoothSettings }, Priority = (int)IntentFilterPriority.HighPriority)]
		[IntentFilter(new[] { Android.Bluetooth.BluetoothAdapter.ActionStateChanged }, Priority = (int)IntentFilterPriority.HighPriority)]
		public class Receiver : BroadcastReceiver
		{
			public override void OnReceive(Context context, Intent intent)
			{
				String action = intent.Action;
				//BluetoothDevice device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);

				if (BluetoothDevice.ActionFound.Equals(action))
				{
					//Device found
					Console.WriteLine("ActionFound");
				}
				else if (BluetoothDevice.ActionAclConnected.Equals(action))
				{
					//Device is now connected
					Console.WriteLine("ActionAclConnected");
				}
				else if (BluetoothAdapter.ActionDiscoveryFinished.Equals(action))
				{
					//Done searching
					Console.WriteLine("ActionDiscoveryFinished");
				}
				else if (BluetoothDevice.ActionAclDisconnectRequested.Equals(action))
				{
					//Device is about to disconnect
					Console.WriteLine("ActionAclDisconnectRequested");
				}
				else if (BluetoothDevice.ActionAclDisconnected.Equals(action))
				{
					//Device has disconnected
					Console.WriteLine("ActionAclDisconnected");
				}
			}
		};

		public void getAllPairedDevices()
		{
			BluetoothAdapter btAdapter = BluetoothAdapter.DefaultAdapter;

			var devices = btAdapter.BondedDevices;
			if (devices != null && devices.Count > 0)
			{
				foreach (BluetoothDevice mDevice in devices)
				{
					//mDevice.Name.Split(' ');
					Console.WriteLine("Device Name: {0}", mDevice.Name);
					Console.WriteLine("State: {0}", mDevice.BondState);
				}
			}
		}

		protected override void OnPause()
		{
			base.OnPause();
		}

		protected override void OnStop()
		{
			base.OnStop();

			if(receiver != null)
				this.UnregisterReceiver(receiver);
		}

		protected override void OnResume()
		{
			base.OnResume();

			IntentFilter filter1 = new IntentFilter(BluetoothDevice.ActionAclConnected);
			IntentFilter filter2 = new IntentFilter(BluetoothDevice.ActionAclDisconnectRequested);
			IntentFilter filter3 = new IntentFilter(BluetoothDevice.ActionAclDisconnected);

			this.RegisterReceiver(receiver, filter1);
			this.RegisterReceiver(receiver, filter2);
			this.RegisterReceiver(receiver, filter3);
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			//ZXing.Net.Mobile.Forms.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}