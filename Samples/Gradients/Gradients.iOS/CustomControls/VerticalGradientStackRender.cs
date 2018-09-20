using System;
using CoreAnimation;
using CoreGraphics;
using Gradients.CustomControls;
using Gradients.iOS.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly : ExportRenderer(typeof(VerticalGradientStack), typeof(VerticalGradientStackRender))]
namespace Gradients.iOS.CustomControls
{
    public class VerticalGradientStackRender : VisualElementRenderer<StackLayout>
    {
        public VerticalGradientStackRender(){}

        public override void Draw(CoreGraphics.CGRect rect)
        {
            base.Draw(rect);

            VerticalGradientStack stack = (VerticalGradientStack)this.Element;
            CGColor startColor = stack.StartColor.ToCGColor();
            CGColor endColor = stack.EndColor.ToCGColor();
            var gradientLayer = new CAGradientLayer();
            gradientLayer.Frame = rect;
            gradientLayer.Colors = new CGColor[] { startColor, endColor };
            NativeView.Layer.InsertSublayer(gradientLayer, 0);
        }
    }
}

