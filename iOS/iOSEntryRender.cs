using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using TallySoftShared.iOS;
using TallySoftShared;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace TallySoftShared.iOS
{
	public class MyEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				// do whatever you want to the UITextField here!
				//Control.BackgroundColor = UIColor.FromRGB(204, 153, 255);
				Control.ReturnKeyType = UIReturnKeyType.Done;
				Control.AutocorrectionType = UITextAutocorrectionType.No;
				Control.SpellCheckingType = UITextSpellCheckingType.No;
				Control.AutocapitalizationType = UITextAutocapitalizationType.None;
				Control.Layer.BorderWidth = (float)1.0;
				Control.Layer.BorderColor = UIColor.LightGray.CGColor;
				Control.Layer.CornerRadius = (float)5.0;
			}
		}
	}
}


