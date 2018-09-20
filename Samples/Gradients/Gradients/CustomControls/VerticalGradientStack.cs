using System;

using Xamarin.Forms;

namespace Gradients.CustomControls
{
    public class VerticalGradientStack : StackLayout
    {
        public VerticalGradientStack(){}

        public Color StartColor { get; set; }
        public Color EndColor { get; set; }
    }
}

