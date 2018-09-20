using System;
using System.Collections.Generic;
using TakeScreenShot.DependencyServices;
using Xamarin.Forms;

namespace TakeScreenShot.Views
{
    public partial class ScreenShotSource : ContentPage
    {
        public ScreenShotSource()
        {
            InitializeComponent();
        }

        private async void SubmitButtonCLicked(object sender, EventArgs e)
        {
            try
            {
                var imageResonse = await DependencyService.Get<IScreenshot>().CaptureScreen();
                if(imageResonse!=null)
                {
                    Navigation.PushModalAsync(new ScreenShotDisplay(imageResonse));
                }
                else
                {
                    
                }
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}
