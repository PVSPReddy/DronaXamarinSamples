using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TextToSpeechToText
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();

        }

        private void StartNavigationClicked(object sender, EventArgs e)
        {
            try
            {
                //Navigation.PushModalAsync(new SpeechToTextPage());
                Navigation.PushModalAsync(new TextToSpeechPage());
            }
            catch(Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
    }
}
