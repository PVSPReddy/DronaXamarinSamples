using System;
using System.IO;
using Android.Content;
using Android.Content.Res;
using FileDownloaderExample.DependencyServices;
using FileDownloaderExample.Droid.DependencyServices;
using Java.IO;
using Xamarin.Forms;


[assembly : Dependency(typeof(SendingMessageService))]
namespace FileDownloaderExample.Droid.DependencyServices
{
    public class SendingMessageService : ISendingMessageService
    {
        public SendingMessageService() { }

        public void SendMMSss()
        {
            try
            {
                Intent sendIntent = new Intent(Intent.ActionSend);
                sendIntent.SetClassName("com.android.mms", "com.android.mms.ui.ComposeMessageActivity");
                sendIntent.PutExtra("address", "1213123123");
                sendIntent.PutExtra("sms_body", "if you are sending text");

                //File file1 = new File("Icon.png");
                //using (File file1 = new File("https://assets-cdn.github.com/images/modules/site/logos/swift-logo.png"))
                using (Java.IO.File file1 = new Java.IO.File("Icon.png"))
                {
                    if (file1.Exists())
                    {
                        //File Exist
                    }
                    var uri = Android.Net.Uri.FromFile(file1);
                    sendIntent.PutExtra(Intent.ExtraStream, uri);
                    sendIntent.SetType("image/png");
                    //sendIntent.SetType("application/pdf");
                    //sendIntent.SetType("video/*");
                    Forms.Context.StartActivity(sendIntent);
                }
                //File file1 = new File(new Uri("https://assets-cdn.github.com/images/modules/site/logos/swift-logo.png"));
                //if (file1.Exists())
                //{
                //    //File Exist
                //}
                //var uri = Android.Net.Uri.FromFile(file1);
                //sendIntent.PutExtra(Intent.ExtraStream, uri);
                //sendIntent.SetType("application/pdf");
                ////sendIntent.SetType("video/*");
                //Forms.Context.StartActivity(sendIntent);
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);

            }
        }

        public void SendMMS()
        {
            try
            {
                //string fileName = "my-pdf-File--2017.pdf";
                string absFileName = "Icon.png";

                var localFolder = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
                var MyFilePath = System.IO.Path.Combine(localFolder, absFileName);

                AssetManager assets = Forms.Context.Assets;
                //using (var streamReader = new StreamReader(Insets.Open(absFileName)))
                using (var streamReader = new StreamReader(assets.Open(absFileName)))
                {
                    using (var memstream = new MemoryStream())
                    {
                        streamReader.BaseStream.CopyTo(memstream);
                        var bytes = memstream.ToArray();
                        //write to local storage
                        System.IO.File.WriteAllBytes(MyFilePath, bytes);

                        MyFilePath = $"file://{localFolder}/{absFileName}";
                        absFileName = MyFilePath;



                        Intent sendIntent = new Intent(Intent.ActionSend);
                        sendIntent.SetClassName("com.android.mms", "com.android.mms.ui.ComposeMessageActivity");
                        sendIntent.PutExtra("address", "1213123123");
                        sendIntent.PutExtra("sms_body", "if you are sending text");
                        Java.IO.File file1 = new Java.IO.File(absFileName);
                        if (file1.Exists())
                        {
                            //File Exist
                        }
                        var uri = Android.Net.Uri.FromFile(file1);
                        sendIntent.PutExtra(Intent.ExtraStream, uri);
                        sendIntent.SetType("image/png");
                        //sendIntent.SetType("application/pdf");
                        //sendIntent.SetType("video/*");
                        Forms.Context.StartActivity(sendIntent);
                    }
                }



                /*
                var fileUri = Android.Net.Uri.Parse(MyFilePath);

                var intent = new Intent();
                intent.SetFlags(ActivityFlags.ClearTop);
                intent.SetFlags(ActivityFlags.NewTask);
                intent.SetAction(Intent.ActionSend);
                intent.SetType("* /*");
                intent.PutExtra(Intent.ExtraStream, fileUri);
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);

                var chooserIntent = Intent.CreateChooser(intent, title);
                chooserIntent.SetFlags(ActivityFlags.ClearTop);
                chooserIntent.SetFlags(ActivityFlags.NewTask);
                Android.App.Application.Context.StartActivity(chooserIntent);
                */
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

        public void SendSMS()
        {
            try
            {

            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
    }
}​