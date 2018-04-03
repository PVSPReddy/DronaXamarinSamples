using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FormsAnimations.Views
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void SubmitClicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushModalAsync(new SpeedMeterSampleTwo());
            }
            catch(Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
    }
}
