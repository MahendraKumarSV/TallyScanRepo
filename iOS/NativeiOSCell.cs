using Foundation;
using UIKit;

namespace TallySoftShared.iOS
{
	/// <summary>
	/// Sample of a custom cell layout, taken from the iOS docs at:
	/// http://developer.xamarin.com/guides/ios/user_interface/tables/part_3_-_customizing_a_table's_appearance/
	/// </summary>
	public class NativeiOSCell : UITableViewCell
	{
		UILabel quantityLabel, separatorLabel, skuLabel;

		public NativeiOSCell (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;

			//ContentView.BackgroundColor = UIColor.FromRGB (255, 255, 224);
			//ContentView.Layer.BorderWidth = (float)1.0;
			//ContentView.Layer.BorderColor = UIColor.Black.CGColor;

			quantityLabel = new UILabel () {
				//Font = UIFont.FromName ("Cochin-BoldItalic", 22f),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear
			};

			separatorLabel = new UILabel () {
				BackgroundColor = UIColor.Black
			};

			skuLabel = new UILabel () {
				//Font = UIFont.FromName ("AmericanTypewriter", 12f),
				TextColor = UIColor.Black,
				TextAlignment = UITextAlignment.Center,
				BackgroundColor = UIColor.Clear,
				MinimumFontSize = 15,
				Lines = 3
			};

			ContentView.Add (quantityLabel);
			//ContentView.Add (separatorLabel);
			ContentView.Add (skuLabel);
		}

		public void UpdateCell (string quantity, string sku)
		{
			quantityLabel.Text = quantity;
			skuLabel.Text = sku;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			quantityLabel.Frame = new CoreGraphics.CGRect (30, 22, 120, 20);
			separatorLabel.Frame = new CoreGraphics.CGRect (90, 0, 2, 43);
			skuLabel.Frame = new CoreGraphics.CGRect(95, 5, 200, 60);
		}
	}
}
