using System;
using System.IO;
using QuickLook;
using System.Net;
using System.Text;
using FileDownloaderExample;
using FileDownloaderExample.iOS;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

[assembly : Dependency(typeof(IdownloadService))]
namespace FileDownloaderExample.iOS
{
	public class IdownloadService : IDownload
	{
		WebClient webclient;
		string localPath;
		public IdownloadService(){}
		DownloadPage downloadpage { set; get;}
		IEnumerable<FileInfo> files;
		List<FileDataFolder> _fileDataFolder = new List<FileDataFolder>();
		List<FileDataFolder> _fileDataFolder1 = new List<FileDataFolder>();

		public void DownladThis(string fileUrl, string fileName, DownloadPage _downloadpage )
		{
			downloadpage = _downloadpage;
			webclient = new WebClient();
			var url = new Uri(fileUrl);
			webclient.DownloadDataAsync(url);
			webclient.Encoding = Encoding.UTF8;
			webclient.DownloadDataCompleted += (sender, e) =>
			{
				var text = e.Result;
				string documentPath = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
				//string name = fileName;
				//string extens = fileName;
				//string fName = "Download.pdf";
				string folderName = "Sportzb";
				var folderPath = Path.Combine(documentPath, folderName);
				Directory.CreateDirectory(folderPath);
				//string fName = "Download.pdf";
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
					fName = "Download"+((count+1).ToString())+".pdf";
					localPath = Path.Combine(folderPath, fName);
					File.WriteAllBytes(localPath, text);
				}
				else
				{
					localPath = Path.Combine(folderPath, fName);
					File.WriteAllBytes(localPath, text);
				}
				#endregion
				//var text = e.Result;
				//string documentPath = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
				//string library = Path.Combine(documentPath, "..", "Library");
				//var directoryname = Path.Combine(library, "NewLibraryDirectory");
				//Directory.CreateDirectory(directoryname);
				////string name = fileName;
				////string extens = fileName;
				//string fName = fileName;
				////string fName = "Download.pdf";
				//localPath = Path.Combine(directoryname, fName);
				//File.WriteAllText(localPath, text);
				//test(fileName);
				SendDetails(localPath);
				SendTotalFiles(files);
			};
			//while (webclient.IsBusy)
			//{
			//	int i = 0;
			//	i++;
			//}
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



		/*public void SendTotalFiles()
		{
			try
			{
				string documentPath = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
				string folderName = "Sportzb";
				var folderPath = Path.Combine(documentPath, folderName);
				DirectoryInfo di = new DirectoryInfo(folderPath);
				FileInfo fi = new FileInfo(folderPath);
				if (fi.Exists)
				{
					var _files = di.EnumerateFiles("*.pdf");
					foreach (var _value in _files)
					{
						_fileDataFolder.Add(new FileDataFolder { file_Name = _value.Name, path_data = _value.DirectoryName });
					}
					downloadpage.ShowAllFiles(_fileDataFolder);
				}
				else
				{
				}
			}
			catch (Exception ex)
			{
				var msg = ex.Message;
			}
			*/


		void SendDetails(string _localpath)
		{
			downloadpage.DownloadDetails(_localpath);
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




		//public async Task<string> Downlad_This(string fileUrl, string fileName)
		//{
		//	webclient = new WebClient();
		//	var url = new Uri(fileUrl);
		//	webclient.DownloadDataAsync(url);
		//	webclient.Encoding = Encoding.UTF8;

		//	webclient.DownloadDataCompleted += (sender, e) =>
		//	{
		//		var text = e.Result;
		//		string documentPath = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
		//		//string name = fileName;
		//		//string extens = fileName;
		//		string fName = fileName;
		//		//string fName = "Download.pdf";
		//		localPath = Path.Combine(documentPath, fName);
		//		File.WriteAllBytes(localPath, text);


		//		//var text = e.Result;
		//		//string documentPath = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
		//		//string library = Path.Combine(documentPath, "..", "Library");
		//		//var directoryname = Path.Combine(library, "NewLibraryDirectory");
		//		//Directory.CreateDirectory(directoryname);
		//		////string name = fileName;
		//		////string extens = fileName;
		//		//string fName = fileName;
		//		////string fName = "Download.pdf";
		//		//localPath = Path.Combine(directoryname, fName);
		//		//File.WriteAllText(localPath, text);
		//		//test(fileName);
		//	};
		//	return @localPath;
		//}


		//async void test(string fName)
		//{
		//	var data = await OpenFile(fName);
		//}
		//public async Task<String> OpenFile(String filename)
		//{
		//	String documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		//	String fullPath = Path.Combine(documentsPath, filename.Replace(' ', '_'));
		//	return @fullPath;
		//}

		/*public async Task<String> DownladThis(string fileUrl, string fileName)
		{
			webclient = new WebClient();
			var url = new Uri(fileUrl);
			webclient.DownloadDataAsync(url);
			webclient.Encoding = Encoding.UTF8;
			webclient.DownloadDataCompleted += (sender, e) =>
			{
				var text = e.Result;
				string documentPath = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
				//string name = fileName;
				//string extens = fileName;
				string fName = fileName;
				//string fName = "Download.pdf";
				localPath = Path.Combine(documentPath, fileName.Replace(' ', '_'));
				File.WriteAllBytes(localPath, text);


				//var text = e.Result;
				//string documentPath = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
				//string library = Path.Combine(documentPath, "..", "Library");
				//var directoryname = Path.Combine(library, "NewLibraryDirectory");
				//Directory.CreateDirectory(directoryname);
				////string name = fileName;
				////string extens = fileName;
				//string fName = fileName;
				////string fName = "Download.pdf";
				//localPath = Path.Combine(directoryname, fName);
				//File.WriteAllText(localPath, text);
				//test(fileName);
			};
			return @localPath;
		}*/
	}

}

