using System;
using System.IO;
using System.Net;
using System.Text;
using FileDownloaderExample;
using FileDownloaderExample.Droid;
using Xamarin.Forms;
using System.Threading.Tasks;
//using MonoTouch.UIKit;
//using MonoTouch.QuickLook;
//using MonoTouch.Foundation;
using System.Security.Cryptography;
using System.Collections.Generic;

[assembly: Dependency(typeof(IdownloadService))]
namespace FileDownloaderExample.Droid
{
	public class IdownloadService : IDownload
	{
		WebClient webclient;
		string localPath;
		DownloadPage downloadpage { set; get; }
		IEnumerable<FileInfo> files;
		List<FileDataFolder> _fileDataFolder = new List<FileDataFolder>();
		List<FileDataFolder> _fileDataFolder1 = new List<FileDataFolder>();
		public IdownloadService(){}

		public void DownladThis(string fileUrl, string fileName, DownloadPage downloadpage)
		{
            this.downloadpage = downloadpage;
			webclient = new WebClient();
			var url = new Uri(fileUrl);
			webclient.DownloadDataAsync(url);
			webclient.Encoding = Encoding.UTF8;
			webclient.DownloadDataCompleted += (sender, e) =>
			{
				var text = e.Result;
				string documentPath = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
				//string documentPath = Android.OS.Environment.ExternalStorageDirectory.ToString();
				string folderName = "Sportzb";
				var folderPath = Path.Combine(documentPath, folderName);
				Directory.CreateDirectory(folderPath);
				string fName = "Download.pdf";
				localPath = Path.Combine(folderPath, fName);
				#region extras
				DirectoryInfo di = new DirectoryInfo(folderPath);
				FileInfo fi = new FileInfo(localPath);
				if (fi.Exists)
				{
					int count = 0;
					//fi.Delete();
					files = di.EnumerateFiles("*.pdf");
					foreach (var file in files)
					{
						count++;
						var files1 = file.Name;
					}
					fName = "Download" + ((count + 1).ToString()) + ".pdf";
					localPath = Path.Combine(folderPath, fName);
					File.WriteAllBytes(localPath, text);
				}
				else
				{
					localPath = Path.Combine(folderPath, fName);
					File.WriteAllBytes(localPath, text);
				}
				#endregion
				//localPath = Path.Combine(localPath, fName);
				SendDetails(localPath);
				SendTotalFiles(files);
			};
			//return localPath;
		}

		public void SendTotalFiles(IEnumerable<FileInfo> _files)
		{
			try
			{
				_fileDataFolder.Clear();
				foreach (var _value in _files)
				{
					_fileDataFolder.Add(new FileDataFolder { file_Name = _value.Name, path_data = _value.DirectoryName });
				}
				downloadpage.ShowAllFiles(_fileDataFolder);
			}
			catch (Exception ex)
			{
				var msg = ex.Message;
			}
			//return _fileDataFolder;
		}


		void SendDetails(string _localpath)
		{
            try
            {
                downloadpage.DownloadDetails(_localpath);
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
		}

		public void openFolders()
		{
			//int i = 0;
			//List<string> folders = new List<string>();
			//string documentPath = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
			//var directories = Directory.EnumerateDirectories("./");
			//foreach (var directory in directories)
			//{
			//	folders[i] = directory;
			//	i++;
			//}
			//return folders;
		}


		//public Task<string> Downlad_This(string fileUrl, string fileName)
		//{
		//	webclient = new WebClient();
		//	var url = new Uri(fileUrl);
		//	webclient.DownloadDataAsync(url);
		//	webclient.Encoding = Encoding.UTF8;
		//	webclient.DownloadDataCompleted += (sender, e) =>
		//	{
		//		var text = e.Result;
		//		//string documentPath = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
		//		string docpath = Android.OS.Environment.ExternalStorageDirectory.ToString();
		//		string folderName = "Sportzb";
		//		var folderPath = Path.Combine(docpath, folderName);
		//		Directory.CreateDirectory(folderPath);
		//		string fName = "Download.pdf";
		//		localPath = Path.Combine(folderPath, fName);
		//		//localPath = Path.Combine(localPath, fName);
		//		File.WriteAllBytes(localPath, text);
		//	};
		//	return @localPath;
		//}
	}
}

/*using System;
using System.IO;
using System.Threading.Tasks;
using MonoTouch.UIKit;
using MonoTouch.QuickLook;
using System.Net;
using MonoTouch.Foundation;
using System.Security.Cryptography;
using System.Text;

namespace BigTed.Utils
{

  public class PDFItem : QLPreviewItem
	{
		string title;
		string uri;
		
		public PDFItem(string title, string uri)
		{
			this.title = title;
			this.uri = uri;
		}
		
		public override string ItemTitle {
			get { return title; }
		}
		
		public override NSUrl ItemUrl {
			get { return NSUrl.FromFilename(uri); }
		}
	}
	
	public class PDFPreviewControllerDataSource : QLPreviewControllerDataSource
	{
		string url = "";
		string filename = "";
		
		public PDFPreviewControllerDataSource (string url, string filename)
		{
			this.url = url;
			this.filename = filename;
		}
		
		public override QLPreviewItem GetPreviewItem (QLPreviewController controller, int index)
		{
			return new PDFItem (filename, url);
		}
		
		public override int PreviewItemCount (QLPreviewController controller)
		{
			return 1;
		}
	}


	public class BTPdfUtils
	{
		public static string UrlHash(string source)
		{
			MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
			
			// Convert the input string to a byte array and compute the hash. 
			byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(source));
			
			// Create a new Stringbuilder to collect the bytes 
			// and create a string.
			StringBuilder sBuilder = new StringBuilder();
			
			// Loop through each byte of the hashed data  
			// and format each one as a hexadecimal string. 
			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}
			
			// Return the hexadecimal string. 
			return sBuilder.ToString();

		}

		public static void DownloadAndDisplayPdf (string url, string filename, UINavigationController parentController)
		{
			
			string path = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "attachments");
			if (!Directory.Exists(path)) Directory.CreateDirectory(path);
			string hash = UrlHash (url);
			path = Path.Combine (path, hash);
			if (!Directory.Exists(path)) Directory.CreateDirectory(path);

			string newFilename = Path.Combine (path, filename);

			if (!newFilename.StartsWith (path))
			{
				newFilename = Path.Combine (path, hash + ".pdf");
			}
			
			Task.Factory.StartNew (() => {
				try 
				{
					if (File.Exists (newFilename)) 
					{
						var fi = new FileInfo(newFilename);
						if (fi.Length > 1024 * 2)
						{
							return true;
						}

						File.Delete (newFilename);
					}

					if (Reachability.InternetConnectionStatus() == NetworkStatus.NotReachable) 
					{
					
						return false;
					}
					


					
					var wc = new WebClient();
					
					wc.DownloadFile(new Uri(url), newFilename);
					return true;
				} catch (Exception ex){
					return false;
				}
			}).ContinueWith (tr => {
				
				BTUtils.HideStatus();
				
				if (tr.Result) 
				{
					
					QLPreviewController previewController = new QLPreviewController ();
					previewController.DataSource = new PDFPreviewControllerDataSource (newFilename, filename);
				
					parentController.PushViewController(previewController, true);

					
				} else {
					//show a fail to the user
				}
				
				
				
				
			}, new UIKitScheduler ());
		}
	}
}
*/

