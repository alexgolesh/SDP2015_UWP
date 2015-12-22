using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.VoiceCommands;

namespace VoiceService
{
    public sealed class VoiceService : IBackgroundTask
    {
        private BackgroundTaskDeferral serviceDeferral;
        VoiceCommandServiceConnection voiceServiceConnection;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral _deferral = taskInstance.GetDeferral();

            taskInstance.Canceled += OnTaskCanceled;

            var triggerDetails = taskInstance.TriggerDetails as AppServiceTriggerDetails;

            if (triggerDetails != null && triggerDetails.Name == "VoiceService")
            {
                try
                {
                    voiceServiceConnection = VoiceCommandServiceConnection.FromAppServiceTriggerDetails(triggerDetails);

                    voiceServiceConnection.VoiceCommandCompleted += VoiceCommandCompleted;

                    VoiceCommand voiceCommand = await voiceServiceConnection.GetVoiceCommandAsync();

                    switch (voiceCommand.CommandName)
                    {
                        case "whenIsMySessionInDestination":
                            {
                                var destination = voiceCommand.Properties["destination"][0];
                                SendCompletionMessageForDestination(destination);
                                break;
                            }
                        case "canceSessionInDestination":
                            {
                                var destination = voiceCommand.Properties["destination"][0];
                                CancelForDestination(destination);
                                break;
                            }
                        case "canceFlightToDestination":
                            {
                                var destination = voiceCommand.Properties["destination"][0];
                                CancelFlightToDestination(destination);
                                break;
                            }
                        // As a last resort launch the app in the foreground
                        default:
                            LaunchAppInForeground();
                            break;
                    }
                }
                finally
                {
                    if (this.serviceDeferral != null)
                    {
                        //Complete the service deferral
                        this.serviceDeferral.Complete();
                    }
                }
            }
        }

        

        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            if (this.serviceDeferral != null)
            {
                //Complete the service deferral
                this.serviceDeferral.Complete();
            }
        }

        private void VoiceCommandCompleted(VoiceCommandServiceConnection sender, VoiceCommandCompletedEventArgs args)
        {
            if (this.serviceDeferral != null)
            {
                //Complete the service deferral
                this.serviceDeferral.Complete();
            }
        }

        private async void CancelFlightToDestination(string destination)
        {
            var userPrompt = new VoiceCommandUserMessage();
            userPrompt.DisplayMessage = "Which one do you want to cancel?";
            userPrompt.SpokenMessage = "Which Tel-Aviv trip do you want to cancel? Flight on December 14th or December 17th?";


            var userReprompt = new VoiceCommandUserMessage();
            userReprompt.DisplayMessage = "Which one did you want to cancel?";
            userReprompt.SpokenMessage = "Which one did you want to cancel?";

            var destinationsContentTiles = new List<VoiceCommandContentTile>();

            var destinationTile = new VoiceCommandContentTile();
            destinationTile.ContentTileType = VoiceCommandContentTileType.TitleWithText;
            destinationTile.AppContext = "id_TLV_001"; //OPTIONAL, for use in the app
            destinationTile.Title = "Early flight";
            destinationTile.TextLine1 = "December 14th";
            destinationsContentTiles.Add(destinationTile);

            var destination2 = new VoiceCommandContentTile();
            destination2.ContentTileType = VoiceCommandContentTileType.TitleWithText;
            destination2.AppContext = "id_TLV_002";
            destination2.Title = "Later flight";
            destination2.TextLine1 = "December 18th";
            destinationsContentTiles.Add(destination2);

            var response = VoiceCommandResponse.CreateResponseForPrompt(userPrompt, userReprompt, destinationsContentTiles);

            var voiceCommandDisambiguationResult = await voiceServiceConnection.RequestDisambiguationAsync(response);

            if (voiceCommandDisambiguationResult != null)
            {
                var id = voiceCommandDisambiguationResult.SelectedItem.AppContext.ToString();

                if (id == "id_TLV_001")
                {
                    var userMessage = new VoiceCommandUserMessage();
                    userMessage.DisplayMessage = "Cancelling your flight on December 14th.";
                    userMessage.SpokenMessage = "Okay, cancelling your flight on December 14th.";

                    var resp = VoiceCommandResponse.CreateResponse(userMessage, destinationsContentTiles);
                    await voiceServiceConnection.ReportSuccessAsync(resp);
                }
                else if (id == "id_TLV_002")
                {
                    var userMessage = new VoiceCommandUserMessage();
                    userMessage.DisplayMessage = "Cancelling your flight on December 18th.";
                    userMessage.SpokenMessage = "Okay, cancelling your flight on December 18th.";

                    var resp = VoiceCommandResponse.CreateResponse(userMessage, destinationsContentTiles);

                    await voiceServiceConnection.ReportSuccessAsync(resp);
                }
                else
                {
                    var userMessage = new VoiceCommandUserMessage();
                    userMessage.DisplayMessage = "Not sure what to cancel. Try again.";
                    userMessage.SpokenMessage = "Not sure what to cancel. Try again.";

                    var resp = VoiceCommandResponse.CreateResponse(userMessage, destinationsContentTiles);
                    await voiceServiceConnection.ReportSuccessAsync(resp);
                }
            }
        }

        private async void CancelForDestination(string destination)
        {
            var userPrompt = new VoiceCommandUserMessage();
            userPrompt.SpokenMessage = userPrompt.DisplayMessage = "Are you sure you want to cancel the session in Tel-Aviv?";

            var userReprompt = new VoiceCommandUserMessage();
            userReprompt.DisplayMessage = userReprompt.SpokenMessage = "Do you want to cancel this session in Tel-Aviv?";

            var destinationsContentTiles = new List<VoiceCommandContentTile>();

            var destinationTile = new VoiceCommandContentTile();
            destinationTile.ContentTileType = VoiceCommandContentTileType.TitleWithText;
            destinationTile.Title = "SDP 2015";
            destinationTile.TextLine1 = "December 21nd";
            destinationsContentTiles.Add(destinationTile);

            var response = VoiceCommandResponse.CreateResponseForPrompt(userPrompt, userReprompt, destinationsContentTiles);

            var voiceCommandConfirmation = await voiceServiceConnection.RequestConfirmationAsync(response);

            if (voiceCommandConfirmation != null)
            {
                if (voiceCommandConfirmation.Confirmed)
                {
                    var userMessage = new VoiceCommandUserMessage();
                    userMessage.DisplayMessage = "Cancelling your session.";
                    userMessage.SpokenMessage = "Okay, cancelling your session in Tel-Aviv.";

                    var resp = VoiceCommandResponse.CreateResponse(userMessage, destinationsContentTiles);

                    await voiceServiceConnection.ReportSuccessAsync(resp);
                }
            }
        }

        private async void SendCompletionMessageForDestination(string destination)
        {
            var userMessage = new VoiceCommandUserMessage();
            userMessage.DisplayMessage = "Here’s are your sessions.";
            userMessage.SpokenMessage = "Your have two sessions in Tel-Aviv.";

            var destinationsContentTiles = new List<VoiceCommandContentTile>();

            var destinationTile = new VoiceCommandContentTile();
            destinationTile.ContentTileType = VoiceCommandContentTileType.TitleWithText;
            destinationTile.AppLaunchArgument = string.Format("destination={0}", "Tel-Aviv-1");
            destinationTile.Title = "Tel-Aviv";
            destinationTile.TextLine1 = "December 21st, 2015";
            destinationsContentTiles.Add(destinationTile);

            var destinationTile2 = new VoiceCommandContentTile();
            destinationTile2.ContentTileType = VoiceCommandContentTileType.TitleWithText;
            destinationTile2.AppLaunchArgument = string.Format("destination={0}", "Tel-Aviv-2");
            destinationTile2.Title = "Tel-Aviv";
            destinationTile2.TextLine1 = "December 22nd, 2015";
            destinationsContentTiles.Add(destinationTile2);

            var response = VoiceCommandResponse.CreateResponse(userMessage, destinationsContentTiles);
            response.AppLaunchArgument =string.Format("destination={0}", "Tel-Aviv");

            await voiceServiceConnection.ReportSuccessAsync(response);
        }

        private async void LaunchAppInForeground()
        {
            var userMessage = new VoiceCommandUserMessage();
            userMessage.SpokenMessage = "Launching Cortana Demo";

            var response = VoiceCommandResponse.CreateResponse(userMessage);
            response.AppLaunchArgument = "showAllSessions=true";

            await voiceServiceConnection.RequestAppLaunchAsync(response);
        }
    }
}
