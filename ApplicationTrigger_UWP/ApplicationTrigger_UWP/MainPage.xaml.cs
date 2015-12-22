using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ApplicationTrigger_UWP
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

        private async Task RegisterAndRunBackgroundTask()
        {
            var access = await BackgroundExecutionManager.RequestAccessAsync();
            if (access == BackgroundAccessStatus.Denied || access == BackgroundAccessStatus.Unspecified)
            {
                Debug.WriteLine("Background tasks are not permitted");
            }
            else
            {
                string name = nameof(MyBackgroundTask.MyTask);

                foreach (var cur in BackgroundTaskRegistration.AllTasks)
                {
                    if (cur.Value.Name == name)
                    {
                        cur.Value.Unregister(true);
                    }
                }

                var builder = new BackgroundTaskBuilder();

                var trigger = new ApplicationTrigger();

                builder.Name = name;
                builder.TaskEntryPoint = typeof(MyBackgroundTask.MyTask).ToString();
                builder.SetTrigger(trigger);
                BackgroundTaskRegistration task = builder.Register();

                var result = await trigger.RequestAsync();

                Debug.WriteLine(result.ToString());
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await RegisterAndRunBackgroundTask();
            Debug.WriteLine("Done! You should see toast anytime soon!");
        }
    }
}
