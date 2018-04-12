using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

namespace SplashScreenSample.Droid
{
    //[Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    //[Activity(Label = "Splash", MainLauncher = true, NoHistory = true)]
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SplashLayout);
            var text = FindViewById<TextView>(Resource.Id.splashTextTitle);
            text.Text = "Hello World in Splash";

            System.Threading.Tasks.Task.Run(() => {
                Thread.Sleep(2000);
                StartActivity(typeof(MainActivity));
            });

            //this also works but splash will not be displayed because
            /*
            The screen is blank because by calling Thread.Sleep then StartActivity in OnCreateView,
            you are first pausing the UI thread (which will cause nothing to display)
            and are then exiting the activity immediately by using StartActivity.
            To fix this, shift Thread.Sleep() and StartActivity() into a background thread;
            */
            //so we have to use above method

            //Thread.Sleep(1000); // Simulate a long loading process on app startup.
            //StartActivity(typeof(MainActivity));


        }
    }
}
