using System;
using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using PunchCardExample;
using PunchCardExample.Droid;

[assembly: ExportRenderer(typeof(XamCustomRoundCornerLayout), typeof(XamCustomRoundCornerLayoutRender))]
namespace PunchCardExample.Droid
{
    public class XamCustomRoundCornerLayoutRender : VisualElementRenderer<StackLayout>
    {

        private float _cornerRadius;
        private RectF _bounds;
        private Path _path;
        private Xamarin.Forms.Color StartColor { get; set; }
        private Xamarin.Forms.Color EndColor { get; set; }
        private GradientDirection gradientStyle { get; set; }

        public XamCustomRoundCornerLayoutRender(){}

        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
            {
                return;
            }
            try
            {
                var stack = e.NewElement as XamCustomRoundCornerLayout;

                this.StartColor = stack.StartColor;
                this.EndColor = stack.EndColor;
                this.gradientStyle = stack.SetGradientDirection;
                _cornerRadius = (float)stack.cornerRadius;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"ERROR:", ex.Message);
            }
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            if (w != oldw && h != oldh)
            {
                _bounds = new RectF(0, 0, w, h);
            }

            _path = new Path();
            _path.Reset();
            _path.AddRoundRect(_bounds, _cornerRadius, _cornerRadius, Path.Direction.Cw);
            _path.Close();
        }

        protected override void DispatchDraw(Canvas canvas)
        {
            int height = Height;
            int width = Width;

            if (gradientStyle == GradientDirection.Vertical)
            {
                width = 0;
            }
            else
            {
                height = 0;
            }

            /*
            #region for Vertical Gradient  
            var gradient = new Android.Graphics.LinearGradient(0, 0, 0, Height,
            #endregion

            //#region for Horizontal Gradient  
            //var gradient = new Android.Graphics.LinearGradient(0, 0, Width, 0,

            this.StartColor.ToAndroid(),
            this.EndColor.ToAndroid(),
            Android.Graphics.Shader.TileMode.Mirror);
            //#endregion
            */

            var gradient = new Android.Graphics.LinearGradient(0, 0, width, height, this.StartColor.ToAndroid(), this.EndColor.ToAndroid(), Android.Graphics.Shader.TileMode.Mirror);
            var paint = new Android.Graphics.Paint()
            {
                Dither = true,
            };
            paint.SetShader(gradient);
            canvas.Save();
            canvas.ClipPath(_path);
            canvas.DrawPaint(paint);
            //base.Draw(canvas);
            base.DispatchDraw(canvas);
            canvas.Restore();
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

        }
    }
}

