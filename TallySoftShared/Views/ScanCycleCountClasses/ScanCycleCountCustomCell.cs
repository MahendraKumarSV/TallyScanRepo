using Xamarin.Forms;
using System;
using Plugin.Messaging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// ScanCycleCountCustomCell
/// </summary>
public class ScanCycleCountCustomCell : ViewCell
{
    /// <summary>
    /// ScanCycleCountCustomCell constructor
    /// </summary>
	public ScanCycleCountCustomCell()
	{
		var qtyLabel = new Label() //quantity label
		{
			HorizontalOptions = LayoutOptions.Start,
			FontSize = 15,
			TranslationX = -25,
			WidthRequest = 100,
			TextColor = Color.Black
		};

		qtyLabel.SetBinding(Label.TextProperty, "quantity"); //binding quantity property

		var skuLabel = new Label() //stock keeping unit label
		{
			TranslationX = 8,
			FontSize = 15,
			WidthRequest = TallySoftShared.App.ScreenWidth < 321 ? 150 : 180,
			LineBreakMode = LineBreakMode.CharacterWrap,
			TextColor = Color.Black,
		};

		skuLabel.SetBinding(Label.TextProperty, "sku"); //sku proeprty binding

		var cellLayout = new StackLayout
		{
			Padding = new Thickness(0, 0, 0, 0),
			TranslationX = 35,
			Orientation = StackOrientation.Horizontal,
			Children = { qtyLabel, skuLabel }
		};

		var box = new BoxView()
		{
			Color = Color.Black,
			HeightRequest = 1,
			HorizontalOptions = LayoutOptions.FillAndExpand,
			TranslationY = 10,
			Opacity = 0.5
		};

		var cellLayout1 = new StackLayout
		{
			Spacing = 10,
			Orientation = StackOrientation.Vertical,
			Padding = 10,
			HorizontalOptions = LayoutOptions.FillAndExpand,
			Children = { cellLayout, box }
		};
		this.View = cellLayout1;
	}
}