using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProtocolHandlerAPP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProtocolForResultsPage : Page
    {
        private Windows.System.ProtocolForResultsOperation _operation = null;

        public ProtocolForResultsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var protocolForResultsArgs = e.Parameter as ProtocolForResultsActivatedEventArgs;
            // Set the ProtocolForResultsOperation field.
            _operation = protocolForResultsArgs.ProtocolForResultsOperation;

            if (protocolForResultsArgs.Data.ContainsKey("TestData"))
            {
                txtOutput.Text = "Input received: " + protocolForResultsArgs.Data["TestData"] as string;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ValueSet result = new ValueSet();
            result["ReturnedData"] = "The returned result";
            _operation.ReportCompleted(result);
        }
    }
}
