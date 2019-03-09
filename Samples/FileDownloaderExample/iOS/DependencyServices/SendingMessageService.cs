using System;
using FileDownloaderExample.DependencyServices;
using FileDownloaderExample.iOS.DependencyServices;
using MessageUI;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SendingMessageService))]
namespace FileDownloaderExample.iOS.DependencyServices
{
    public class SendingMessageService : UIViewController, ISendingMessageService
    {
        public SendingMessageService() { }

        public void SendMMS()
        {
            try
            {
                var messageController = new MFMessageComposeViewController();

                //Verify app can send text message             
                if (MFMessageComposeViewController.CanSendText)
                {
                    messageController.Body = "Here's an image for u 2 n joy.";

                    //Add attachment as NSData, and set the uti   
                    var ttt = UIImage.FromFile("Icon.png").AsPNG(); //new UIImage("Icon.png");
                    messageController.AddAttachment(ttt, "kUTTypePNG", "image.png");
                    //messageController.AddAttachment(UIImage.FromFile("Icon.png").AsPNG(), "kUTTypePNG", "image.png");

                    //messageController.Finished += (sender, e) => (DismissViewController(true, null));
                    //this.PresentViewController(messageController, true, () => ViewModel.IsBusy = false);
                    //messageController.Finished += (sender, e) =>
                    //{
                    //    this.PresentViewController(messageController, true, DismissViewController())
                    //};
                    //this.PresentViewController(messageController, true, () => ViewModel.IsBusy = false);

                    messageController.Finished += (sender, e) =>
                    {
                        e.Controller.DismissViewController(true, null);
                        //tcs.SetResult(e.Result == MFMailComposeResult.Sent);
                    };
                    //this.PresentViewController(messageController, true, null);
                    UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(messageController, true, null);

                    //var rootController = ((AppDelegate)(UIApplication.SharedApplication.Delegate)).Window.RootViewController.ChildViewControllers[0].ChildViewControllers[0].ChildViewControllers[0];
                    //var navcontroller = rootController as UINavigationController;
                    //if (navcontroller != null)
                    //    rootController = navcontroller.VisibleViewController;
                    //rootController.PresentViewController(messageController, true, null);
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

        public void SendMessage()
        {
            UIWindow window = UIApplication.SharedApplication.KeyWindow;
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
}                 