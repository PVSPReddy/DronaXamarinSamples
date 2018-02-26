using System;
using Xamarin.Forms;
using FileDownloaderExample;
using FileDownloaderExample.Droid;
using Java.IO;
using Android.Content;
using Android.Net;
//using Android.App;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Android.OS;
//using Android.Provider;
//using Android.Graphics;


[assembly: Dependency(typeof(IPdfViewService))]
namespace FileDownloaderExample.Droid
{
	public class IPdfViewService : IPdfView
	{
		public IPdfViewService(){}

		public void ShowPdf(string filePath)
		{
			//var fileLocation = "/sdcard/Template.pdf";
			var fileLocation = filePath;
			var file = new File(fileLocation);

			if (!file.Exists())
				return;

			var intent = DisplayPdf(file);
			Forms.Context.StartActivity(intent);
		}

		public Intent DisplayPdf(File file)
		{
			var intent = new Intent(Intent.ActionView);
			var filepath = Android.Net.Uri.FromFile(file);
			intent.SetDataAndType(filepath, "application/pdf");
			intent.SetFlags(ActivityFlags.ClearTop);
			return intent;
		}

		public void PrintPdf(string filePath)
		{
			throw new NotImplementedException();
		}
	}
}

