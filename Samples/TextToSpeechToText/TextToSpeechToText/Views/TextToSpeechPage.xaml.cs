using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TextToSpeechToText
{
    public partial class TextToSpeechPage : ContentPage
    {
        public static TextToSpeechPage textToSpeechPage;
        public TextToSpeechPage()
        {
            InitializeComponent();
            textToSpeechPage = this;
        }

        private void StartClicked(object sender, EventArgs e)
        {
            try
            {
                //DependencyService.Get<ITextToSpeechService>().StartClick("Hello World");
                DependencyService.Get<ITextToSpeechGoogleServicesOne>().Speak("Hello World");
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

        public void onAndroidResultObtained(string textObtained)
        {
            try
            {
                labelSpeech.Text = textObtained;
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

        private void StopClicked(object sender, EventArgs e)
        {
            try
            {
                DependencyService.Get<ITextToSpeechService>().Talk("Hello World");
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
    }
}
