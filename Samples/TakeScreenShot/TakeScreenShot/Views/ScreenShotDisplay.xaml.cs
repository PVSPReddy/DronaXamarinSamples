using System;
using System.Collections.Generic;

using System.IO;

using Xamarin.Forms;

namespace TakeScreenShot.Views
{
    public partial class ScreenShotDisplay : ContentPage
    {
        public byte[] imgScreenShotBytes;
        public ScreenShotDisplay(byte[] screenShotData)
        {
            InitializeComponent();

            imgScreenShotBytes = null;
            imgScreenShotBytes = screenShotData;
            imgScreenShot.Source = null;
            imgScreenShot.Source = ImageSource.FromStream(() => new MemoryStream(imgScreenShotBytes));
        }
        private void BackButtonCLicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}
