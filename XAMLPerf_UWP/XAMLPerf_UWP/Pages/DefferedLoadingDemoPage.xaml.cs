using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;

namespace XAMLPerf_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DefferedLoadingDemoPage : Page
    {
        public DefferedLoadingDemoPage()
        {
            this.InitializeComponent();

            Loaded += DefferedLoadingDemoPage_Loaded;
        }

        private void DefferedLoadingDemoPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Debug.WriteLine("Page loaded");
            FindName("my3");
            my3.Width = 300;
        }

        private void my5_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            FindName("my2");
            my2.Width = 200;
            //my4.Width = 50;
        }
    }
}
