using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace TallySoftShared
{
    /// <summary>
    /// Menu list view
    /// </summary>
	public class MenuListView : ListView
	{
		public MenuListView()
		{
			App.data = new System.Collections.ObjectModel.ObservableCollection<List<SliderMenuItem>>();
			App.data.Add(new MenuListData());
			ItemsSource = App.data[0];
			VerticalOptions = LayoutOptions.StartAndExpand;
			BackgroundColor = Color.FromHex("EBEBEB");
			SeparatorVisibility = SeparatorVisibility.None;

			var cell = new DataTemplate(typeof(MenuCell));
			ItemTemplate = cell;
			ItemSelected += MenuListView_ItemSelected;
		}

        /// <summary>
        /// Menulistview item selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void MenuListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var items = (SliderMenuItem)e.SelectedItem;
			if (e.SelectedItem != null)
			{
				var d = App.data[0].FindIndex(r => r.Title == items.Title);

				for (int i = 0; i < App.data[0].Count; i++)
				{
					if (d == i)
					{
						if (App.data[0][i].Title == "Save" || App.data[0][i].Title == "Save & Email" || App.data[0][i].Title == "Save & End")
						{
							App.data[0][0].TitleColor = Color.FromHex("1866B0");
							App.data[0][0].Active = true;


							if (App.data[0][0].Title == "Main Menu" && App.data[0][0].Active == true)
							{
								App.data[0][0].IconSource = "main_menu_active.png";
							}

							else if (App.data[0][0].Title == "Main Menu" && App.data[0][0].Active == false)
							{
								App.data[0][0].IconSource = "main_menu.png";
							}
						}

						else
						{
							App.data[0][i].TitleColor = Color.FromHex("1866B0");
							App.data[0][i].Active = true;
						}
					}

					else
					{
						App.data[0][i].TitleColor = Color.FromHex("000000");
						App.data[0][i].Active = false;
					}

					if (App.data[0][i].Title == "Main Menu" && App.data[0][i].Active == true)
					{
						App.data[0][i].IconSource = "main_menu_active.png";
					}

					else if (App.data[0][i].Title == "Main Menu" && App.data[0][i].Active == false)
					{
						App.data[0][i].IconSource = "main_menu.png";
					}

					if (App.data[0][i].Title == "Help" && App.data[0][i].Active == true)
					{
						App.data[0][i].IconSource = "help_active.png";
					}

					else if (App.data[0][i].Title == "Help" && App.data[0][i].Active == false)
					{
						App.data[0][i].IconSource = "help.png";
					}
				}
			}
		}
	}
}