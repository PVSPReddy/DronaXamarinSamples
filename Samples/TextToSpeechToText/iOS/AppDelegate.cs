using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace TextToSpeechToText.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            #region For Screen Height & Width

            App.screenWidth = (int)UIScreen.MainScreen.Bounds.Width;
            App.screenHeight = (int)UIScreen.MainScreen.Bounds.Height;

            #endregion

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
