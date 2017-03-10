using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using TallySoftShared;
using TallySoftShared.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer (typeof(NativeCell), typeof(NativeAndroidCellRenderer))]
namespace TallySoftShared.Droid
{
	/// <summary>
	/// This renderer uses a view defined in /Resources/Layout/NativeAndroidCell.axml
	/// as the cell layout
	/// </summary>
	public class NativeAndroidCellRenderer : ViewCellRenderer
	{
		protected override Android.Views.View GetCellCore (Xamarin.Forms.Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
		{
			var x = (NativeCell)item;

			var view = convertView;

			if (view == null) { 
				// no view to re-use, create new
				view = (context as Activity).LayoutInflater.Inflate (Resource.Layout.NativeAndroidCell, null);
			}

			view.FindViewById<TextView> (Resource.Id.Text1).Text = x.Quantity;
			view.FindViewById<TextView> (Resource.Id.Text2).Text = x.Sku;

			return view;
		}
	}
}
