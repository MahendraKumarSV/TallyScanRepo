using TallySoftShared;
using TallySoftShared.iOS;
using Foundation;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomCell), typeof(FileManageriOSCellRenderer))]

namespace TallySoftShared.iOS
{
	public class FileManageriOSCellRenderer : ViewCellRenderer
	{
		static NSString rid = new NSString("CustomCell");

		public override UITableViewCell GetCell(Xamarin.Forms.Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var x = (CustomCell)item;

			FileManageriOSCell c = reusableCell as FileManageriOSCell;

			if (c == null)
			{
				c = new FileManageriOSCell(rid);
			}

			c.UpdateCell(x.ImageFilename ,x.Name, x.ImageVisibility);

			return c;
		}
	}
}
