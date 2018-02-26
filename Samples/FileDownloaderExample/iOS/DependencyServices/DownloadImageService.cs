using System;
using System.Threading.Tasks;
using FileDownloaderExample.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(DownloadImageService))]
namespace FileDownloaderExample.iOS
{
    public class DownloadImageService : IDownloadImage
    {
        public DownloadImageService(){}

        public async Task<string> GetDownloadedImage(string url)
        {
            try
            {
                
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
            return "";
        }
    }
}

