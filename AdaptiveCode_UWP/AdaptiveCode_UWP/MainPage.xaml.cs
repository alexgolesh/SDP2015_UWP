using System;
using System.Diagnostics;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AdaptiveCode_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public string Platform
        {
            get { return (string)GetValue(PlatformProperty); }
            set { SetValue(PlatformProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Platform.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlatformProperty =
            DependencyProperty.Register("Platform", typeof(string), typeof(MainPage), new PropertyMetadata("Unknown"));


        public MainPage()
        {
            this.InitializeComponent();

            this.DataContext = this;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            bool isHardwareButtonsAPIPresent = Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons");
            bool isHardwareButtons_CameraPressedAPIPresent = Windows.Foundation.Metadata.ApiInformation.IsEventPresent("Windows.Phone.UI.Input.HardwareButtons", "CameraPressed");

            if (isHardwareButtonsAPIPresent || isHardwareButtons_CameraPressedAPIPresent)
                HardwareButtons.CameraPressed += HardwareButtons_CameraPressed;

            if (Windows.Foundation.Metadata.ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1))
                Platform = "Windows 10 Mobile";
            else if (Windows.Foundation.Metadata.ApiInformation.IsApiContractPresent("Windows.ApplicationModel.Calls.LockScreenCallContract", 1))
                Platform = "Windows 10 Desktop";
            else if (Windows.Foundation.Metadata.ApiInformation.IsApiContractPresent("Windows.System.SystemManagementContract", 1))
                Platform = "Windows 10 IoT";
            else
                Platform = "Windows 10 Universal";
        }

        private void HardwareButtons_CameraPressed(object sender, CameraEventArgs e)
        {
            Debug.WriteLine("Camera button pressed!");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(ThePage));
        }
    }
}
