using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TextToSpeechToText
{
    public partial class SpeechToTextPage : ContentPage
    {
        public static SpeechToTextPage speechToTextPage;
        public SpeechToTextPage()
        {
            InitializeComponent();
            speechToTextPage = this;
        }

        private void StartClicked(object sender, EventArgs e)
        {
            try
            {
                //DependencyService.Get<ISpeechToTextService>().StartClick();
                DependencyService.Get<ISpeechToTextGoogleServiceOne>().StartClick();
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
                
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
    }
}
