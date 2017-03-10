
using TallyScan.Windows8_1;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinRT;



[assembly:ExportRenderer(typeof(TallySoftShared.NativeCell), typeof(NativeWinPhoneCellRenderer))]
namespace TallyScan.Windows8_1
{
    public class NativeWinPhoneCellRenderer : ViewCellRenderer
    {
        public override Windows.UI.Xaml.DataTemplate GetTemplate(Cell cell)
        {
            return App.Current.Resources["ListViewItemTemplate"] as Windows.UI.Xaml.DataTemplate;
        }
    }
}
