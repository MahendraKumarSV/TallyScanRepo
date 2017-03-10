using System;
using Xamarin.Forms;
using System.Threading.Tasks;

#if __IOS__
using UIKit;
#endif

#if __ANDROID__
using Android.Views.InputMethods;
using Android.App;
using Android.Bluetooth;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Java.Util;
using Android.OS;
#endif

namespace TallySoftShared
{
    /// <summary>
    /// roote page for tally scan (master page)
    /// </summary>
	public class RootPage : MasterDetailPage
	{
		MenuPage menuPage;

		public RootPage ()
		{
			menuPage = new MenuPage ();

			if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android) {
				this.IsGestureEnabled = false;
			}

			menuPage.Menu.ItemSelected += (sender, e) => NavigateTo (e.SelectedItem as SliderMenuItem);

			Master = menuPage;

			this.IsPresentedChanged += (sender, e) =>
			{
				#if __IOS__
					if (App.currentScreen.Equals("ScanCycleCount"))
					{
						DismissKeyboardAsync();
					}
				#endif

				#if __ANDROID__
					if (App.currentScreen.Equals("ScanCycleCount"))
					{
						
					}
					HideKeyboard();
				#endif
			};

			#if WINDOWS_UWP
				var np = new NavigationPage(new MainMenu());
            	np.BarBackgroundColor = Color.FromHex("#1866B0");
            	np.BarTextColor = Color.White;
            	np.BackgroundColor = Color.White;
			#endif

			Detail = new NavigationPage (new MainMenu());
		}

		#if __IOS__
			public Task<bool> DismissKeyboardAsync()
			{
				TaskCompletionSource<bool> result = new TaskCompletionSource<bool>();
				try
				{
					bool dismissalResult = UIApplication.SharedApplication.KeyWindow.EndEditing(true);
					result.SetResult(dismissalResult);
				}
				catch (Exception exception)
				{
					Console.WriteLine("\nIn NativeHelper_iOS.DismissKeyboardAsync() - Exception:\n{0}\n", exception);
					result.SetResult(false);
				}

				return result.Task;
			}
		#endif

		#if __ANDROID__
			public static void HideKeyboard()
			{
				var context = Forms.Context;
				var inputMethodManager = (InputMethodManager)Forms.Context.GetSystemService(Android.Content.Context.InputMethodService);
				if (inputMethodManager != null && context is Activity)
				{
					var activity = context as Activity;
					var token = activity.CurrentFocus?.WindowToken;
					inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);
					activity.Window.DecorView.ClearFocus();
				}
			}
		#endif

		protected override void OnAppearing()
		{
			NavigationPage.SetHasNavigationBar(this, true);
			NavigationPage.SetHasBackButton(this, false);

			base.OnAppearing();
		}

        /// <summary>
        /// Navigate to method
        /// </summary>
        /// <param name="menu"></param>
		void NavigateTo (SliderMenuItem menu)
		{
			if (menu == null)
				return;

#if WINDOWS_UWP
            np.BarBackgroundColor = Color.FromHex("#1866B0");
            np.BarTextColor = Color.White;
            np.BackgroundColor = Color.White;
            np.WidthRequest = 100;
#endif
			Detail = new NavigationPage ((Page)Activator.CreateInstance (menu.TargetType));
			this.IsPresented = false;
			menuPage.Menu.SelectedItem = null;
		}
	}
}