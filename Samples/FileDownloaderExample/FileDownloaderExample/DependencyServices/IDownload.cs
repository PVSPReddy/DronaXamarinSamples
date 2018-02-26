using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileDownloaderExample
{
	public interface IDownload
	{
		void DownladThis(String fileUrl, string fileName, DownloadPage downloadpage);

		//List<string> openFolders();
		void openFolders();
		//Task<string> Downlad_This(String fileUrl, string fileName);

		//void SendTotalFiles();

		//List<FileDataFolder> RequestFilesData();
	}
}

