using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CameraGallery.Droid
{
    //[Activity(Label = "CropServiceActivity")]
    [Activity(Label = "Crop Selected Image")]
    public class CropServiceActivityTwo : Activity
    {
        Bitmap imageBitmap;
        ImageView image;
        RelativeLayout relativeOverView;
        CropOverLayCircleViewTwo overlay;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //var height = (App.screenHeight * 1) / 100;
            //var width = (App.screenWidth * 1) / 100;

            var height = ((Resources.DisplayMetrics.HeightPixels) * 1) / 100;
            var width = ((Resources.DisplayMetrics.WidthPixels) * 1) / 100;

            // Create your application here
            #region for UI Part of the crop layout
            #region for bit map android
            try
            {
                Android.Graphics.Drawables.Drawable icon;
                if ((int)Android.OS.Build.VERSION.SdkInt >= 21)
                {
                    icon = this.GetDrawable(Resource.Drawable.WallpaperTwo);
                    //icon = Android.Support.V4.Content.Res.ResourcesCompat.GetDrawable(Resources, Resource.Drawable.WallpaperTwo, null);
                }
                else
                {
                    icon = Resources.GetDrawable(Resource.Drawable.WallpaperTwo);
                }

                imageBitmap = ((Android.Graphics.Drawables.BitmapDrawable)icon).Bitmap;
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            #endregion

            #region for image, crop overlay
            relativeOverView = new RelativeLayout(this);
            relativeOverView.SetBackgroundColor(Android.Graphics.Color.Orange);
            relativeOverView.SetMinimumHeight(height * 70);
            relativeOverView.SetMinimumWidth(width * 70);
            var relativeOverViewParms = new LinearLayout.LayoutParams((ViewGroup.LayoutParams.MatchParent), (ViewGroup.LayoutParams.MatchParent), 1);
            relativeOverView.LayoutParameters = relativeOverViewParms;
            //relativeOverView.SetGravity(GravityFlags.Center);
            relativeOverView.Focusable = false;
            relativeOverView.Clickable = false;

            image = new ImageView(this);
            if (imageBitmap != null)
            {
                image.SetImageBitmap(imageBitmap);
            }
            else
            {
                image.SetImageResource(Resource.Drawable.WallpaperTwo);//image.SetImageResource(Resource.Drawable.icon);
            }
            //image.SetMinimumHeight(height * 70);
            //image.SetMinimumWidth(width * 70);
            image.Focusable = false;
            image.Clickable = false;
            var imageHolderView = new LinearLayout(this);
            imageHolderView.SetMinimumHeight(height * 60);
            imageHolderView.SetMinimumWidth(width * 80);
            //var imageHolderViewParms = new LinearLayout.LayoutParams((width * 80), (height * 60));
            var imageHolderViewParms = new LinearLayout.LayoutParams((ViewGroup.LayoutParams.MatchParent), (ViewGroup.LayoutParams.MatchParent));
            imageHolderView.SetGravity(GravityFlags.Center);
            imageHolderView.Orientation = Orientation.Vertical;
            imageHolderView.LayoutParameters = imageHolderViewParms;
            imageHolderView.Focusable = false;
            imageHolderView.Clickable = false;
            imageHolderView.AddView(image);
            relativeOverView.AddView(imageHolderView);

            overlay = new CropOverLayCircleViewTwo(this);
            //overlay.SetBackgroundColor(Android.Graphics.Color.Yellow);
            //overlay.SetMinimumHeight(height * 70);
            //overlay.SetMinimumWidth(width * 70);
            overlay.Focusable = true;
            overlay.Clickable = true;
            var overlayHolderView = new LinearLayout(this);
            //overlayHolderView.SetBackgroundColor(Android.Graphics.Color.Blue);
            overlayHolderView.SetMinimumHeight(height * 60);
            overlayHolderView.SetMinimumWidth(width * 80);
            //var overlayHolderViewParms = new LinearLayout.LayoutParams((width * 80), (height * 60));
            var overlayHolderViewParms = new LinearLayout.LayoutParams((ViewGroup.LayoutParams.MatchParent), (ViewGroup.LayoutParams.MatchParent));
            overlayHolderView.SetGravity(GravityFlags.Center);
            overlayHolderView.Orientation = Orientation.Vertical;
            overlayHolderView.LayoutParameters = overlayHolderViewParms;
            overlayHolderView.Focusable = false;
            overlayHolderView.Clickable = false;
            overlayHolderView.AddView(overlay);
            relativeOverView.AddView(overlayHolderView);
            #endregion

            #region for Buttons at the bottom
            var buttonHolderView = new LinearLayout(this);
            //overlayHolderView.SetBackgroundColor(Android.Graphics.Color.Blue);
            var buttonHolderViewParms = new LinearLayout.LayoutParams((ViewGroup.LayoutParams.MatchParent), (ViewGroup.LayoutParams.WrapContent));
            buttonHolderView.SetGravity(GravityFlags.Center);
            buttonHolderView.Orientation = Orientation.Horizontal;
            buttonHolderView.LayoutParameters = buttonHolderViewParms;
            buttonHolderView.Focusable = false;
            buttonHolderView.Clickable = false;

            Button cancelButton = new Button(this);
            cancelButton.Focusable = true;
            cancelButton.Clickable = true;
            cancelButton.Text = "Finish";
            cancelButton.SetMinimumHeight(height * 10);
            cancelButton.SetMinimumWidth(width * 70);
            cancelButton.Click += ButtonCancelClicked;
            buttonHolderView.AddView(cancelButton);

            Button saveButton = new Button(this);
            saveButton.Focusable = true;
            saveButton.Clickable = true;
            saveButton.Text = "Save";
            saveButton.SetMinimumHeight(height * 10);
            saveButton.SetMinimumWidth(width * 70);
            saveButton.Click += ButtonSaveClicked;
            buttonHolderView.AddView(saveButton);
            #endregion

            #region for main holder view
            var mainView = new LinearLayout(this);
            mainView.Orientation = Orientation.Vertical;
            mainView.SetBackgroundColor(Android.Graphics.Color.Green);
            //mainView.SetMinimumHeight(height * 85);
            //mainView.SetMinimumWidth(width * 80);
            mainView.Focusable = false;
            mainView.Clickable = false;
            var mainViewParms = new LinearLayout.LayoutParams((ViewGroup.LayoutParams.MatchParent), (ViewGroup.LayoutParams.MatchParent));
            mainView.SetGravity(GravityFlags.Center);
            mainView.LayoutParameters = mainViewParms;
            mainView.AddView(relativeOverView);
            mainView.AddView(buttonHolderView);

            SetContentView(mainView);
            #endregion

            #endregion
        }

        #region for button clickevents and logical methods
        private async void ButtonSaveClicked(object sender, EventArgs e)
        {
            try
            {
                if(overlay != null)
                {
                    await overlay.CropTheHighlightedRegion(imageBitmap, ((image.Width - relativeOverView.Width)/2), ((image.Height - relativeOverView.Height)/2));
                }
                await ClearResidue();
                Finish();
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

        private async void ButtonCancelClicked(object sender, EventArgs e)
        {
            try
            {
                await ClearResidue();
                Finish();
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

        public async System.Threading.Tasks.Task<bool> ClearResidue()
        {
            try
            {
                if (imageBitmap != null)
                {
                    imageBitmap.Recycle();
                    imageBitmap = null;
                    System.GC.Collect();
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            return true;
        }
        #endregion
    }



    public class CropOverLayCircleViewTwo : View//, Android.Views.View.IOnTouchListener
    {
        CropOverLayCircle element;
        Android.Graphics.Canvas actionCanvas;
        int customHeight = 0, customWidth = 0;
        float pointerX, pointerY;
        int pointerCurrentDistance = 0, pointerPastDistance = 0, cutRadius;
        Android.Content.Context context;
        Path cutPath;
        //Android.Widget.ImageView img;
        public CropOverLayCircleViewTwo(Android.Content.Context _context) : base(_context)
        {
            context = _context;
            if ((int)Android.OS.Build.VERSION.SdkInt < 18)
            {
                SetLayerType(Android.Views.LayerType.Hardware, null);
            }
        }

        #region for touch event and movable events
        public override bool OnTouchEvent(MotionEvent e)
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
            //return true;
            return base.OnTouchEvent(e);
        }
        #endregion

        #region for touch event not working here but works with renders
        /*
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
                        *\/
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
        */
        #endregion

        #region to draw the canavas with the high lighted view
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
                cutRadius = radius;

                actionCanvas.Restore();

                Path path = new Path();
                //path.AddCircle(pointerX, pointerY, radius, Path.Direction.Ccw);
                //path.AddRect(new RectF(((pointerX + cutRadius) / 2), ((pointerY + cutRadius) / 2), (pointerX + cutRadius), (pointerY + cutRadius)), Path.Direction.Ccw);
                //path.AddRect(new RectF(Math.Abs(pointerX - (cutRadius / 2)), Math.Abs(pointerY - (cutRadius / 2)), (pointerX + cutRadius), (pointerY + cutRadius)), Path.Direction.Ccw);
                path.AddRect(new RectF((pointerX - (cutRadius / 2)), (pointerY - (cutRadius / 2)), (pointerX + cutRadius), (pointerY + cutRadius)), Path.Direction.Ccw);

                path.SetFillType(Path.FillType.InverseEvenOdd);

                var mSemiBlackPaint = new Paint();
                //mSemiBlackPaint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.SrcIn));
                mSemiBlackPaint.Color = global::Android.Graphics.Color.Transparent;
                mSemiBlackPaint.StrokeWidth = 10;

                actionCanvas.DrawPath(path, mSemiBlackPaint);
                actionCanvas.ClipPath(path);

                if (cutPath != null)
                {
                    cutPath.Dispose();
                    cutPath = null;
                    cutPath = path;
                }
                else
                {
                    cutPath = path;
                }

                #region for bit map android
                /*//
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
                *///
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
        /*
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
                cutRadius = radius;

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

                if (cutPath != null)
                {
                    cutPath.Dispose();
                    cutPath = null;
                    cutPath = path;
                }
                else
                {
                    cutPath = path;
                }

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
                *\/
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
        */
        #endregion

        #region to crop the image

        public async System.Threading.Tasks.Task<bool> CropTheHighlightedRegion(Bitmap sourceBitmap, float v1, float v2)
        {
            var _file = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "CroppedImage.jpg");
            var _uuuri = _file.ToString();
            try
            {
                //Bitmap output = Bitmap.CreateBitmap(cutRadius, cutRadius, Bitmap.Config.Argb8888);
                Bitmap output = Bitmap.CreateBitmap(cutRadius, cutRadius, Bitmap.Config.Rgb565);

                //Bitmap output = Bitmap.CreateBitmap(sourceBitmap.Width, sourceBitmap.Height, Bitmap.Config.Rgb565);
                Canvas canvas = new Canvas(output);
                Paint paint = new Paint(PaintFlags.AntiAlias);
                paint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.SrcIn));

                int srcx = (Convert.ToInt32(pointerX - v1));//Math.Abs((Convert.ToInt32(pointerX - (cutRadius / 2))));//(Convert.ToInt32(pointerX));//Math.Abs((Convert.ToInt32(pointerX - (cutRadius / 2))));//0;
                int srcy = (Convert.ToInt32(pointerY - v2));//Math.Abs((Convert.ToInt32(pointerY - (cutRadius / 2))));//(Convert.ToInt32(pointerY));//Math.Abs((Convert.ToInt32(pointerY - (cutRadius / 2))));//0;
                int srcWidth = cutRadius;//cutRadius;//720;//sourceBitmap.Width;
                int srcHeight = cutRadius;//cutRadius;//1280;//sourceBitmap.Height;
                var srcRect = new Rect(srcx, srcy, srcWidth, srcHeight);

                int dstx = 0;//(sourceBitmap.Width / 2);//(Convert.ToInt32(pointerX));//Math.Abs((Convert.ToInt32(pointerX - (cutRadius / 2))));//0;
                int dsty = 0;//(sourceBitmap.Height / 2);//(Convert.ToInt32(pointerY));//Math.Abs((Convert.ToInt32(pointerY - (cutRadius / 2))));//0;
                int dstWidth = cutRadius;//cutRadius;//720;//sourceBitmap.Width;
                int dstHeight = cutRadius;//cutRadius;//1280;//sourceBitmap.Height;
                var dstRect = new Rect(dstx, dsty, dstWidth, dstHeight);

                canvas.DrawBitmap(sourceBitmap, null, dstRect, null);


                //canvas.DrawBitmap(sourceBitmap, new Rect(0, 0, sourceBitmap.Width, sourceBitmap.Height), new Rect(0, 0, sourceBitmap.Width, sourceBitmap.Height), paint);
                //canvas.DrawColor(global::Android.Graphics.Color.ParseColor("#A0000000"));

                //canvas.DrawPath(cutPath, paint);
                //canvas.ClipPath(cutPath);

                #region to save a bitmap
                try
                {
                    using (System.IO.Stream stream = System.IO.File.Create(_uuuri))
                    {
                        //var filedone = resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 30, stream);
                        var filedone = output.Compress(Bitmap.CompressFormat.Jpeg, 90, stream);
                        try
                        {
                            PictureActionArgs args = new PictureActionArgs()
                            {
                                LocalPictureURL = _uuuri
                            };
                            System.Diagnostics.Debug.WriteLine(_uuuri);
                            XamCustomImage.xamCustomImage.SetImage(_uuuri);
                        }
                        catch (Exception ex)
                        {
                            var msg = "Line 217 :\n" + ex.Message + "\n" + ex.StackTrace;
                            System.Diagnostics.Debug.WriteLine(msg);
                        }
                        if (sourceBitmap != null)
                        {
                            sourceBitmap.Recycle();
                            sourceBitmap = null;
                            output.Recycle();
                            output = null;
                            //options = null;
                            System.GC.Collect();
                        }
                    }
                }
                catch (Exception ex)
                {
                    var msg = ex.Message + "\n" + ex.StackTrace;
                    System.Diagnostics.Debug.WriteLine(msg);
                }
                if (sourceBitmap != null || output != null)
                {
                    sourceBitmap.Recycle();
                    sourceBitmap = null;
                    if (output != null)
                    {
                        output.Recycle();
                        output = null;
                    }
                    //options = null;
                    System.GC.Collect();
                }
                paint.Dispose();
                cutPath.Dispose();
                //path.Dispose();
                #endregion
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }

            return true;
        }
        #endregion

    }
}
