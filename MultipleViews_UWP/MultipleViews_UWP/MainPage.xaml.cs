using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MultipleViews_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var view = CoreApplication.CreateNewView();
            int newViewId = 0;
            await view.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var frame = new Frame();
                frame.Navigate(typeof(SecondaryViewPage), null);
                Window.Current.Content = frame;

                newViewId = ApplicationView.GetForCurrentView().Id;
                Window.Current.Activate();
            });

            await ApplicationViewSwitcher.SwitchAsync(newViewId);
        }

        private async void Button1_Click(object sender, RoutedEventArgs e)
        {
            var view = CoreApplication.CreateNewView();
            int newViewId = 0;
            await view.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var frame = new Frame();
                frame.Navigate(typeof(SecondaryViewPage), null);
                Window.Current.Content = frame;

                newViewId = ApplicationView.GetForCurrentView().Id;
                Window.Current.Activate();
            });

            await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId, ViewSizePreference.UseHalf, ((App)(App.Current)).MainViewId, ViewSizePreference.UseLess);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            ApplicationView.GetForCurrentView().TryEnterFullScreenMode();

            //ApplicationView.GetForCurrentView().TryResizeView(new Size(200, 100));
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SecondaryViewPage), null);

        }
    }
}
