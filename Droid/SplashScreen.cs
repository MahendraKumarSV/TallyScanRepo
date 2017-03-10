using Android.App;
using Android.Content.PM;
using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;

namespace TallySoftShared.Droid
{
	[Activity (Theme = "@style/MyTheme.Splash", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]

	public class SplashScreen : AppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
		}

		protected override void OnResume()
		{
			base.OnResume();

			Task startupWork = new Task(() =>
				{
					Task.Delay(5000); // Simulate a bit of startup work.
				});

			startupWork.ContinueWith(t =>
				{
					StartActivity(new Intent(Application.Context, typeof (MainActivity)));
					Finish();
				}, TaskScheduler.FromCurrentSynchronizationContext());

			startupWork.Start();
		}
	}
}
