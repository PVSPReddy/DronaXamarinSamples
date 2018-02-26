using System;
using System.Threading.Tasks;

namespace FileDownloaderExample
{
    public interface IDownloadImage
    {
        Task<string> GetDownloadedImage(string url);
    }
}
