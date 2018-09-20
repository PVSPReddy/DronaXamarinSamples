using System;
using CoreAnimation;
using CoreGraphics;
using Gradients.CustomControls;
using Gradients.iOS.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HorizontalGradientStack), typeof(HorizontalGradientStackRender))]
namespace Gradients.iOS.CustomControls
{
    public class HorizontalGradientStackRender : VisualElementRenderer<StackLayout>
    {
        public HorizontalGradientStackRender(){}

        public override void Draw(CoreGraphics.CGRect rect)
        {
            base.Draw(rect);

            HorizontalGradientStack stack = (HorizontalGradientStack)this.Element;
            CGColor startColor = stack.StartColor.ToCGColor();
            CGColor endColor = stack.EndColor.ToCGColor();
            var gradientLayer = new CAGradientLayer();
            gradientLayer.StartPoint = new CGPoint(0, 0.5);
            gradientLayer.EndPoint = new CGPoint(1, 0.5);
            gradientLayer.Frame = rect;
            gradientLayer.Colors = new CGColor[] { startColor, endColor };
            NativeView.Layer.InsertSublayer(gradientLayer, 0);
        }
    }
}

