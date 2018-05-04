using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CameraGallery
{
    public partial class TestTwoSample : ContentPage
    {
        public TestTwoSample()
        {
            InitializeComponent();

            /*
            DependencyService.Get<IPictureService>().PictureActionCompleted += (object sender, IPictureActionArgs e) =>
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine(e.LocalPictureURL);
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
                try
                {
                    image.Source = new UriImageSource()
                    {
                        CachingEnabled = false,
                        Uri = new Uri(e.LocalPictureURL)
                    };
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            };
            */
        }

        private async void SelectImage(object sender, EventArgs e)
        {
            try
            {
                string[] imageOptions = new string[]
                {
                    "Camera",
                    "Gallery",
                    "CropImage"
                };
                var imageChoice = await DisplayActionSheet("Select Image From", "Cancel", null, imageOptions);
                switch (imageChoice)
                {
                    case "Camera":
                        DependencyService.Get<IPictureService>().CapturePicture();
                        break;
                    case "Gallery":
                        DependencyService.Get<IPictureService>().SelectImage();
                        break;
                    case "CropImage":
                        DependencyService.Get<IPictureService>().CropImage();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }

        }
    }
}
