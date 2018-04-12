using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Speech;
using TextToSpeechToText.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(SpeechToTextGoogleServiceOne))]
namespace TextToSpeechToText.Droid
{
    public class SpeechToTextGoogleServiceOne : ISpeechToTextGoogleServiceOne
    {
        public SpeechToTextGoogleServiceOne(){}

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
            var activity = Forms.Context as Activity;
            try
            {
                var intent = new Intent(activity, typeof(SpeechToTextGoogleServiceOneActivity));
                intent.PutExtra("id", 2);
                activity.StartActivity(intent);
                ////Intent = new Intent();
                //intent.SetType("image/*");
                ////Intent.PutExtra(Intent.ActionSendMultiple, true);
                ////Intent.PutExtra(Intent.ExtraAllowMultiple, true);
                //intent.SetAction(Intent.ActionGetContent);
                //activity.StartActivityForResult(Intent.CreateChooser(intent, "Select Picture"), 2);

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return returnValue;
        }
    }

    [Android.App.Activity(Label = "Activity")]
    public class SpeechToTextGoogleServiceOneActivity : Activity//global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity//Android.App.Activity
    {

        protected override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
            voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, this.GetString(Resource.String.messageSpeakNow));
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
            voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
            //StartActivityForResult(voiceIntent, VOICE);
            this.StartActivityForResult(voiceIntent, 0);
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
            else if (requestCode == 1)
            {
                // we need a new language installed
                var installTTS = new Intent();
                installTTS.SetAction(Android.Speech.Tts.TextToSpeech.Engine.ActionInstallTtsData);
                StartActivity(installTTS);
            }
            Finish();
        }
    }
}

