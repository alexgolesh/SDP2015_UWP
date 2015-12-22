using System;
using System.Linq;
using Windows.UI.Xaml.Controls;
using XAMLPerf_UWP.Pages;
using XAMLPerf_UWP.Presentation;

namespace XAMLPerf_UWP
{
    public sealed partial class Shell : UserControl
    {
        public Shell()
        {
            this.InitializeComponent();

            var vm = new ShellViewModel();
            vm.MenuItems.Add(new MenuItem { Icon = "", Title = "x:Phase", PageType = typeof(XPhaseDemoPage) });
            vm.MenuItems.Add(new MenuItem { Icon = "", Title = "Deferred Loading", PageType = typeof(DefferedLoadingDemoPage) });

            // select the first menu item
            vm.SelectedMenuItem = vm.MenuItems.First();

            this.ViewModel = vm;
        }

        public ShellViewModel ViewModel { get; private set; }

        public Frame RootFrame
        {
            get
            {
                return this.Frame;
            }
        }
    }
}
