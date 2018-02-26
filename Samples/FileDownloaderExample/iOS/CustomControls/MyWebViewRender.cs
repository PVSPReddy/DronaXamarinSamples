using System;
using System.IO;
using System.Net;
using FileDownloaderExample;
using FileDownloaderExample.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MyWebView), typeof(MyWebViewRender))]
namespace FileDownloaderExample.iOS
{
	
	public class MyWebViewRender : ViewRenderer<MyWebView, UIWebView>
	{
		public MyWebViewRender()
		{
		}
		protected override void OnElementChanged(ElementChangedEventArgs<MyWebView> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
			{
				SetNativeControl(new UIWebView());
			}
			if (e.OldElement != null)
			{
				// Cleanup
			}
			if (e.NewElement != null)
			{
				var customWebView = Element as MyWebView;
				string fileName = Path.Combine(NSBundle.MainBundle.BundlePath, string.Format("Content/{0}", WebUtility.UrlEncode(customWebView.Uri)));
				Control.LoadRequest(new NSUrlRequest(new NSUrl(fileName, false)));
				Control.ScalesPageToFit = true;
			}
		}
	}
}


