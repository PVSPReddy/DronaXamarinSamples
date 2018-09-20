using System;
using System.IO;
using Android.App;
using Android.Graphics;
using TakeScreenShot.DependencyServices;
using TakeScreenShot.Droid.DependencyServices;
using TakeScreenShot.Views;
using Xamarin.Forms;
using System.Threading.Tasks;

[assembly: Dependency(typeof(IScreenshotService))]
namespace TakeScreenShot.Droid.DependencyServices
{
    public class IScreenshotService : IScreenshot
    {
        public IScreenshotService(){}

        public async Task<byte[]> CaptureScreen()
        {
            byte[] bitmapData = new byte[0];
            try
            {            
                var activity = Forms.Context as Activity;
                Android.Views.View view = activity.Window.DecorView;
                view.DrawingCacheEnabled = true;
                view.BuildDrawingCache();
                Bitmap bitmap = view.DrawingCache;
                Rect rect = new Rect();
                activity.Window.DecorView.GetWindowVisibleDisplayFrame(rect);
                int width = activity.WindowManager.DefaultDisplay.Width;
                int height = activity.WindowManager.DefaultDisplay.Height;
                var bitmapYConstraint = ((height * 14) / 100);
                var bitmapHeight = height - bitmapYConstraint;
                Bitmap screenShotBitmap = Bitmap.CreateBitmap(bitmap, 0, Convert.ToInt32(bitmapYConstraint), width, Convert.ToInt32(bitmapHeight));
                view.DestroyDrawingCache();
                using (var stream = new MemoryStream())
                {
                    screenShotBitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                    bitmapData = stream.ToArray();
                }
                return bitmapData;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }
    }
}

