using Xamarin.Forms;

public class MenuCell : ViewCell
{
    /// <summary>
    /// Menu Cell 
    /// </summary>
	public MenuCell() : base()
	{
		var menuicon = new Image // menu icon
		{
			HeightRequest = 30,
			WidthRequest = 30,
			Aspect = Aspect.AspectFit,
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center,
		};
        menuicon.SetBinding(Image.SourceProperty, "IconSource", BindingMode.TwoWay);
		var nameLabel = new Label() // Menu label
		{
			FontSize = 15,
		};
		nameLabel.SetBinding(Label.TextProperty, "Title");
		nameLabel.SetBinding(Label.TextColorProperty, "TitleColor",BindingMode.TwoWay); // title color
		var vetDetailsLayout = new StackLayout
		{            
			Padding = new Thickness(10, 5, 0, 0),
			Spacing = 0,
			HorizontalOptions = LayoutOptions.StartAndExpand,
			Children = { nameLabel }
		};

		var box = new BoxView() // boxview for label and icon
		{         
			Color = Color.Black,            
			HeightRequest = 1,
			HorizontalOptions = LayoutOptions.FillAndExpand,
			Opacity = 0.5
		};

		var cellLayout = new StackLayout // cell area
		{
			BackgroundColor=Color.Transparent,
			Spacing = 0,
			Padding = new Thickness(10,5,0,5),
			Orientation = StackOrientation.Horizontal,
			HorizontalOptions = LayoutOptions.FillAndExpand,
			Children = { menuicon, vetDetailsLayout }
		};

		var cellLayout1 = new StackLayout //all menus area
		{
			Spacing = 0,            
			Orientation = StackOrientation.Vertical,
			HorizontalOptions = LayoutOptions.FillAndExpand,
			Children = { cellLayout, box }
		};
		this.View = cellLayout1;
	}
}