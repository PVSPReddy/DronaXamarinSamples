using System;
namespace FileDownloaderExample.DependencyServices
{
    public interface ISendingMessageService
    {
        void SendSMS();
        void SendMMS();
    }
}
