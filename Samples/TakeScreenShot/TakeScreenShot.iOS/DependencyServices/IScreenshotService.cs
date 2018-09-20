using System;
using CoreGraphics;
using TakeScreenShot.DependencyServices;
using TakeScreenShot.iOS.DependencyServices;
using TakeScreenShot.Views;
using UIKit;
using Xamarin.Forms;
using System.Threading.Tasks;

[assembly: Dependency(typeof(IScreenshotService))]
namespace TakeScreenShot.iOS.DependencyServices
{
    public class IScreenshotService : IScreenshot
    {
        public IScreenshotService(){}

        public async Task<byte[]> CaptureScreen()
        {
            byte[] senddata = new byte[0];
            try
            {
                var view = UIApplication.SharedApplication.KeyWindow.RootViewController.View;
                UIGraphics.BeginImageContext(view.Frame.Size);
                int screenWidth = (int)UIScreen.MainScreen.Bounds.Width;
                int screenHeight = (int)UIScreen.MainScreen.Bounds.Height;
                var yConstraint = -((screenHeight * 14) / 100);
                CGRect screenShotRect = new CGRect(0, yConstraint, screenWidth, screenHeight - yConstraint);
                view.DrawViewHierarchy(screenShotRect, true);
                var image = UIGraphics.GetImageFromCurrentImageContext();
                UIGraphics.EndImageContext();

                using (var imageData = image.AsPNG())
                {
                    var bytes = new byte[imageData.Length];
                    System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, bytes, 0, Convert.ToInt32(imageData.Length));
                    senddata = bytes;
                }
                return senddata;
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }
    }
}

