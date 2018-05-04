using System;
using Android.Graphics;
using Android.Views;
using CameraGallery;
using CameraGallery.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly : ExportRenderer(typeof(CropOverLayCircle), typeof(CropOverLayCircleThreeRender))]
namespace CameraGallery.Droid
{
    public class CropOverLayCircleThreeRender : ViewRenderer, Android.Views.View.IOnTouchListener
    {
        CropOverLayCircle element;
        Android.Graphics.Canvas actionCanvas;
        int customHeight = 0, customWidth = 0;
        float pointerX, pointerY;
        int pointerCurrentDistance = 0, pointerPastDistance = 0;
        Android.Content.Context context;
        //Android.Widget.ImageView img;
        public CropOverLayCircleThreeRender(Android.Content.Context _context) : base(_context)
        {
            context = _context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);
            element = (CropOverLayCircle)Element;
            if (e.OldElement == null)
            {
                if ((int)Android.OS.Build.VERSION.SdkInt < 18)
                {
                    SetLayerType(Android.Views.LayerType.Hardware, null);
                }
                SetOnTouchListener(this);
                Android.Widget.ImageView imageView = new Android.Widget.ImageView(context);
                imageView.SetImageResource(Resource.Drawable.WallpaperTwo);
                //AddView(imageView);
                //SetNativeControl(imageView);

                //img = new Android.Widget.ImageView(context);
                //img.source
            }
        }

        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            try
            {
                var outCoordinates = new Android.Views.MotionEvent.PointerCoords();
                outCoordinates.X = 0;
                outCoordinates.Y = 0;
                switch (e.Action)
                {
                    case Android.Views.MotionEventActions.Down:
                        break;
                    case Android.Views.MotionEventActions.Up:
                        break;
                    case Android.Views.MotionEventActions.Move:
                        //System.Diagnostics.Debug.WriteLine("Moving Pointer");
                        if (e.PointerCount == 1)
                        {
                            pointerX = e.GetX();
                            pointerY = e.GetY();
                            System.Diagnostics.Debug.WriteLine("X-Axis" + pointerX + "Y-Axis:" + pointerY + "\n");
                            System.Diagnostics.Debug.WriteLine("Single Pointer Moving");
                        }
                        /*
                        else if (e.PointerCount == 2)
                        {
                            var p1CordsX = e.GetX(e.GetPointerId(0));
                            var p1CordsY = e.GetX(e.GetPointerId(0));

                            var p2CordsX = e.GetX(e.GetPointerId(1));
                            var p2CordsY = e.GetX(e.GetPointerId(1));

                            var radius = Math.Max((Math.Abs(p1CordsX - p2CordsX)), (Math.Abs(p1CordsY - p2CordsY)));

                            pointerCurrentDistance = Convert.ToInt32(radius);

                            if (pointerCurrentDistance < 50)
                            {
                                pointerCurrentDistance = 50;
                            }

                            customHeight = pointerCurrentDistance;
                            customWidth = pointerCurrentDistance;
                            System.Diagnostics.Debug.WriteLine("Two Pointers Moving");
                        }
                        */
                        else
                        {

                        }
                        Invalidate();

                        break;
                    case Android.Views.MotionEventActions.Pointer1Down:
                        //System.Diagnostics.Debug.WriteLine("Found Pointer");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return true;
        }

        protected override void OnDraw(Android.Graphics.Canvas canvas)
        {
            try
            {
                if (actionCanvas != null)
                {
                    actionCanvas.Restore();
                }
                actionCanvas = canvas;
                int radius = 0;
                if (customHeight == 0 && customWidth == 0)
                {
                    customWidth = 200;//Width;
                    customHeight = 200;//Height;
                    pointerX = Width / 2;
                    pointerY = Height / 2;
                    radius = Math.Min(customWidth, customHeight) / 2;
                }
                else
                {
                    radius = Math.Min(customWidth, customHeight);
                }

                pointerPastDistance = radius;
                var strokeWidth = 10;
                radius -= strokeWidth / 2;

                actionCanvas.Restore();

                Path path = new Path();
                path.AddCircle(pointerX, pointerY, radius, Path.Direction.Ccw);
                path.SetFillType(Path.FillType.InverseEvenOdd);
                var mSemiBlackPaint = new Paint();
                //mSemiBlackPaint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.SrcIn));
                mSemiBlackPaint.Color = global::Android.Graphics.Color.Transparent;
                mSemiBlackPaint.StrokeWidth = 10;

                actionCanvas.DrawPath(path, mSemiBlackPaint);
                actionCanvas.ClipPath(path);

                #region for bit map android
                /*
                Android.Graphics.Drawables.Drawable icon;
                if ((int)Android.OS.Build.VERSION.SdkInt >= 21)
                {
                    //Android.Content.Context context = GetApplicationContext();
                    //Android.Resource.Drawable drawable = context.GetDrawable(Resource.Drawable.WallpaperTwo);
                    //// convert drawable to bitmap
                    //Bitmap bitmap = ((BitmapDrawable)drawable).getBitmap();
                    //// convert bitmap to drawable
                    //Drawable d = new BitmapDrawable(bitmap);
                    icon = Android.Support.V4.Content.Res.ResourcesCompat.GetDrawable(Resources, Resource.Drawable.WallpaperTwo, null);
                }
                else
                {
                    icon = Resources.GetDrawable(Resource.Drawable.WallpaperTwo);
                }

                Bitmap bp = ((Android.Graphics.Drawables.BitmapDrawable)icon).Bitmap;
                Paint paint = new Paint();
                Rect rect = new Rect(0, 0, bp.Width, bp.Height);

                paint.AntiAlias = false;
                //canvas.drawARGB(0, 0, 0, 0);
                //paint.setColor(color);
                actionCanvas.DrawBitmap(bp, rect, rect, paint);
                //Bitmap bp = BitmapFactory.DecodeResource(icon, Resource.Drawable.WallpaperTwo);
                */
                #endregion

                actionCanvas.DrawColor(global::Android.Graphics.Color.ParseColor("#A6000000"));

                mSemiBlackPaint.Dispose();
                path.Dispose();

            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            base.OnDraw(actionCanvas);
        }
    }
}



