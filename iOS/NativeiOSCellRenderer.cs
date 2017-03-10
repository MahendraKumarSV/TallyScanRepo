using TallySoftShared;
using TallySoftShared.iOS;
using Foundation;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer (typeof(NativeCell), typeof(NativeiOSCellRenderer))]
namespace TallySoftShared.iOS
{
	public class NativeiOSCellRenderer : ViewCellRenderer
	{
		static NSString rid = new NSString ("NativeCell");

		public override UITableViewCell GetCell (Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var x = (NativeCell)item;

			NativeiOSCell c = reusableCell as NativeiOSCell;

			if (c == null) {
				c = new NativeiOSCell (rid);
			}

			c.UpdateCell (x.Name, x.Category);

			return c;
		}
	}
}
