using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using TallySoftShared;
using TallySoftShared.Droid;
using Android.Text;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace TallySoftShared.Droid
{
	class MyEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
				Control.InputType = InputTypes.TextFlagNoSuggestions;
				//Control.ImeOptions = Android.Views.InputMethods.ImeAction.Next;
			}
		}
	}
}