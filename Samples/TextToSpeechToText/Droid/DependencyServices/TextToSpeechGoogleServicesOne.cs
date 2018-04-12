using System;
using Android.App;
using Android.Speech.Tts;
using TextToSpeechToText.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(TextToSpeechGoogleServicesOne))]
namespace TextToSpeechToText.Droid
{
    public class TextToSpeechGoogleServicesOne : Java.Lang.Object, ITextToSpeechGoogleServicesOne, TextToSpeech.IOnInitListener 
    {
        public TextToSpeechGoogleServicesOne(){}


        TextToSpeech speaker;
        string toSpeak;
        public void Speak(string text)
        {
            toSpeak = text;
            if (speaker == null)
            {
                speaker = new TextToSpeech(MainActivity.Instance, this);
                //speaker = new TextToSpeech(TextToSpeechGoogleServicesOneActivity.Instance, this);
            }
            else
            {
                speaker.Speak(toSpeak, QueueMode.Flush, null, null);
                System.Diagnostics.Debug.WriteLine("spoke " + toSpeak);
            }
        }

        #region IOnInitListener implementation
        public void OnInit(OperationResult status)
        {
            if (status.Equals(OperationResult.Success))
            {
                System.Diagnostics.Debug.WriteLine("speaker init");
                speaker.Speak(toSpeak, QueueMode.Flush, null, null);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("was quiet");
            }
        }
        #endregion
    }

    [Activity(Label="TextToSpeechGoogleServicesOneActivity")]
    public class TextToSpeechGoogleServicesOneActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        internal static TextToSpeechGoogleServicesOneActivity Instance { get; private set; }

        protected override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Instance = this;
        }
    }
}