using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CameraGallery
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();

        }

        private void ButtonSubmitClicked(object sender, EventArgs e)
        {
            try
            {
                if(false)
                {
                    
                }
                else
                {
                    Navigation.PushModalAsync(new TestThreeSample());
                }
            }
            catch(Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
            }
        }
    }
}
