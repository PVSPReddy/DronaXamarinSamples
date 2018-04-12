using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Content;
using Android.Runtime;
using Android.Speech.Tts;
using TextToSpeechToText.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(TextToSpeechAndroidService))]
namespace TextToSpeechToText.Droid
{
    public class TextToSpeechAndroidService : Java.Lang.Object, ITextToSpeechService, Android.Speech.Tts.TextToSpeech.IOnInitListener
    {
        public TextToSpeechAndroidService(){}

        //public IntPtr Handle => throw new NotImplementedException();

        //public void Dispose()
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message + "\n" + ex.StackTrace;
        //        System.Diagnostics.Debug.WriteLine(msg);
        //    }
        //}

        public void OnInit([GeneratedEnum] OperationResult status)
        {
            try
            {

            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

        TextToSpeech textToSpeech;
        Android.App.Activity activity = (Android.App.Activity)Forms.Context;
        Java.Util.Locale lang;
        List<string> langAvailable;
        public async Task<bool> StartClick(string speakValue)
        {
            //var activitye = (Android.App.Activity)Forms.Context;
            bool returnValue = false;
            try
            {
                // set up the TextToSpeech object
                // third parameter is the speech engine to use
                textToSpeech = new TextToSpeech(activity, this, "com.google.android.tts");

                // set up the langauge spinner
                // set the top option to be default
                langAvailable = new List<string> { "Default" };

                // our spinner only wants to contain the languages supported by the tts and ignore the rest
                var localesAvailable = Java.Util.Locale.GetAvailableLocales().ToList();
                foreach (var locale in localesAvailable)
                {
                    LanguageAvailableResult res = textToSpeech.IsLanguageAvailable(locale);
                    switch (res)
                    {
                        case LanguageAvailableResult.Available:
                            langAvailable.Add(locale.DisplayLanguage);
                            break;
                        case LanguageAvailableResult.CountryAvailable:
                            langAvailable.Add(locale.DisplayLanguage);
                            break;
                        case LanguageAvailableResult.CountryVarAvailable:
                            langAvailable.Add(locale.DisplayLanguage);
                            break;
                    }

                }
                langAvailable = langAvailable.OrderBy(t => t).Distinct().ToList();

                // set up the speech to use the default langauge
                // if a language is not available, then the default language is used.
                lang = Java.Util.Locale.Default;
                textToSpeech.SetLanguage(lang);

                // set the speed and pitch
                textToSpeech.SetPitch(.5f);
                textToSpeech.SetSpeechRate(.5f);

                AddLanguage();

            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            //Talk(speakValue);
            return returnValue;
        }

        public void Talk(string speakValue)
        {
            if (!string.IsNullOrEmpty(speakValue))
            {
                textToSpeech.Speak(speakValue, QueueMode.Flush, null);
            }
        }

        void AddLanguage()
        {
            lang = Java.Util.Locale.GetAvailableLocales().FirstOrDefault(t => t.DisplayLanguage == langAvailable[0]);
            //lang = Java.Util.Locale.GetAvailableLocales().FirstOrDefault(t => t.DisplayLanguage == langAvailable[(int)e.Id]);
            // create intent to check the TTS has this language installed
            var checkTTSIntent = new Intent();
            checkTTSIntent.SetAction(TextToSpeech.Engine.ActionCheckTtsData);
            activity.StartActivityForResult(checkTTSIntent, 1);

        }

        // Interface method required for IOnInitListener
        void TextToSpeech.IOnInitListener.OnInit(OperationResult status)
        {
            // if we get an error, default to the default language
            if (status == OperationResult.Error)
                textToSpeech.SetLanguage(Java.Util.Locale.Default);
            // if the listener is ok, set the lang
            if (status == OperationResult.Success)
                textToSpeech.SetLanguage(lang);
        }

        //void ITextToSpeechService.Talk(string speakValue)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

