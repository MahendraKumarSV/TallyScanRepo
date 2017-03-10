using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Proximity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TallyScan.UniversalApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        
        public MainPage()
        {
            this.InitializeComponent();   
            //StatusBar for Mobile
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                Windows.UI.ViewManagement.StatusBar.GetForCurrentView().BackgroundColor = Windows.UI.Colors.White;
                //Windows.UI.Color.FromArgb(1, 24, 102, 176);
                Windows.UI.ViewManagement.StatusBar.GetForCurrentView().BackgroundOpacity = 1;
                Windows.UI.ViewManagement.StatusBar.GetForCurrentView().ForegroundColor = Windows.UI.Colors.Black;
            }            
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            Windows.Graphics.Display.DisplayInformation.AutoRotationPreferences = Windows.Graphics.Display.DisplayOrientations.Portrait;
            ZXing.Net.Mobile.Forms.WindowsUniversal.ZXingScannerViewRenderer.Init();            
            TallySoftShared.App.ScreenWidth = (int)Window.Current.Bounds.Width; // real pixels
            TallySoftShared.App.ScreenHeight = (int)Window.Current.Bounds.Height; // real pixels
            //string dbPath = FileAccessHelper.GetLocalFilePath("FileDataAndStrings.sqlite");
            //LoadApplication(new TallySoftShared.App(dbPath, new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT()));
            LoadApplication(new TallySoftShared.App());
        }


    }
}
