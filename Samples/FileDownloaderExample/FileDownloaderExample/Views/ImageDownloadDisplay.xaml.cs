using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FileDownloaderExample
{
    public partial class ImageDownloadDisplay : ContentPage
    {
        string offlineData="";
        List<ImageData> listImageData { get; set; }
        public static ImageDownloadDisplay imageDownloadDisplay;

        public ImageDownloadDisplay()
        {
            listImageData = new List<ImageData>()
            {
                new ImageData(){ ImageName="Satya133@gmail.com", ImageWebAddress= "http://www.devrabbit.com/incalert/uploads/profile_pics/1ee70eaf5d216947e5cde311d6ec6d40.jpg" }
            };
            //BindingContext = this;
            imageDownloadDisplay = this;
            InitializeComponent();
            listViewImageData.ItemsSource = listImageData;
        }

        public void LoadAndroidImage(string imageURL)
        {
            try
            {

                //ImageSource imageSource = ImageSource.FromStream(() => Forms.Context.ContentResolver.OpenInputStream(data.Data));
                //Console.WriteLine("uri : {0}", imageSource.ToString());

                //if (isandroidcameratrue == false)
                //{
                //    CameraAndGallery_Dependency_AppPage.aaa.finalselectedimagefromandroidgallery(imageSource);
                //}
                imageDisplay.Source = ImageSource.FromFile(imageURL);
                //imgProfile.Source = new UriImageSource()
                //{
                //    CachingEnabled = false,
                //    Uri = new Uri(imageURL)
                //};
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        private void SelectedImage(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var selectedImage = ((ListView)sender).SelectedItem as ImageData;
                if(selectedImage == null)
                {
                    return;
                }
                try
                {
                    entryImageURL.Text = selectedImage.ImageWebAddress;
                    BtnShowAll.Text = "Show All";
                    stackImageListView.IsVisible = false;
                    imageDisplay.Source = new UriImageSource()
                    {
                        CachingEnabled = true,
                        CacheValidity = TimeSpan.FromSeconds(3),
                        Uri = new Uri(selectedImage.ImageWebAddress)
                    };//FromFile(selectedImage.ImageWebAddress);
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    System.Diagnostics.Debug.WriteLine(msg + "\n" + ex.Message);
                }
                ((ListView)sender).SelectedItem = null;
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
                System.Diagnostics.Debug.WriteLine(msg+"\n"+ex.Message);
            }
        }

        private async void DownloadImageClicked(object sender, EventArgs e)
        {
            try
            {
                await DependencyService.Get<IDownloadImage>().GetDownloadedImage(entryImageURL.Text);
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
                System.Diagnostics.Debug.WriteLine(msg + "\n" + ex.Message);
            }
        }

        private void ShowFolderClicked(object sender, EventArgs e)
        {
            try
            {
                if(BtnShowAll.Text == "Show All")
                {
                    BtnShowAll.Text = "Hide";
                    stackImageListView.IsVisible = true;
                }
                else
                {
                    BtnShowAll.Text = "Show All";
                    stackImageListView.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                System.Diagnostics.Debug.WriteLine(msg + "\n" + ex.Message);
            }
        }
    }

    public class ImageData
    {
        
        public string ImageName { get; set; }

        public string ImageWebAddress { get; set; }
        
    }
}
