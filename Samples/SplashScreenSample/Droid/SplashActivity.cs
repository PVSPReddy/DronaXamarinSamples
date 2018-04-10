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
    [Activity(Label = "SplashActivity", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //var inflater = LayoutInflater.From(Forms.Context);
            //var nativeView = inflater.Inflate(Resource.Layout.SplashLayout, null);
            SetContentView(Resource.Layout.SplashLayout);
            //SetContentView(Resource.Layout.SplashScreen);
            // Create your application here
            Thread.Sleep(1000); // Simulate a long loading process on app startup.
            StartActivity(typeof(MainActivity));

            /*
            var inflater = LayoutInflater.From(Forms.Context);
            nativeView = inflater.Inflate(Resource.Layout.viewpager, null);
            */
        }
    }
}
