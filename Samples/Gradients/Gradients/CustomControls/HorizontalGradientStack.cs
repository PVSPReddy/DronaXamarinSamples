using System;

using Xamarin.Forms;

namespace Gradients.CustomControls
{
    public class HorizontalGradientStack : StackLayout
    {
        public HorizontalGradientStack(){}

        public Color StartColor { get; set; }
        public Color EndColor { get; set; }
    }
}

