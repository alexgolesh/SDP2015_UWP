using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;

namespace ToastNotificationTask
{
    public sealed class ToastNotificationBackgroundTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral _deferral = taskInstance.GetDeferral();

            var details = taskInstance.TriggerDetails as ToastNotificationActionTriggerDetail;
            var arguments = details.Argument;
            if (details.UserInput.ContainsKey("feedbackSelection"))
            {
                Debug.WriteLine("[BT] User said: " + arguments + " and selected: " + details.UserInput["feedbackSelection"]);
            }
            else if (details.UserInput.ContainsKey("feedbackFreeText"))
            {
                Debug.WriteLine("[BT] User said: " + arguments + " and added: " + details.UserInput["feedbackFreeText"]);
            }
            else
            {
                Debug.WriteLine("[BT] User said: " + arguments);
            }

            _deferral.Complete();
        }
    }
}
