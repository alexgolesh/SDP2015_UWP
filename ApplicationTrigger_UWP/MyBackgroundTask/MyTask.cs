using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace MyBackgroundTask
{
    public sealed class MyTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var cost = BackgroundWorkCost.CurrentBackgroundWorkCost;
            if (cost == BackgroundWorkCostValue.High)
            {
                Debug.WriteLine("Cannot run due to high cost");
                return;
            }

            taskInstance.Canceled += TaskInstance_Canceled;

            var deferral = taskInstance.GetDeferral();
            try
            {
                await Task.Delay(5000);

                string toastXML = @"<toast>
                                  <visual>
                                    <binding template=""ToastGeneric"">
	                                    <text>Sample toast from background</text>
                                          <text>look at new image</text>
                                          <text>It is amazing!!</text>
                                          <image placement=""appLogoOverride"" src=""http://theartmad.com/wp-content/uploads/2015/04/Smiley-Face-2.png"" />
	                                      <image placement=""inline"" src=""http://www.picgifs.com/clip-art/cartoons/snoopy/clip-art-snoopy-124159.jpg"" />
                                    </binding>
                                  </visual>
                                </toast>";

                XmlDocument toastContent = new XmlDocument();
                toastContent.LoadXml(toastXML);

                ToastNotification toastNotification = new ToastNotification(toastContent);
                ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
            }
            catch
            {
                //Catch exceptions
            }
            finally
            {
                deferral.Complete();
            }
        }

        private void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            //Handle cancellation
        }
    }
}
