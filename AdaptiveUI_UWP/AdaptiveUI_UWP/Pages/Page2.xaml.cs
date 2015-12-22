using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;

namespace AdaptiveUI_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page2 : Page
    {
        public Page2()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var result = await TheContentDialog.ShowAsync();
            Debug.WriteLine(result);
        }
    }
}
