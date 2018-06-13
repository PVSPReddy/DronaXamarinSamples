using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Google.AR.Core;

namespace ARCoreXamSample.Droid
{
    [Activity(Label = "ARCoreXamSample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);






            #region for ARCore
            //var config = Google.AR.Core.Config.CreateDefaultConfig();
            //var config = Config.CreateDefaultConfig();
            //session = new Session(this);
            var session = new Session(this);
            var config = new Config(session);
            // Make sure ARCore is supported on this device
            if (!session.IsSupported(config))
            {
                Toast.MakeText(this, "ARCore unsupported!", ToastLength.Long).Show();
                Finish();
            }
            #endregion



            LoadApplication(new App());
        }
    }
}

