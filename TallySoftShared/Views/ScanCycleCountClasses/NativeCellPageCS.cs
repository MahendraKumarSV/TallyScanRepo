using Xamarin.Forms;

namespace TallySoftShared
{
	public class NativeCellPageCS : ContentPage
	{
		ListView listView;

		public NativeCellPageCS ()
		{
			listView = new ListView {
				ItemsSource = DataSource.GetList (),
				ItemTemplate = new DataTemplate (() => {
					var nativeCell = new NativeCell ();
					nativeCell.SetBinding (NativeCell.QuantityProperty, "quantity");
					nativeCell.SetBinding (NativeCell.SkuProperty, "sku");
 
					return nativeCell;
				})
			};

			Padding = new Thickness (0, Device.OnPlatform (20, 0, 0), 0, 0);
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Xamarin.Forms native cell", HorizontalTextAlignment = TextAlignment.Center },
					listView
				}
			};
					
			listView.ItemSelected += OnItemSelected;
		}

		void OnItemSelected (object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null) {
				return;
			}

			// Deselect row
			listView.SelectedItem = null;
		}
	}
}
