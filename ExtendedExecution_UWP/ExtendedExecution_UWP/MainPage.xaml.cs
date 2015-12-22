using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.ExtendedExecution;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ExtendedExecution_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Geolocator locator;
        private ObservableCollection<string> coordinates = new ObservableCollection<string>();
        private ExtendedExecutionSession session;

        public MainPage()
        {
            this.InitializeComponent();

            locator = new Geolocator();
            locator.DesiredAccuracy = PositionAccuracy.High;
            locator.DesiredAccuracyInMeters = 100;
            locator.MovementThreshold = 100;
            locator.PositionChanged += Locator_PositionChanged;
            locator.StatusChanged += (s, e) =>
            {
                Debug.WriteLine(e.Status);
            };
            coords.ItemsSource = coordinates;
        }

        private void Locator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            var coord = args.Position;
            string position = string.Format("{0},{1}",
                args.Position.Coordinate.Point.Position.Latitude, 
                args.Position.Coordinate.Point.Position.Longitude);
            Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                coordinates.Insert(0, position);
            });
        }

        private async void StartLocationExtensionSession()
        {
            session = new ExtendedExecutionSession();
            session.Description = "Location Tracker";
            session.Reason = ExtendedExecutionReason.LocationTracking;
            session.Revoked += ExtendedExecutionSession_Revoked;
            var result = await session.RequestExtensionAsync();
            if (result == ExtendedExecutionResult.Denied)
            {
                StopLocationExtensionSession();
                Debug.WriteLine("Denied!");
            }
        }

        private void ExtendedExecutionSession_Revoked(object sender, ExtendedExecutionRevokedEventArgs args)
        {
            StopLocationExtensionSession();
            Debug.WriteLine("Revoked!");
        }

        private void StopLocationExtensionSession()
        {
            if (session != null)
            {
                session.Dispose();
                session = null;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartLocationExtensionSession();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StopLocationExtensionSession();
        }
    }
}
