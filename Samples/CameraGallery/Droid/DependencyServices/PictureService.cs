using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Database;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using CameraGallery.Droid;
using Java.IO;
using Java.Util.Regex;
using Xamarin.Forms;

[assembly : Dependency(typeof(PictureService))]
namespace CameraGallery.Droid
{
    public class PictureActionArgs : EventArgs, IPictureActionArgs
    {
        public string LocalPictureURL { get; set; }
    }

    public class PictureService : IPictureService
    {
        static readonly File file = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "tmp.jpg");

        public event EventHandler<IPictureActionArgs> PictureActionCompleted;

        //public static PictureService pictureService;

        //public static void AccessEvent(IPictureActionArgs args)
        //{
        //    AccessingEvent(args);
        //}

        //public void AccessingEvent(IPictureActionArgs args)
        //{
        //    PictureActionCompleted(this, args);
        //}

        public void CapturePicture()
        {
            var activity = Forms.Context as Activity;

            try
            {
                //pictureService = this;
                var isCameraAvailable = activity.PackageManager.HasSystemFeature(PackageManager.FeatureCamera);//use the Android.Content.PM
                if (isCameraAvailable)
                {
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.Gingerbread)
                    {
                        isCameraAvailable |= activity.PackageManager.HasSystemFeature(PackageManager.FeatureCameraFront);
                    }
                    try
                    {
                        var intent = new Intent(activity, typeof(MediaActivity));
                        intent.PutExtra("id", 1);
                        activity.StartActivity(intent);
                    }
                    catch (Exception ex)
                    {
                        var msg = ex.Message;
                    }

                }

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        //public Task<ForLargeImages> CapturePictureDroid(MyTrailCameraOne mtco)
        //{
        //    throw new NotImplementedException();
        //}

        public void SelectImage()
        {
            var activity = Forms.Context as Activity;
            try
            {
                var intent = new Intent(activity, typeof(MediaActivity));
                intent.PutExtra("id", 2);
                activity.StartActivity(intent);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public void CropImage()
        {
            var activity = Forms.Context as Activity;
            try
            {
                var intent = new Intent(activity, typeof(CropServiceActivityTwo));
                intent.PutExtra("id", 1);
                activity.StartActivity(intent);
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }

        //public Task<ForLargeImages> SelectImageDroid(MyTrailCameraOne mtco)
        //{
        //    throw new NotImplementedException();
        //}
    }


    [Activity(Label = "MediaActivity")]
    public class MediaActivity : Activity
    {
        public event EventHandler<IPictureActionArgs> PictureActionCompleted;

        static readonly File file = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "tmp.jpg");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            int _id = Intent.GetIntExtra("id", 0);
            Intent intent = new Intent();
            if (_id == 1)
            {
                intent.SetAction(MediaStore.ActionImageCapture);
                intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(file));
                StartActivityForResult(intent, _id);
            }
            else if (_id == 2)
            {

                if (Build.VERSION.SdkInt < BuildVersionCodes.Kitkat)
                {
                    intent = new Intent();
                    intent.SetType("image/*");
                    intent.SetAction(Intent.ActionGetContent);
                    StartActivityForResult(Intent.CreateChooser(intent, "Select Picture"), 2);
                }
                else
                {
                    intent = new Intent();
                    intent.SetType("image/*");
                    intent.SetAction(Intent.ActionOpenDocument);
                    intent.AddCategory(Intent.CategoryOpenable);
                    StartActivityForResult(Intent.CreateChooser(intent, "Select Picture"), 2);
                }
            }
            else
            {
                Finish();
            }
        }

        protected async override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            try
            {
                string uuuri = file.ToString();
                base.OnActivityResult(requestCode, resultCode, data);
                if (requestCode == 2)
                {
                    if (resultCode == Result.Ok)
                    {
                        try
                        {
                            var uri1 = data.Data;
                            uuuri = await getRealPathFromURI(this, uri1);
                            PictureActionArgs args = new PictureActionArgs()
                            {
                                LocalPictureURL = uuuri
                            };
                            XamCustomImage.xamCustomImage.SetImage(uuuri);
                            //PictureActionCompleted(this, args);
                        }
                        catch (Exception ex)
                        {
                            var msg = "Line 217 :\n" + ex.Message + "\n" + ex.StackTrace;
                            System.Diagnostics.Debug.WriteLine(msg);
                            Finish();
                        }
                    }
                }

                else if (requestCode == 1)
                {
                    if (resultCode == Result.Ok)
                    {
                        uuuri = file.ToString();
                    }
                    else
                    {
                        //mid.ShowImage(imageSource);
                    }
                }

                #region 
                var _file = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "Display.jpg");
                var _uuuri = _file.ToString();
                Bitmap originalImage;
                BitmapFactory.Options options = new BitmapFactory.Options();
                //options.InPreferredConfig = Android.Graphics.Bitmap.Config.Rgb565;// this decreases the image temp size and avoides exception out of memory
                options.InJustDecodeBounds = true;
                //options.InTempStorage = new byte[16 * 1024]; //use if any error occurs more frequently
                originalImage = BitmapFactory.DecodeFile(uuuri, options);
                //options.InSampleSize = 2;

                #region For Screen Height & Width
                var pixels = Resources.DisplayMetrics.WidthPixels;
                var scale = Resources.DisplayMetrics.Density;

                var dps = (double)((pixels - 0.5f) / scale);

                int reqWidth = ((int)dps) - 20;

                pixels = Resources.DisplayMetrics.HeightPixels;
                dps = (double)((pixels - 0.5f) / scale);

                int reqHeight = ((int)dps) - 20;
                #endregion

                int actualWidth = options.OutWidth;
                int actualHeight = options.OutHeight;
                options.InSampleSize = await CalculateInSampleSize(options, reqHeight, reqWidth);
                options.InJustDecodeBounds = false;

                originalImage = BitmapFactory.DecodeFile(uuuri, options);
                //Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, reqWidth, reqHeight, false);
                Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, reqWidth, reqHeight, true);
                using (System.IO.Stream stream = System.IO.File.Create(_uuuri))
                {
                    //var filedone = resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 30, stream);
                    var filedone = resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 90, stream);
                    try
                    {
                        PictureActionArgs args = new PictureActionArgs()
                        {
                            LocalPictureURL = _uuuri
                        };
                        XamCustomImage.xamCustomImage.SetImage(_uuuri);
                        //PictureActionCompleted(this, args);

                        /*
                        PictureActionArgs args = new PictureActionArgs()
                        {
                            LocalPictureURL = uuuri
                        };
                        XamCustomImage.xamCustomImage.SetImage(uuuri);
                        //PictureActionCompleted(this, args);
                        */
                    }
                    catch(Exception ex)
                    {
                        var msg = "Line 217 :\n" +ex.Message + "\n" + ex.StackTrace;
                        System.Diagnostics.Debug.WriteLine(msg);
                    }
                    if (originalImage != null)
                    {
                        originalImage.Recycle();
                        originalImage = null;
                        options = null;
                        System.GC.Collect();
                    }
                }
                Finish();
                #endregion
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
                Finish();
            }
        }

        public async Task<int> CalculateInSampleSize(BitmapFactory.Options options, int reqHeight, int reqWidth)
        {
            int height = options.OutHeight;
            int width = options.OutWidth;
            int inSampleSize = 1;
            if (height > reqHeight || width > reqWidth)
            {

                int halfHeight = height / 2;
                int halfWidth = width / 2;

                while ((halfHeight / inSampleSize) >= reqHeight && (halfWidth / inSampleSize) >= reqWidth)
                {
                    inSampleSize *= 2;
                }
            }

            return inSampleSize;
        }


        public async Task<string> getRealPathFromURI(Context context, Android.Net.Uri contentURI)
        {
            try
            {
                var fixedUri = FixUri(contentURI.Path);

                if (fixedUri != null)
                {
                    contentURI = fixedUri;
                }

                if (contentURI.Scheme == "file")
                {
                    var _filepath = new System.Uri(contentURI.ToString()).LocalPath;
                    //mid.ShowImageAndroid(_filepath);
                    return _filepath;
                }
                else if (contentURI.Scheme == "content")
                {
                    ICursor cursor = ContentResolver.Query(contentURI, null, null, null, null);
                    try
                    {
                        string contentPath = null;
                        cursor.MoveToFirst();
                        string[] projection = new[] { MediaStore.Images.Media.InterfaceConsts.Data };
                        String document_id = cursor.GetString(0);
                        document_id = document_id.Substring(document_id.LastIndexOf(":") + 1);
                        cursor.Close();

                        cursor = ContentResolver.Query(MediaStore.Images.Media.ExternalContentUri, null, MediaStore.Images.Media.InterfaceConsts.Id + " = ? ", new String[] { document_id }, null);
                        cursor.MoveToFirst();
                        contentPath = cursor.GetString(cursor.GetColumnIndex(MediaStore.Images.Media.InterfaceConsts.Data));
                        cursor.Close();
                        return contentPath;
                    }
                    catch (Exception ex)
                    {
                        var msg = ex.Message;
                        return null;
                    }
                    finally
                    {
                        if (cursor != null)
                        {
                            cursor.Close();
                            cursor.Dispose();
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }

        public Android.Net.Uri FixUri(string uriPath)
        {
            //remove /ACTUAL
            if (uriPath.Contains("/ACTUAL"))
            {
                uriPath = uriPath.Substring(0, uriPath.IndexOf("/ACTUAL", StringComparison.Ordinal));
            }

            Java.Util.Regex.Pattern pattern = Java.Util.Regex.Pattern.Compile("(content://media/.*\\d)");

            if (uriPath.Contains("content"))
            {
                Matcher matcher = pattern.Matcher(uriPath);

                if (matcher.Find())
                {
                    matcher.Group(1);
                    var flvse = Android.Net.Uri.Parse(matcher.Group(1));
                    return flvse;
                }
                else
                {
                    throw new ArgumentException("Cannot handle this URI");
                }
            }
            else
            {
                return null;
            }
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            Finish();
        }
    }
}