﻿using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading.Forms.Platform;
using Foundation;
using Lottie.Forms.iOS.Renderers;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfChart.XForms.iOS.Renderers;
using Syncfusion.SfNumericUpDown.XForms.iOS;
using Syncfusion.SfPdfViewer.XForms.iOS;
using Syncfusion.SfPicker.XForms.iOS;
using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.XForms.iOS.DataForm;
using Syncfusion.XForms.iOS.ProgressBar;
using Syncfusion.XForms.iOS.TabView;
using UIKit;
using UserNotifications;

namespace ImDiabetic.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // Ask the user for permission to get notifications on iOS 10.0+
                UNUserNotificationCenter.Current.RequestAuthorization(
                    UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound,
                    (approved, error) => { });

                // Watch for notifications while app is active
                UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();
            }
            else if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                // Ask the user for permission to get notifications on iOS 8.0+
                var settings = UIUserNotificationSettings.GetSettingsForTypes(
                    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                    new NSSet());

                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }

            global::Xamarin.Forms.Forms.Init();
            AnimationViewRenderer.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            SfChartRenderer.Init();
            SfListViewRenderer.Init();
            SfButtonRenderer.Init();
            SfCheckBoxRenderer.Init();
            SfRadioButtonRenderer.Init();
            SfPdfDocumentViewRenderer.Init();
            SfNumericUpDownRenderer.Init();
            SfTabViewRenderer.Init();
            SfPickerRenderer.Init();
            SfLinearProgressBarRenderer.Init();
            SfCircularProgressBarRenderer.Init();
            CachedImageRenderer.Init();
            SfDataFormRenderer.Init();
            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }
    }
}
