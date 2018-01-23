using System;

using CoreAnimation;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using PunchCardExample;
using PunchCardExample.iOS;

[assembly: ExportRenderer(typeof(XamCustomRoundCornerLayout), typeof(XamCustomRoundCornerLayoutRender))]
namespace PunchCardExample.iOS
{
    public class XamCustomRoundCornerLayoutRender : VisualElementRenderer<StackLayout>
    {
        public XamCustomRoundCornerLayoutRender(){}

        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }



        //public override void Draw(CGRect rect)
        //{
        //    base.Draw(rect);
        //}

        public override void Draw(CoreGraphics.CGRect rect)
        {
            base.Draw(rect);

            XamCustomRoundCornerLayout stack = (XamCustomRoundCornerLayout)this.Element;
            CGColor startColor = stack.StartColor.ToCGColor();
            CGColor endColor = stack.EndColor.ToCGColor();
            GradientDirection gradientStyle = stack.SetGradientDirection;

            /*
            #region for Vertical Gradient  

            var gradientLayer = new CAGradientLayer();

            #endregion

            #region for Horizontal Gradient  

            //var gradientLayer = new CAGradientLayer()
            //{
            //  StartPoint = new CGPoint(0, 0.5),
            //  EndPoint = new CGPoint(1, 0.5)
            //};

            #endregion
            */
            var gradientLayer = new CAGradientLayer();
            if (gradientStyle == GradientDirection.Vertical)
            {

            }
            else
            {
                gradientLayer.StartPoint = new CGPoint(0, 0.5);
                gradientLayer.EndPoint = new CGPoint(1, 0.5);
            }
            gradientLayer.Frame = rect;
            gradientLayer.Colors = new CGColor[] { startColor, endColor };
            gradientLayer.ModelLayer.CornerRadius = (float)(stack.cornerRadius);
            NativeView.Layer.InsertSublayer(gradientLayer, 0);
        }
    }
}

