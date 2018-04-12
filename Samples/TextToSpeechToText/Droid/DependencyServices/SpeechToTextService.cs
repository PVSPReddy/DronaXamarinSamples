using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Speech;
using TextToSpeechToText.Droid;
using Xamarin.Forms;

[assembly : Dependency(typeof(SpeechToTextService))]
namespace TextToSpeechToText.Droid
{
    public class SpeechToTextService : ISpeechToTextService
    {
        public SpeechToTextService(){}

        public async Task<bool> StartClick()
        {
            bool returnValue = false;
            try
            {
                string rec = Android.Content.PM.PackageManager.FeatureMicrophone;
                if (rec != "android.hardware.microphone")
                {
                    returnValue = false;
                    //var alert = new AlertDialog.Builder(recButton.Context);
                    //alert.SetTitle("You don't seem to have a microphone to record with");
                    //alert.SetPositiveButton("OK", (sender, e) =>
                    //{
                    //    return;
                    //});
                    //alert.Show();
                }
                else
                {
                    StartRecording();
                    returnValue = true;
                }
            }
            catch (Exception ex)
            {
                returnValue = false;
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            return returnValue; 
            /*
            bool returnValue = false;
            try
            {

            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            return returnValue;*/
        }

        public async Task<bool> StartRecording()
        {
            bool returnValue = false;
            try
            {
                var activity = (Android.App.Activity)Forms.Context;
                var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
                voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
                voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, activity.GetString(Resource.String.messageSpeakNow));
                voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
                voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
                voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
                voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
                voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
                //StartActivityForResult(voiceIntent, VOICE);
                activity.StartActivityForResult(voiceIntent, 0);
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            return returnValue; 
        }
    }
}

