using System;
using System.IO;
using FileDownloaderExample;
using FileDownloaderExample.iOS;
using Foundation;
using QuickLook;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PdfViewer), typeof(DocumentViewRenderer))]
namespace FileDownloaderExample.iOS
{
	public class DocumentViewRenderer : ViewRenderer<PdfViewer, UIView>
	{
		/*private QLPreviewController controller;

		protected override void OnElementChanged(ElementChangedEventArgs<PdfViewer> e)
		{
			base.OnElementChanged(e);

			this.controller = new QLPreviewController();
			this.controller.DataSource = new DocumentQLPreviewControllerDataSource(e.NewElement.FilePath);

			SetNativeControl(this.controller.View);
		}
	}
	class DocumentQLPreviewControllerDataSource : QLPreviewControllerDataSource
	{
		private string fileName;
		public DocumentQLPreviewControllerDataSource(string fileName)
		{
			this.fileName = fileName;
		}

		public override nint PreviewItemCount(QLPreviewController controller)
		{
			return 1;
		}

		public override IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index)
		{
			var documents = NSBundle.MainBundle.BundlePath;
			var library = Path.Combine(documents, this.fileName);
			NSUrl url = NSUrl.FromFilename(library);

			return new QlItem(string.Empty, url);
		}

		private class QlItem : QLPreviewItem
		{
			public QlItem(string title, NSUrl uri)
			{
				this.ItemTitle = title;
				this.ItemUrl = uri;
			}

			public override string ItemTitle{ get; private set; }
			public override NSUrl ItemUrl { get; private set; }

		}*/
	}
}
