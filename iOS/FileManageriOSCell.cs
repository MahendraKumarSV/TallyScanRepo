using Foundation;
using UIKit;

namespace TallySoftShared.iOS
{
	/// <summary>
	/// Sample of a custom cell layout, taken from the iOS docs at:
	/// http://developer.xamarin.com/guides/ios/user_interface/tables/part_3_-_customizing_a_table's_appearance/
	/// </summary>
	public class FileManageriOSCell : UITableViewCell
	{
		UILabel fileNameLabel;
		//UILabel separatorLabel;
		UIImageView imageView;

		public FileManageriOSCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;

			imageView = new UIImageView();
			//imageView.Image = UIImage.FromFile({ Binding ImageFilename});

			fileNameLabel = new UILabel()
			{
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear
			};

			/*separatorLabel = new UILabel()
			{
				BackgroundColor = UIColor.Black
			};*/

			ContentView.Add(imageView);
			ContentView.Add(fileNameLabel);
		}

		public void UpdateCell(string imageFileName, string filename, bool visible)
		{
			imageView.Hidden = visible;
			imageView.Image = UIImage.FromFile(imageFileName);
			fileNameLabel.Text = filename;
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			imageView.Frame = new CoreGraphics.CGRect(10, 5, 22, 22);
			fileNameLabel.Frame = new CoreGraphics.CGRect(50, 5, 120, 20);
			//separatorLabel.Frame = new CoreGraphics.CGRect(90, 0, 2, 43);
		}
	}
}