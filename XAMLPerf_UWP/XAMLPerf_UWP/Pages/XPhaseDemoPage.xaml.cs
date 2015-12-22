using System;
using Windows.UI.Xaml.Controls;
using XAMLPerf_UWP.Model;

namespace XAMLPerf_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class XPhaseDemoPage : Page
    {
        public ViewModel ViewModel { get; set; }

        public XPhaseDemoPage()
        {
            this.InitializeComponent();

            ViewModel = new ViewModel();
            Bindings.Update();
        }
    }
}
