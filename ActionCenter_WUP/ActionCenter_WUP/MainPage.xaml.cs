using AdaptiveTileExtensions;
using InteractiveToastExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ActionCenter_WUP
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

        private void CreateTextTile(object sender, RoutedEventArgs e)
        {
            string tileXML = @"<tile>
                                    <visual>
                                        <binding template=""TileMedium"">
                                          <text hint-style=""subtitle"">SDP 2015</text>
                                           <text hint-style=""captionSubtle"">Dynamic medium tile on Windows 10</text>
                                           <text hint-style=""captionSubtle"">It really works and promotes the creativity!</text>
                                        </binding>

                                        <binding template=""TileWide"">
                                          <text hint-style=""subtitle"">SDP 2015</text>
                                           <text hint-style=""captionSubtle"">Dynamic medium tile on Windows 10</text>
                                           <text hint-style=""captionSubtle"">It really works and promotes the creativity!</text>
                                        </binding>
                      
                                        <binding template=""TileLarge"">
                                           <text hint-style=""subtitle"">SDP 2015</text>
                                           <text hint-style=""captionSubtle"">Dynamic medium tile on Windows 10</text>
                                           <text hint-style=""captionSubtle"">It really works and promotes the creativity!</text>
                                        </binding>
                                    </visual>
                                </tile>";

            XmlDocument tileContent = new XmlDocument();
            tileContent.LoadXml(tileXML);

            TileNotification tileNotification = new TileNotification(tileContent);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }

        private void CreateImageTile(object sender, RoutedEventArgs e)
        {
            //Using "alternative" package from http://github.com/ScottIsAFool/AdaptiveTileExtensions
            //Warning: DEPRICATED
            var tile = AdaptiveTile.CreateTile();

            var wideBinding = TileBinding.Create(TemplateType.TileWide);
            wideBinding.Branding = AdaptiveTileExtensions.Branding.NameAndLogo;
            var wideLogo = new TileImage(AdaptiveTileExtensions.ImagePlacement.Inline)
            {
                Source = "http://theartmad.com/wp-content/uploads/2015/04/Smiley-Face-2.png",
                ImageCropping = AdaptiveTileExtensions.ImageCropping.Circle
            };

            var imageSubgroup = new SubGroup()
            {
                Width = 40
            };
            imageSubgroup.AddImage(wideLogo);
            wideBinding.AddSubgroup(imageSubgroup);

            var wideHeader = new AdaptiveTileExtensions.Text("Smile!") { Style = TextStyle.Caption };
            var wideContent = new AdaptiveTileExtensions.Text("Someone likes you!") { Style = TextStyle.Body };

            var wideSubgroup = new SubGroup();
            wideSubgroup.AddText(wideHeader);
            wideSubgroup.AddText(wideContent);
            wideBinding.AddSubgroup(wideSubgroup);

            tile.Tiles.Add(wideBinding);
            var notification = tile.GetNotification();

            ////Alternative method using "official" NotificationsExtensions.Win10 Nuget
            //var tile = new NotificationsExtensions.Tiles.TileContent();
            //tile.Visual.Branding = NotificationsExtensions.Tiles.TileBranding.NameAndLogo;
            ////...

            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);
        }

        private void CreateGrouppedTile(object sender, RoutedEventArgs e)
        {
            var tile = AdaptiveTile.CreateTile();

            var wideBinding = TileBinding.Create(TemplateType.TileWide);
            wideBinding.Branding = AdaptiveTileExtensions.Branding.None;

            var wideText11 = new AdaptiveTileExtensions.Text("Alex") { IsSubtleStyle = true, Alignment = Alignment.Center };
            var wideText12 = new AdaptiveTileExtensions.Text("95%") { Alignment = Alignment.Center };

            var subgroup1 = new SubGroup();
            subgroup1.AddText(wideText11);
            subgroup1.AddText(wideText12);

            var wideText21 = new AdaptiveTileExtensions.Text("Nicole") { IsSubtleStyle = true, Alignment = Alignment.Center };
            var wideText22 = new AdaptiveTileExtensions.Text("90%") { Alignment = Alignment.Center };
            var wideText23 = new AdaptiveTileExtensions.Text("Cool!") { Alignment = Alignment.Center, IsSubtleStyle = true };

            var subgroup2 = new SubGroup();
            subgroup2.AddText(wideText21);
            subgroup2.AddText(wideText22);
            subgroup2.AddText(wideText23);

            wideBinding.AddSubgroup(subgroup1);
            wideBinding.AddSubgroup(subgroup2);

            tile.Tiles.Add(wideBinding);
            var notification = tile.GetNotification();

            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);
        }

        private void CreateBackgroundTile(object sender, RoutedEventArgs e)
        {            
            var tile = AdaptiveTile.CreateTile();

            var wideBinding = TileBinding.Create(TemplateType.TileWide);
            wideBinding.Branding = AdaptiveTileExtensions.Branding.Name;
            //wideBinding.OverlayOpacity = 60;

            var wideLogo = new TileImage(AdaptiveTileExtensions.ImagePlacement.Background)
            {
                Source = "http://www.aertenart.com/images/largeabstract05-310x150.jpg",
            };
            wideBinding.BackgroundImage = wideLogo;

            var wideText11 = new AdaptiveTileExtensions.Text("Cool tile") { Style = TextStyle.Title };
            var wideText12 = new AdaptiveTileExtensions.Text("Windows 10 Tiles are cool!") { Style = TextStyle.Body };

            var subgroup1 = new SubGroup();
            subgroup1.AddText(wideText11);
            subgroup1.AddText(wideText12);

            wideBinding.AddSubgroup(subgroup1);

            tile.Tiles.Add(wideBinding);
            var notification = tile.GetNotification();

            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);
        }

        private async void CreatePeekTile(object sender, RoutedEventArgs e)
        {
            var tile = AdaptiveTile.CreateTile();

            var wideBinding = TileBinding.Create(TemplateType.TileWide);
            wideBinding.Branding = AdaptiveTileExtensions.Branding.None;

            var logo = new TileImage(AdaptiveTileExtensions.ImagePlacement.Peek)
            {
                Source = "http://www.picgifs.com/clip-art/cartoons/snoopy/clip-art-snoopy-124159.jpg"
            };

            var wideText11 = new AdaptiveTileExtensions.Text("Snoopy") { Style = TextStyle.Caption };
            var wideText12 = new AdaptiveTileExtensions.Text("Works like on Windows Phone!") { IsSubtleStyle = true, WrapText = true };

            wideBinding.Add(logo);
            wideBinding.Add(wideText11);
            wideBinding.Add(wideText12);

            tile.Tiles.Add(wideBinding);
            var notification = tile.GetNotification();

            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);

            //JumpList functionality -- supported from Win10.TH2 (Target SDK for 10586 or higher is required)
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.StartScreen.JumpList"))
            {
                if (Windows.UI.StartScreen.JumpList.IsSupported())
                {
                    var jumpList = await Windows.UI.StartScreen.JumpList.LoadCurrentAsync();
                    jumpList.Items.Clear();
                    await jumpList.SaveAsync();

                    jumpList.SystemGroupKind = Windows.UI.StartScreen.JumpListSystemGroupKind.None;
                    jumpList.Items.Add(Windows.UI.StartScreen.JumpListItem.CreateWithArguments("item1", "Snoopy!"));
                    jumpList.Items.Add(Windows.UI.StartScreen.JumpListItem.CreateSeparator());
                    jumpList.Items.Add(Windows.UI.StartScreen.JumpListItem.CreateWithArguments("item2", "Help"));
                    jumpList.Items.Add(Windows.UI.StartScreen.JumpListItem.CreateWithArguments("item3", "About"));
                    await jumpList.SaveAsync();
                }
            }
        }

        private void CreateTextToast(object sender, RoutedEventArgs e)
        {
            string toastXML = @"<toast>
                                  <visual>
                                    <binding template=""ToastGeneric"">
	                                    <text>Sample toast</text>
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

        private void CreateActionToast(object sender, RoutedEventArgs e)
        {
            //Using "alternative" package from http://github.com/ScottIsAFool/InteractiveToastExtensions
            //Warning: DEPRICATED
            var toast = new InteractiveToast();
            var visual = new Visual();
            visual.AddText(new InteractiveToastExtensions.Text("SDP 2015"));
            visual.AddText(new InteractiveToastExtensions.Text("Is is cool?"));
            visual.AddImage(new VisualImage("http://theartmad.com/wp-content/uploads/2015/04/Smiley-Face-2.png")
            {
                ImagePlacement = InteractiveToastExtensions.ImagePlacement.AppLogoOverride
            });
            toast.SetVisual(visual);

            toast.AddActionItem(new ToastAction("Yes", "yes")
            {
                ActivationType = ActivationType.Foreground
            });
            toast.AddActionItem(new ToastAction("Sure!", "sure")
            {
                ActivationType = ActivationType.Background
            });

            var notification = toast.GetNotification();
            ToastNotificationManager.CreateToastNotifier().Show(notification);
        }

        private void CreateInputToast(object sender, RoutedEventArgs e)
        {
            var toast = new InteractiveToast();
            var visual = new Visual();
            visual.AddText(new InteractiveToastExtensions.Text("SDP 2015"));
            visual.AddText(new InteractiveToastExtensions.Text("Any feedback to share?"));
            visual.AddImage(new VisualImage("http://theartmad.com/wp-content/uploads/2015/04/Smiley-Face-2.png")
            {
                ImagePlacement = InteractiveToastExtensions.ImagePlacement.AppLogoOverride
            });

            toast.SetVisual(visual);

            var input = new ToastInput("feedbackFreeText", ToastInputType.Text)
            {
                Title = "You answer:",
                PlaceholderContent = "Your feedback goes here..."
            };

            toast.AddActionItem(input);

            toast.AddActionItem(new ToastAction("Yes, sure!", "yes")
            {
                ActivationType = ActivationType.Foreground
            });
            toast.AddActionItem(new ToastAction("No, thanks!", "no")
            {
                ActivationType = ActivationType.Background
            });

            var notification = toast.GetNotification();

            ToastNotificationManager.CreateToastNotifier().Show(notification);
        }

        private void CreateSelectionToast(object sender, RoutedEventArgs e)
        {
            var toast = new InteractiveToast();
            var visual = new Visual();
            visual.AddText(new InteractiveToastExtensions.Text("SDP 2015"));
            visual.AddText(new InteractiveToastExtensions.Text("Any feedback to share?"));
            visual.AddImage(new VisualImage("http://theartmad.com/wp-content/uploads/2015/04/Smiley-Face-2.png")
            {
                ImagePlacement = InteractiveToastExtensions.ImagePlacement.AppLogoOverride
            });

            toast.SetVisual(visual);

            var input = new ToastInput("feedbackSelection", ToastInputType.Selection)
            {
                DefaultInput = "2",
                Title = "You selection:",
            };
            input.AddSelection("1", "Perfect");
            input.AddSelection("2", "Very Good");
            input.AddSelection("3", "Great");
            toast.AddActionItem(input);

            toast.AddActionItem(new ToastAction("Yes, sure!", "yes")
            {
                ActivationType = ActivationType.Foreground
            });
            toast.AddActionItem(new ToastAction("No, thanks!", "no")
            {
                ActivationType = ActivationType.Background
            });

            var notification = toast.GetNotification();

            ToastNotificationManager.CreateToastNotifier().Show(notification);
        }
    }
}
