using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace TextToSpeechToText.Droid
{
    [Activity(Label = "TextToSpeechToText.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        internal static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            Instance = this;

            #region For screen Height & Width

            var pixels = Resources.DisplayMetrics.WidthPixels;
            var scale = Resources.DisplayMetrics.Density;

            var dps = (double)((pixels - 0.5f) / scale);

            var ScreenWidth = (int)dps;
            App.screenWidth = ScreenWidth;

            RequestedOrientation = ScreenOrientation.Portrait;

            pixels = Resources.DisplayMetrics.HeightPixels;
            dps = (double)((pixels - 0.5f) / scale);

            var ScreenHeight = (int)dps;
            App.screenHeight = ScreenHeight;

            #endregion

            LoadApplication(new App());
        }

        string textInput = "";
        protected override void OnActivityResult(int requestCode, Result resultVal, Intent data)
        {
            if (requestCode == 0)
            {
                if (resultVal == Result.Ok)
                {
                    var matches = data.GetStringArrayListExtra(Android.Speech.RecognizerIntent.ExtraResults);
                    if (matches.Count != 0)
                    {
                        // = textBox.Text + matches[0];
                        textInput = textInput + matches[0];
                        SpeechToTextPage.speechToTextPage.onAndroidResultObtained(textInput);
                        //textBox.Text = textInput;
                        switch (matches[0].Substring(0, 5).ToLower())
                        {
                            //case "north":
                            //    MovePlayer(0);
                            //    break;
                            //case "south":
                                //MovePlayer(1);
                                //break;
                        }
                    }
                    else
                    {
                        SpeechToTextPage.speechToTextPage.onAndroidResultObtained(textInput);
                        //textBox.Text = "No speech was recognised";
                    }
                }
                base.OnActivityResult(requestCode, resultVal, data);
            }
            else if(requestCode == 1)
            {
                // we need a new language installed
                var installTTS = new Intent();
                installTTS.SetAction(Android.Speech.Tts.TextToSpeech.Engine.ActionInstallTtsData);
                StartActivity(installTTS);
            }
        }
    }
}
