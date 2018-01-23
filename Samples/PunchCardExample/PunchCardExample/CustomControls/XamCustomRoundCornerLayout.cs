using System;

using Xamarin.Forms;

namespace PunchCardExample
{
    public class XamCustomRoundCornerLayout : StackLayout
    {
        public XamCustomRoundCornerLayout(){}

        public Color StartColor { get; set; }
        public Color EndColor { get; set; }
        public double cornerRadius { get; set; }
        public GradientDirection SetGradientDirection { get; set; }
    }

    public enum GradientDirection
    {
        Vertical = 1,
        Horizontal = 2
    }
}

