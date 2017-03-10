using TallyScan.UniversalApp;
using TallySoftShared;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;


[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace TallyScan.UniversalApp
{


    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                //Control.AcceptsReturn = false;
                //Control.KeyDown += Control_KeyDown;                
            }
        }
        //private void Control_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        //{
        //    if (e.Key == Windows.System.VirtualKey.Enter)
        //    {
        //        e.Handled = false;
        //    }
        //}

        //private void Control_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        //{
        //    //var controll = (Xamarin.Forms.Platform.UWP.FormsTextBox)sender;
        //    //Control.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Transparent);
        //}

        //private void Control_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        //{
        //    var controll = (Xamarin.Forms.Platform.UWP.FormsTextBox)sender;
        //    Control.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Transparent);
        //}

        //private void Control_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        //{
        //    var controll = (Xamarin.Forms.Platform.UWP.FormsTextBox)sender;
        //    Control.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Transparent);
        //}

        //private void Control_GotFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        //{
        //    var controll = (Xamarin.Forms.Platform.UWP.FormsTextBox)sender;
        //    //Control.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Transparent);
        //}
    }
}

