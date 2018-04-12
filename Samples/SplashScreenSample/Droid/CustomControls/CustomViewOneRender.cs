using System;
using SplashScreenSample;
using SplashScreenSample.Droid;
using Xamarin.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AViews = Android.Views;
using AControls = Android.Widget;
using AGraphics = Android.Graphics;
using Android.Widget;
using Android.Views;

[assembly : ExportRenderer(typeof(CustomViewOne), typeof(CustomViewOneRender))]
namespace SplashScreenSample.Droid
{
    public class CustomViewOneRender : ViewRenderer<CustomViewOne, AViews.View>
    {
        public CustomViewOneRender(){}
        CustomViewOne llayout;
        AViews.View nativeView;

        protected override void OnElementChanged(ElementChangedEventArgs<CustomViewOne> e)
        {
            base.OnElementChanged(e);
            //llayout = new LinearLayout(Context);

            if (Control == null)
            {
                // Instantiate the native control and assign it to the Control property with
                // the SetNativeControl method
                try
                {
                    var inflater = LayoutInflater.From(Forms.Context);
                    nativeView = inflater.Inflate(Resource.Layout.SplashLayout, null);
                    //if (Element.Orientation == Orientation.Horizontal)
                    //{
                    //nativeView = inflater.Inflate(Resource.Layout.viewpager, null);
                    //}
                    //else
                    //{
                    //nativeView = inflater.Inflate(Resource.Layout.vertical_viewpager, null);
                    //}

                    SetNativeControl(nativeView);
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            }


            if (e.OldElement != null)
            {
                
            }


            if (e.NewElement != null)
            {
                try
                {
                    
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            }

        }
    }
}

