using System;

using Xamarin.Forms;

namespace CameraGallery
{
    public class XamCustomImage : Image
    {
        public static XamCustomImage xamCustomImage;

        public XamCustomImage()
        {
            xamCustomImage = this;
            TapGestureRecognizer ImageSelected = new TapGestureRecognizer();
            ImageSelected.NumberOfTapsRequired = 1;
            ImageSelected.Tapped += SelectImage;
            GestureRecognizers.Add(ImageSelected);

            Source = ImageSource.FromFile("DummyImage.png");

            DependencyService.Get<IPictureService>().PictureActionCompleted += (object sender, IPictureActionArgs e) =>
            {
                try
                {
                    Source = ImageSource.FromFile(e.LocalPictureURL);
                    //Source = new UriImageSource()
                    //{
                    //    CachingEnabled = false,
                    //    Uri = new Uri(e.LocalPictureURL)
                    //};
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            };
        }

        public void SetImage(string URL)
        {
            try
            {
                this.Source = ImageSource.FromFile(URL);
                 
                //this.Source = new UriImageSource()
                //{
                //    CachingEnabled = false,
                //    Uri = new Uri(URL)
                //};
            }
            catch(Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

        private async void SelectImage(object sender, EventArgs e)
        {
            try
            {
                string[] imageOptions = new string[]
                {
                    "Camera",
                    "Gallery"
                };
                var imageChoice = await App.Current.MainPage.DisplayActionSheet("Select Image From", "Cancel", null, imageOptions);
                switch (imageChoice)
                {
                    case "Camera":
                        DependencyService.Get<IPictureService>().CapturePicture();
                        break;
                    case "Gallery":
                        DependencyService.Get<IPictureService>().SelectImage();
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

