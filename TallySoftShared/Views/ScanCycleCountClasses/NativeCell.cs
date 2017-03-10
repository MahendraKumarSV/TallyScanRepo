using System;
using Xamarin.Forms;

namespace TallySoftShared
{
	public class NativeCell : ViewCell
	{
		public static readonly BindableProperty QuantityProperty = BindableProperty.Create ("quantity", typeof(string), typeof(NativeCell), "");

		public string Quantity {
			get { return (string)GetValue (QuantityProperty); }
			set { SetValue (QuantityProperty, value); }
		}

		public static readonly BindableProperty SkuProperty = BindableProperty.Create ("sku", typeof(string), typeof(NativeCell), "");

		public string Sku {
			get { return (string)GetValue (SkuProperty); }
			set { SetValue (SkuProperty, value); }
		}
	}
}