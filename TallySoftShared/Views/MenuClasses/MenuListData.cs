using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace TallySoftShared
{
	public class MenuListData : List<SliderMenuItem>
	{
		public MenuListData ()
		{
			this.Add (new SliderMenuItem () { 
				Title = "Main Menu", 
				Active=true,
				TitleColor= Color.FromHex("1866B0"),
				IconSource = "main_menu_active.png",
				TargetType = typeof(MainMenu)
			});

			this.Add (new SliderMenuItem () { 
				Title = "Save",
				Active = false,
				TitleColor = Color.FromHex("000000"),
				IconSource = "save.png",
				TargetType = typeof(SaveFile)
			});
					
			this.Add (new SliderMenuItem () {
				Title = "Save & Email",
				Active = false,
				TitleColor = Color.FromHex("000000"),
				IconSource = "mail.png",
				TargetType = typeof(Save_Email)
			});

			this.Add (new SliderMenuItem () {
				Title = "Save & End",
				Active = false,
				TitleColor = Color.FromHex("000000"),
				IconSource = "save_end.png",
				TargetType = typeof(Save_End)
			});

			this.Add (new SliderMenuItem () {
				Title = "Help",
				Active = false,
				TitleColor = Color.FromHex("000000"),
				IconSource = "help.png",
				TargetType = typeof(HelpPage)
			});
		}
	}
}