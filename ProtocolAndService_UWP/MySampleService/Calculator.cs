using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.Foundation.Collections;

namespace MySampleService
{
    public sealed class Calculator : IBackgroundTask
    {
        private BackgroundTaskDeferral backgroundTaskDeferral;
        private AppServiceConnection appServiceconnection;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            this.backgroundTaskDeferral = taskInstance.GetDeferral(); // Get a deferral so that the service isn't terminated.
            taskInstance.Canceled += OnTaskCanceled; // Associate a cancellation handler with the background task.

            // Retrieve the app service connection and set up a listener for incoming app service requests.
            var details = taskInstance.TriggerDetails as AppServiceTriggerDetails;
            appServiceconnection = details.AppServiceConnection;
            appServiceconnection.RequestReceived += OnRequestReceived;
        }

        private async void OnRequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            var messageDeferral = args.GetDeferral();

            ValueSet message = args.Request.Message;
            ValueSet returnData = new ValueSet();

            string command = message["cmd"] as string;
            int? arg1 = message["arg1"] as int?;
            int? arg2 = message["arg2"] as int?;

            switch (command)
            {
                case "ADD":
                    {
                        returnData.Add("Result", arg1 + arg2);
                        returnData.Add("Status", "OK");
                        break;
                    }

                case "SUB":
                    {
                        returnData.Add("Result", arg1 - arg2);
                        returnData.Add("Status", "OK");
                        break;
                    }

                case "MUL":
                    {
                        returnData.Add("Result", arg1 * arg2);
                        returnData.Add("Status", "OK");
                        break;
                    }

                case "DIV":
                    {
                        returnData.Add("Result", arg1 / arg2);
                        returnData.Add("Status", "OK");
                        break;
                    }

                default:
                    {
                        returnData.Add("Status", "Fail: unknown command");
                        break;
                    }
            }

            await args.Request.SendResponseAsync(returnData); // Return the data to the caller.
            messageDeferral.Complete();
        }

        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            if (this.backgroundTaskDeferral != null)
            {
                // Complete the service deferral.
                this.backgroundTaskDeferral.Complete();
            }
        }
    }
}
