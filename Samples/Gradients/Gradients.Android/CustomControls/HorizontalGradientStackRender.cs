using System;
using Android.Content;
using Android.Graphics;
using Gradients.CustomControls;
using Gradients.Droid.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(HorizontalGradientStack), typeof(HorizontalGradientStackRender))]
namespace Gradients.Droid.CustomControls
{
    public class HorizontalGradientStackRender : VisualElementRenderer<StackLayout>
    {
        Context context;
        public HorizontalGradientStackRender(Context _context) : base(_context)
        {
            context = _context;
        }

        private Xamarin.Forms.Color StartColor { get; set; }
        private Xamarin.Forms.Color EndColor { get; set; }
        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }
            try
            {
                var stack = e.NewElement as HorizontalGradientStack;
                this.StartColor = stack.StartColor;
                this.EndColor = stack.EndColor;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"ERROR:", ex.Message);
            }
        }

        protected override void DispatchDraw(Android.Graphics.Canvas canvas)
        {
            int height = 0;
            int width = Width;
            var gradient = new Android.Graphics.LinearGradient(0, 0, width, height, this.StartColor.ToAndroid(), this.EndColor.ToAndroid(), Android.Graphics.Shader.TileMode.Mirror);
            var paint = new Android.Graphics.Paint()
            {
                Dither = true,
            };
            paint.SetShader(gradient);
            canvas.DrawPaint(paint);
            base.DispatchDraw(canvas);
        }
    }
}