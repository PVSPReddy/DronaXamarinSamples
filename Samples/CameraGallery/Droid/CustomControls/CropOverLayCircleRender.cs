using System;
using Android.Graphics;
using Android.Views;
using CameraGallery;
using CameraGallery.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly : ExportRenderer(typeof(CropOverLayCircle), typeof(CropOverLayCircleRender))]
namespace CameraGallery.Droid
{
    public class CropOverLayCircleRender : ViewRenderer, Android.Views.View.IOnTouchListener
    {
        CropOverLayCircle element;
        Android.Graphics.Canvas actionCanvas;
        int customHeight = 0, customWidth = 0;
        float pointerX, pointerY;
        int pointerCurrentDistance = 0, pointerPastDistance = 0;
        public CropOverLayCircleRender(Android.Content.Context _context) : base(_context)
        {
            
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
                    //SetLayerType(LayerType.Software, null);
                }
                SetOnTouchListener(this);
            }
        }

        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            try
            {
                var outCoordinates = new Android.Views.MotionEvent.PointerCoords();
                outCoordinates.X = 0;
                outCoordinates.Y = 0;
                //System.Diagnostics.Debug.WriteLine(e.PointerCount.ToString());
                //if (e.PointerCount == 2)
                //{
                //    pointer1 = e.GetPointerId(0);
                //    pointer2 = e.GetPointerId(1);
                //    //e.GetPointerCoords(pointer1, outCoordinates);
                //}
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



                            //pointerX = ((p1CordsX + p2CordsX)/2);
                            //pointerY = ((p1CordsY + p2CordsY)/2);

                            System.Diagnostics.Debug.WriteLine("Two Pointers Moving");

                            //Path path = new Path();
                            //if(pointerCurrentDistance < 50)
                            //{
                            //    pointerCurrentDistance = 50;
                            //}
                            //customHeight = pointerCurrentDistance;
                            //customWidth = pointerCurrentDistance;
                            //if(pointerPastDistance == 0)
                            //{
                            //    //System.Diagnostics.Debug.WriteLine("pointerPastDistance == 0");
                            //    //path.AddCircle(Width / 2, Height / 2, radius, Path.Direction.Ccw);
                            //}
                            //else if(pointerPastDistance > pointerCurrentDistance)
                            //{
                            //    System.Diagnostics.Debug.WriteLine("pointerPastDistance is more");

                            //    //path.AddCircle(Width / 2, Height / 2, radius, Path.Direction.Ccw);
                            //}
                            //else
                            //{
                            //    System.Diagnostics.Debug.WriteLine("pointerPastDistance is less");
                            //    //path.AddCircle(Width / 2, Height / 2, radius, Path.Direction.Ccw);
                            //}
                        }
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
                actionCanvas = canvas;
                int radius = 0;
                if (customHeight == 0 && customWidth == 0)
                {
                    customWidth = Width;
                    customHeight = Height;
                    pointerX = Width / 2;
                    pointerY = Height / 2;
                    radius = Math.Min(customWidth, customHeight) / 2;
                }
                else
                {
                    radius = Math.Min(customWidth, customHeight);
                }
                //var radius = Math.Min(Width, Height) / 2;

                pointerPastDistance = radius;
                var strokeWidth = 10;
                radius -= strokeWidth / 2;

                actionCanvas.Restore();
                //canvas.Restore();

                //var mTransparentPaint = new Paint();
                //mTransparentPaint.Color = global::Android.Graphics.Color.Transparent;
                //mTransparentPaint.StrokeWidth = 10;
                //canvas.DrawCircle(Width / 2, Height / 2, radius, mTransparentPaint);


                Path path = new Path();
                //path.AddCircle(Width / 2, Height / 2, radius, Path.Direction.Ccw);
                path.AddCircle(pointerX, pointerY, radius, Path.Direction.Ccw);
                path.SetFillType(Path.FillType.InverseEvenOdd);
                var mSemiBlackPaint = new Paint();
                mSemiBlackPaint.Color = global::Android.Graphics.Color.Transparent;
                mSemiBlackPaint.StrokeWidth = 10;

                actionCanvas.DrawPath(path, mSemiBlackPaint);
                actionCanvas.ClipPath(path);
                //canvas.DrawPath(path, mSemiBlackPaint);
                //canvas.ClipPath(path);

                actionCanvas.DrawColor(global::Android.Graphics.Color.ParseColor("#A6000000"));
                //canvas.DrawColor(global::Android.Graphics.Color.ParseColor("#A6000000"));
                mSemiBlackPaint.Dispose();
                path.Dispose();
                //actionCanvas = canvas;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            base.OnDraw(actionCanvas);
        }


        /*

        public override bool OnTouchEvent(Android.Views.MotionEvent e)
        {
            /*
            var outCoordinates = new Android.Views.MotionEvent.PointerCoords();
            outCoordinates.X = 0;
            outCoordinates.Y = 0;
            System.Diagnostics.Debug.WriteLine(e.PointerCount.ToString());
            if (e.PointerCount == 2)
            {
                pointer1 = e.GetPointerId(0);
                pointer2 = e.GetPointerId(1);

                //e.GetPointerCoords(pointer1, outCoordinates);
            }
            switch (e.Action)
            {
                case Android.Views.MotionEventActions.Down:
                    break;
                case Android.Views.MotionEventActions.Up:
                    break;
                case Android.Views.MotionEventActions.Move:
                    System.Diagnostics.Debug.WriteLine("Moving Pointer");
                    break;
                case Android.Views.MotionEventActions.Pointer1Down:
                    System.Diagnostics.Debug.WriteLine("Found Pointer");
                    break;
                default:
                    break;
            }
            *
            return base.OnTouchEvent(e);
        }

        protected override bool DrawChild(Android.Graphics.Canvas canvas, Android.Views.View child, long drawingTime)
        {
            try
            {
                var radius = Math.Min(Width, Height) / 2;
                var strokeWidth = 10;
                radius -= strokeWidth / 2;


                Path path = new Path();
                //path.AddRect();
                path.AddCircle(Width, Height, radius, Path.Direction.Ccw);
                canvas.Save();
                canvas.ClipPath(path);

                var result = base.DrawChild(canvas, child, drawingTime);

                canvas.Restore();

                path = new Path();
                path.AddCircle(Width / 2, Height / 2, radius, Path.Direction.Ccw);

                var paint = new Paint();
                paint.ClearShadowLayer();
                paint.AntiAlias = true;
                paint.StrokeWidth = 5;
                paint.SetStyle(Paint.Style.Stroke);
                paint.Color = global::Android.Graphics.Color.Black;
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
        */
    }
}

