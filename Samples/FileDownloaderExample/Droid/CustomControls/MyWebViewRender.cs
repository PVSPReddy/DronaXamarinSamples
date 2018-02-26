using System;
using System.Net;
using FileDownloaderExample;
using FileDownloaderExample.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly : ExportRenderer(typeof(MyWebView), typeof(MyWebViewRender))]
namespace FileDownloaderExample.Droid
{
	public class MyWebViewRender : WebViewRenderer
	{
		public MyWebViewRender()
		{
		}
		protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				var customWebView = Element as MyWebView;
				Control.Settings.AllowUniversalAccessFromFileURLs = true;
				Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}", string.Format("file:///android_asset/Content/{0}", WebUtility.UrlEncode(customWebView.Uri))));
			}
		}

	}
}


