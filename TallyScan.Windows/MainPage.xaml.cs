using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace TallyScan.Windows8_1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage        
    {
        public MainPage()
        {
            this.InitializeComponent();
          
            this.NavigationCacheMode = NavigationCacheMode.Required;
            TallySoftShared.App.ScreenWidth = (int)Window.Current.Bounds.Width; // real pixels
            TallySoftShared.App.ScreenHeight = (int)Window.Current.Bounds.Height; // real pixels

            string dbPath = FileAccessHelper.GetLocalFilePath("FileDataAndStrings.sqlite");
           LoadApplication(new TallySoftShared.App(dbPath, new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT()));
            
        }
       
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }
       
    }
}
