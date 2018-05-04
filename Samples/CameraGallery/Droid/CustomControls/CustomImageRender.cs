using System;
using Android.Graphics;
using CameraGallery;
using CameraGallery.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

//[assembly : ExportRenderer(typeof(XamCustomImage), typeof(CustomImageRender))]
namespace CameraGallery.Droid
{
    public class CustomImageRender : ImageRenderer
    {
        Android.Content.Context context;
        XamCustomImage element;


        public CustomImageRender(Android.Content.Context _context) : base(_context)
        {
            context = _context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            element = (XamCustomImage)Element;
            if (e.OldElement == null)
            {

                if ((int)Android.OS.Build.VERSION.SdkInt < 18)
                {
                    SetLayerType(Android.Views.LayerType.Software, null);
                }
            }
        }

        protected override bool DrawChild(Android.Graphics.Canvas canvas, global::Android.Views.View child, long drawingTime)
        {
            try
            {
                var radius = Math.Min(Width, Height) / 2;
                var strokeWidth = 10;
                radius -= strokeWidth / 2;


                Path path = new Path();
                path.AddCircle(Width / 2, Height / 2, radius, Path.Direction.Ccw);
                canvas.Save();
                canvas.ClipPath(path);

                var result = base.DrawChild(canvas, child, drawingTime);

                canvas.Restore();

                path = new Path();
                path.AddCircle(Width / 2, Height / 2, radius, Path.Direction.Ccw);

                var paint = new Paint();
                paint.AntiAlias = true;
                paint.StrokeWidth = 5;
                paint.SetStyle(Paint.Style.Stroke);
                //try
                //{
                //    if (element.ImageName == "menuProfile")
                //    {
                //        paint.Color = global::Android.Graphics.Color.Black;
                //    }
                //    else
                //    {
                //        paint.Color = global::Android.Graphics.Color.White;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    var msg = ex.Message;
                //    paint.Color = global::Android.Graphics.Color.White;
                //}

                canvas.DrawPath(path, paint);

                paint.Dispose();
                path.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

            return base.DrawChild(canvas, child, drawingTime);
        }

        protected override void OnDraw(Android.Graphics.Canvas canvas)
        {
            base.OnDraw(canvas);
        }
    }
}

