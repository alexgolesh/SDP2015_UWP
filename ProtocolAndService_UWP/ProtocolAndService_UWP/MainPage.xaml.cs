using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.AppService;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ProtocolAndService_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private AppServiceConnection calcService;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void CallProtocol1(object sender, RoutedEventArgs e)
        {
            var testAppUri = new Uri("com.alexgolesh.sdp-sample-noresults:"); // The protocol handled by the launched app
            var options = new LauncherOptions();
            options.TargetApplicationPackageFamilyName = "0887d9bb-f83b-475b-8628-bcf3ae6d7583_9dbwxr0dvqxpe";

            var inputData = new ValueSet();
            inputData["TestData"] = "Test data";

            bool result = await Windows.System.Launcher.LaunchUriAsync(testAppUri, options, inputData);
            if (result)
            {
                var msg = new Windows.UI.Popups.MessageDialog("Done for #1!");
                await msg.ShowAsync();
            }
        }

        private async void CallProtocol2(object sender, RoutedEventArgs e)
        {
            var testAppUri = new Uri("com.alexgolesh.sdp-sample-optresults:"); // The protocol handled by the launched app
            var options = new LauncherOptions();
            options.TargetApplicationPackageFamilyName = "0887d9bb-f83b-475b-8628-bcf3ae6d7583_9dbwxr0dvqxpe";

            var inputData = new ValueSet();
            inputData["TestData"] = "Test data";

            bool result = await Windows.System.Launcher.LaunchUriAsync(testAppUri, options, inputData);
            if (result)
            {
                var msg = new Windows.UI.Popups.MessageDialog("Done for #2!");
                await msg.ShowAsync();
            }
        }
        private async void CallProtocol2a(object sender, RoutedEventArgs e)
        {
            var testAppUri = new Uri("com.alexgolesh.sdp-sample-optresults:"); // The protocol handled by the launched app
            var options = new LauncherOptions();
            options.TargetApplicationPackageFamilyName = "0887d9bb-f83b-475b-8628-bcf3ae6d7583_9dbwxr0dvqxpe";

            var inputData = new ValueSet();
            inputData["TestData"] = "Test data";

            var result = await Windows.System.Launcher.LaunchUriForResultsAsync(testAppUri, options, inputData);
            if (result.Status == LaunchUriStatus.Success &&
                result.Result != null &&
                result.Result.ContainsKey("ReturnedData"))
            {
                ValueSet theValues = result.Result;
                var msg = new Windows.UI.Popups.MessageDialog("Done for #2 with '" + theValues["ReturnedData"] as string + "'");
                await msg.ShowAsync();
            }
        }

        private async void CallProtocol3(object sender, RoutedEventArgs e)
        {
            var testAppUri = new Uri("com.alexgolesh.sdp-sample-results:"); // The protocol handled by the launched app
            var options = new LauncherOptions();
            options.TargetApplicationPackageFamilyName = "0887d9bb-f83b-475b-8628-bcf3ae6d7583_9dbwxr0dvqxpe";

            var inputData = new ValueSet();
            inputData["TestData"] = "Test data";

            var result = await Windows.System.Launcher.LaunchUriForResultsAsync(testAppUri, options, inputData);
            if (result.Status == LaunchUriStatus.Success &&
                result.Result != null &&
                result.Result.ContainsKey("ReturnedData"))
            {
                ValueSet theValues = result.Result;
                var msg = new Windows.UI.Popups.MessageDialog("Done for #3 with '" + theValues["ReturnedData"] as string + "'");
                await msg.ShowAsync();
            }
        }

        private async void CallService1(object sender, RoutedEventArgs e)
        {
            // Add the connection.
            if (this.calcService == null)
            {
                this.calcService = new AppServiceConnection();
                this.calcService.AppServiceName = "com.alexgolesh.sdp-sample-calc";
                this.calcService.PackageFamilyName = "e04f0489-69d1-4b0e-8838-01bb877a7677_9dbwxr0dvqxpe";

                var status = await this.calcService.OpenAsync();
                if (status != AppServiceConnectionStatus.Success)
                {
                    btn1.IsEnabled = false;
                    btn2.IsEnabled = false;
                    btn3.IsEnabled = false;
                    btn4.IsEnabled = false;
                    btn5.IsEnabled = false;

                    var msg = new Windows.UI.Popups.MessageDialog("Cannot connect to the service!");
                    await msg.ShowAsync();
                    return;
                }
            }

            // Call the service.
            var message = new ValueSet();
            message.Add("cmd", "ADD");
            message.Add("arg1", 1);
            message.Add("arg2", 2);
            AppServiceResponse response = await this.calcService.SendMessageAsync(message);

            if (response.Status == AppServiceResponseStatus.Success)
            {
                // Get the data  that the service sent  to us.
                if (response.Message["Status"] as string == "OK")
                {
                    var msg = new Windows.UI.Popups.MessageDialog("Result : " + response.Message["Result"] as string);
                    await msg.ShowAsync();
                }
                else
                {
                    var msg = new Windows.UI.Popups.MessageDialog("Result : " + response.Message["Status"] as string);
                    await msg.ShowAsync();
                }
            }
            else
            {
                var msg = new Windows.UI.Popups.MessageDialog("Error calling the service: " + response.Status.ToString());
                await msg.ShowAsync();
            }
        }

        private async void CallService2(object sender, RoutedEventArgs e)
        {
            // Add the connection.
            if (this.calcService == null)
            {
                this.calcService = new AppServiceConnection();
                this.calcService.AppServiceName = "com.alexgolesh.sdp-sample-calc";
                this.calcService.PackageFamilyName = "e04f0489-69d1-4b0e-8838-01bb877a7677_9dbwxr0dvqxpe";

                var status = await this.calcService.OpenAsync();
                if (status != AppServiceConnectionStatus.Success)
                {
                    btn1.IsEnabled = false;
                    btn2.IsEnabled = false;
                    btn3.IsEnabled = false;
                    btn4.IsEnabled = false;
                    btn5.IsEnabled = false;

                    var msg = new Windows.UI.Popups.MessageDialog("Cannot connect to the service!");
                    await msg.ShowAsync();
                    return;
                }
            }

            // Call the service.
            var message = new ValueSet();
            message.Add("cmd", "SUB");
            message.Add("arg1", 10);
            message.Add("arg2", 5);
            AppServiceResponse response = await this.calcService.SendMessageAsync(message);

            if (response.Status == AppServiceResponseStatus.Success)
            {
                // Get the data  that the service sent  to us.
                if (response.Message["Status"] as string == "OK")
                {
                    var msg = new Windows.UI.Popups.MessageDialog("Result : " + response.Message["Result"] as string);
                    await msg.ShowAsync();
                }
                else
                {
                    var msg = new Windows.UI.Popups.MessageDialog("Result : " + response.Message["Status"] as string);
                    await msg.ShowAsync();
                }
            }
            else
            {
                var msg = new Windows.UI.Popups.MessageDialog("Error calling the service: " + response.Status.ToString());
                await msg.ShowAsync();
            }
        }

        private async void CallService3(object sender, RoutedEventArgs e)
        {
            // Add the connection.
            if (this.calcService == null)
            {
                this.calcService = new AppServiceConnection();
                this.calcService.AppServiceName = "com.alexgolesh.sdp-sample-calc";
                this.calcService.PackageFamilyName = "e04f0489-69d1-4b0e-8838-01bb877a7677_9dbwxr0dvqxpe";

                var status = await this.calcService.OpenAsync();
                if (status != AppServiceConnectionStatus.Success)
                {
                    btn1.IsEnabled = false;
                    btn2.IsEnabled = false;
                    btn3.IsEnabled = false;
                    btn4.IsEnabled = false;
                    btn5.IsEnabled = false;

                    var msg = new Windows.UI.Popups.MessageDialog("Cannot connect to the service!");
                    await msg.ShowAsync();
                    return;
                }
            }

            // Call the service.
            var message = new ValueSet();
            message.Add("cmd", "MUL");
            message.Add("arg1", 3);
            message.Add("arg2", 5);
            AppServiceResponse response = await this.calcService.SendMessageAsync(message);

            if (response.Status == AppServiceResponseStatus.Success)
            {
                // Get the data  that the service sent  to us.
                if (response.Message["Status"] as string == "OK")
                {
                    var msg = new Windows.UI.Popups.MessageDialog("Result : " + response.Message["Result"] as string);
                    await msg.ShowAsync();
                }
                else
                {
                    var msg = new Windows.UI.Popups.MessageDialog("Result : " + response.Message["Status"] as string);
                    await msg.ShowAsync();
                }
            }
            else
            {
                var msg = new Windows.UI.Popups.MessageDialog("Error calling the service: " + response.Status.ToString());
                await msg.ShowAsync();
            }
        }

        private async void CallService4(object sender, RoutedEventArgs e)
        {
            // Add the connection.
            if (this.calcService == null)
            {
                this.calcService = new AppServiceConnection();
                this.calcService.AppServiceName = "com.alexgolesh.sdp-sample-calc";
                this.calcService.PackageFamilyName = "e04f0489-69d1-4b0e-8838-01bb877a7677_9dbwxr0dvqxpe";

                var status = await this.calcService.OpenAsync();
                if (status != AppServiceConnectionStatus.Success)
                {
                    btn1.IsEnabled = false;
                    btn2.IsEnabled = false;
                    btn3.IsEnabled = false;
                    btn4.IsEnabled = false;
                    btn5.IsEnabled = false;

                    var msg = new Windows.UI.Popups.MessageDialog("Cannot connect to the service!");
                    await msg.ShowAsync();
                    return;
                }
            }

            // Call the service.
            var message = new ValueSet();
            message.Add("cmd", "DIV");
            message.Add("arg1", 10);
            message.Add("arg2", 2);
            AppServiceResponse response = await this.calcService.SendMessageAsync(message);

            if (response.Status == AppServiceResponseStatus.Success)
            {
                // Get the data  that the service sent  to us.
                if (response.Message["Status"] as string == "OK")
                {
                    var msg = new Windows.UI.Popups.MessageDialog("Result : " + response.Message["Result"] as string);
                    await msg.ShowAsync();
                }
                else
                {
                    var msg = new Windows.UI.Popups.MessageDialog("Result : " + response.Message["Status"] as string);
                    await msg.ShowAsync();
                }
            }
            else
            {
                var msg = new Windows.UI.Popups.MessageDialog("Error calling the service: " + response.Status.ToString());
                await msg.ShowAsync();
            }
        }

        private async void CallService5(object sender, RoutedEventArgs e)
        {
            // Add the connection.
            if (this.calcService == null)
            {
                this.calcService = new AppServiceConnection();
                this.calcService.AppServiceName = "com.alexgolesh.sdp-sample-calc";
                this.calcService.PackageFamilyName = "e04f0489-69d1-4b0e-8838-01bb877a7677_9dbwxr0dvqxpe";

                var status = await this.calcService.OpenAsync();
                if (status != AppServiceConnectionStatus.Success)
                {
                    btn1.IsEnabled = false;
                    btn2.IsEnabled = false;
                    btn3.IsEnabled = false;
                    btn4.IsEnabled = false;
                    btn5.IsEnabled = false;

                    var msg = new Windows.UI.Popups.MessageDialog("Cannot connect to the service!");
                    await msg.ShowAsync();
                    return;
                }
            }

            // Call the service.
            var message = new ValueSet();
            message.Add("cmd", "ZZZ");
            message.Add("arg1", 10);
            message.Add("arg2", 2);
            AppServiceResponse response = await this.calcService.SendMessageAsync(message);

            if (response.Status == AppServiceResponseStatus.Success)
            {
                // Get the data  that the service sent  to us.
                if (response.Message["Status"] as string == "OK")
                {
                    var msg = new Windows.UI.Popups.MessageDialog("Result : " + response.Message["Result"] as string);
                    await msg.ShowAsync();
                }
                else
                {
                    var msg = new Windows.UI.Popups.MessageDialog("Result : " + response.Message["Status"] as string);
                    await msg.ShowAsync();
                }
            }
            else
            {
                var msg = new Windows.UI.Popups.MessageDialog("Error calling the service: " + response.Status.ToString());
                await msg.ShowAsync();
            }
        }
    }
}
