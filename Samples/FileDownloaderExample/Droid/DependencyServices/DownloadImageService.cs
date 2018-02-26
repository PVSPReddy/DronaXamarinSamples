using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using FileDownloaderExample.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(DownloadImageService))]
namespace FileDownloaderExample.Droid
{
    public class DownloadImageService : IDownloadImage
    {
        public DownloadImageService(){}

        public async Task<string> GetDownloadedImage(string url)
        {
            var webClient = new WebClient();
            string documentsPath = "";
            webClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            try
            {
                webClient.DownloadDataCompleted += (s, e) =>
                {
                    try
                    {
                        var bytes = e.Result; // get the downloaded data
                        documentsPath = Android.OS.Environment.ExternalStorageDirectory.ToString(); //GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
                        var dateTime = DateTime.Now.Ticks.ToString();
                        string localFilename = "downloaded" + dateTime + ".jpg";
                        string localPath = Path.Combine(documentsPath, localFilename);
                        File.WriteAllBytes(localPath, bytes); // writes to local storage
                                                              //localPath = String.Format("file://{0}", localPath);


                        var imgFile = new Java.IO.File(localPath);
                        //ImageSource imageSource = ImageSource.FromStream(() => Forms.Context.ContentResolver.OpenInputStream(localPath));
                        //Console.WriteLine("uri : {0}", imageSource.ToString());

                        //if (isandroidcameratrue == false)
                        //{
                        //    CameraAndGallery_Dependency_AppPage.aaa.finalselectedimagefromandroidgallery(imageSource);
                        //}
                        ImageDownloadDisplay.imageDownloadDisplay.DisplayAlert("Alert", "Download Completed", "Ok");
                        ImageDownloadDisplay.imageDownloadDisplay.LoadAndroidImage(imgFile.Path);
                    }
                    catch (Exception ex)
                    {
                        var msg = ex.Message;
                    }

                };
                var fileUrl = new Uri(url);
                webClient.DownloadDataAsync(fileUrl);
                //Task.Delay(1000);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return documentsPath;
        }
    }
}

