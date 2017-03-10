using Xamarin.Forms;
[assembly: Xamarin.Forms.Platform.UWP.ExportRenderer(typeof(TallySoftShared.NativeCell), typeof(TallyScan.UniversalApp.NativeUWPCellRenderer))]
namespace TallyScan.UniversalApp
{
    public class NativeUWPCellRenderer : Xamarin.Forms.Platform.UWP.ViewCellRenderer
    {
        public override Windows.UI.Xaml.DataTemplate GetTemplate(Cell cell)
        {
            return App.Current.Resources["ListViewItemTemplate"] as Windows.UI.Xaml.DataTemplate;
        }
    }
}
