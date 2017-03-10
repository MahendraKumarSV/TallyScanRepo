using Foundation;
using UIKit;
using SQLite.Net.Platform.XamarinIOS;
using Xamarin.Forms;
using System;
using System.IO;
using System.Collections.Generic;
using TallySoftShared.Model;

namespace TallySoftShared.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			App.ScreenWidth = (int)UIScreen.MainScreen.Bounds.Width; // Get device frame width
			App.ScreenHeight = (int)UIScreen.MainScreen.Bounds.Height; // Get device frame height

			UINavigationBar.Appearance.TintColor = UIColor.White;
			UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(25,102,175);
			UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = UIColor.White });
			UINavigationBar.Appearance.BackIndicatorImage = UIImage.FromFile("back.png");
			UINavigationBar.Appearance.BackIndicatorTransitionMaskImage = UIImage.FromFile("back.png");
			UIBarButtonItem.AppearanceWhenContainedIn (typeof(UINavigationBar)).TintColor = UIColor.White;

			UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);

			global::Xamarin.Forms.Forms.Init (); // Init Xamarin forms

			ZXing.Net.Mobile.Forms.iOS.Platform.Init(); // Init zxing

			Rg.Plugins.Popup.IOS.Popup.Init(); // Init Popup

			//string dbPath = FileAccessHelper.GetLocalFilePath ("FileDataAndStrings.sqlite"); // Get database path
			//LoadApplication (new App (dbPath, new SQLitePlatformIOS ())); // pass database path to main class
			LoadApplication(new App());

			App.showQtyPopup = true;
			return base.FinishedLaunching (app, options);
		}

		public override void WillTerminate(UIApplication app)
		{
			
		}
	}
}