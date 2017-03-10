using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace TallySoftShared
{
    /// <summary>
    /// Menu page
    /// </summary>
	public partial class MenuPage : ContentPage
	{
		public ListView Menu { get; set; } // listview property

		public MenuPage ()
		{
			Icon = "nav_bar.png";
			Title = "menu"; // The Title property must be set.
			BackgroundColor = Color.FromHex ("EBEAEA");

			Menu = new MenuListView ();

			var menuLabel = new ContentView {
				Padding = new Thickness (10, 10, 0, 5),
				Content = new Label {
					TextColor = Color.FromHex ("000000"),
				}
			};

			var layout = new StackLayout { 
				Spacing = 0, 
				WidthRequest = this.WidthRequest,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			if (Device.OS == TargetPlatform.iOS) {
				layout.Children.Add (menuLabel);
			}

			layout.Children.Add (Menu);
			Content = layout;
		}
	}
}