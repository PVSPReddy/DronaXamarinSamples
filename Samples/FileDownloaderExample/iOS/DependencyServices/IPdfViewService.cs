using System;
using Xamarin.Forms;
using FileDownloaderExample;
using FileDownloaderExample.iOS;
using System.IO;
using QuickLook;
using UIKit;
using Foundation;

[assembly : Dependency(typeof(IPdfViewService))]
namespace FileDownloaderExample.iOS
{
	public class IPdfViewService : IPdfView
	{
		public IPdfViewService() { }



		public void ShowPdf(string filePath)
		{
			//var uri = new Uri(String.Format("file://{0}", filePath));
			//Device.OpenUri(uri);
		}

		public void PrintPdf(string filePath)
		{
			try
			{
				var printInfo = UIPrintInfo.PrintInfo;

				printInfo.Duplex = UIPrintInfoDuplex.LongEdge;

				printInfo.OutputType = UIPrintInfoOutputType.General;

				printInfo.JobName = "Test";

				var printer = UIPrintInteractionController.SharedPrintController;

				printer.PrintInfo = printInfo;

				//printer.PrintingItem = NSData.FromFile(filePath);
				printer.PrintingItem = NSData.FromUrl(NSUrl.FromString(filePath));

				printer.ShowsPageRange = true;

				printer.Present(true, (handler, completed, err) =>
				{
					if (!completed && err != null)
					{
						Console.WriteLine("Printer Error");
					}
				});
			}
			catch (Exception ex)
			{
				var msg = ex.Message;
			}
		}
		
	}
		/*public void ShowPdf(string filePath)
		{
			
			FileInfo fi = new FileInfo(filePath);

			QLPreviewController previewController = new QLPreviewController();
			previewController.DataSource = new PDFPreviewControllerDataSource(fi.FullName, fi.Name);

			UINavigationController controller = FindNavigationController();
			if (controller != null)
				controller.PresentViewController(previewController, true, null);
		}
		private UINavigationController FindNavigationController()
		{
			foreach (var window in UIApplication.SharedApplication.Windows)
			{
				if (window.RootViewController.NavigationController != null)
					return window.RootViewController.NavigationController;
				else
				{
					UINavigationController val = CheckSubs(window.RootViewController.ChildViewControllers);
					if (val != null)
						return val;
				}
			}
						return null;
		}
		private UINavigationController CheckSubs(UIViewController[] controllers)
		{
			foreach (var controller in controllers)
			{
				if (controller.NavigationController != null)
					return controller.NavigationController;
				else
				{
					UINavigationController val = CheckSubs(controller.ChildViewControllers);
					if (val != null)
						return val;
				}
			}
			return null;
		}
	}
	public class PDFItem : QLPreviewItem
	{
		string title;
		string uri;

		public PDFItem(string title, string uri)
		{
			this.title = title;
			this.uri = uri;
		}
		public override string ItemTitle
		{
			get { return title; }
		}
		public override NSUrl ItemUrl
		{
			get { return NSUrl.FromFilename(uri); }
		}
	}
	public class PDFPreviewControllerDataSource : QLPreviewControllerDataSource
	{
		string url = "";
		string filename = "";
		public PDFPreviewControllerDataSource(string url, string filename)
		{
			this.url = url;
			this.filename = filename;
		}
		public override IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index)
		{
			return new PDFItem(filename, url);
		}
		public override nint PreviewItemCount(QLPreviewController controller)
		{
			return 1;
		}
	}*/
	}

/*
public void OpenPDF(string filePath)
{
FileInfo fi = new FileInfo(filePath);

QLPreviewController previewController = new QLPreviewController();
previewController.DataSource = new PDFPreviewControllerDataSource(fi.FullName, fi.Name);

UINavigationController controller = FindNavigationController();
if (controller != null)
controller.PresentViewController(previewController, true, null);
}

private UINavigationController FindNavigationController()
{
foreach (var window in UIApplication.SharedApplication.Windows)
{
if (window.RootViewController.NavigationController != null)
return window.RootViewController.NavigationController;
else
{
UINavigationController val = CheckSubs(window.RootViewController.ChildViewControllers);
if (val != null)
return val;
}
}

return null;
}

private UINavigationController CheckSubs(UIViewController[] controllers)
{
foreach (var controller in controllers)
{
if (controller.NavigationController != null)
return controller.NavigationController;
else
{
UINavigationController val = CheckSubs(controller.ChildViewControllers);
if (val != null)
return val;
}
}
return null;
}

public class PDFItem : QLPreviewItem
{
	string title;
	string uri;

	public PDFItem(string title, string uri)
	{
		this.title = title;
		this.uri = uri;
	}

	public override string ItemTitle
	{
		get { return title; }
	}

	public override NSUrl ItemUrl
	{
		get { return NSUrl.FromFilename(uri); }
	}
}

public class PDFPreviewControllerDataSource : QLPreviewControllerDataSource
{
	string url = "";
	string filename = "";

	public PDFPreviewControllerDataSource(string url, string filename)
	{
		this.url = url;
		this.filename = filename;
	}

	public override QLPreviewItem GetPreviewItem(QLPreviewController controller, int index)
	{
		return new PDFItem(filename, url);
	}

	public override int PreviewItemCount(QLPreviewController controller)
	{
		return 1;
	}
}
*/