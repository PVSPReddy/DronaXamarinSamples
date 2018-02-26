using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FileDownloaderExample
{
	public partial class DownloadPage : ContentPage
	{
		
		IDownload downloadService;
		IPdfView displayService;
		//Object filesList;
		List<FileDataFolder> filesList;
		string fileUrl, fileName, filePath, filePath1;
		//Task<string> filePath1;
		public DownloadPage()
		{
			displayService = DependencyService.Get<IPdfView>();
			InitializeComponent();
			CustomProperties cp = new CustomProperties();
			var screenHeight = cp.AppScreenHeight;
			var screenWidth = cp.AppScreenWidth;
			downloadService = DependencyService.Get<IDownload>();

			holder.HeightRequest = screenHeight;
			holder.WidthRequest = screenWidth;
			stack.HeightRequest = screenHeight / 2;
			stack.WidthRequest = screenWidth;
			stack.Padding = new Thickness((screenHeight * 5) / 100);
			UrlHolder.WidthRequest = (screenWidth * 2) / 3;
			fileNameHolder.WidthRequest = (screenWidth * 2) / 3;
			Submit.WidthRequest = (screenWidth * 2) / 5;
			Display.WidthRequest = (screenWidth * 2) / 5;
			folderView.WidthRequest = (screenWidth * 2) / 5;
			holder.Spacing = 10;







		}
		void SubmitClicked(object sender, EventArgs e)
		{
			try
			{
				#region Coin the File Data
				fileUrl = UrlHolder.Text ?? "https://developer.xamarin.com/guides/xamarin-forms/controls/layouts/offline.pdf";
				//"https://developer.xamarin.com/guides/xamarin-forms/getting-started/hello-xamarin-forms/quickstart/offline.pdf";
				//"https://developer.xamarin.com/guides/xamarin-forms/controls/layouts/offline.pdf";
				//"https://developer.xamarin.com/guides/xamarin-forms/getting-started/offline.pdf";
				var _fileName = fileNameHolder.Text ?? "Download";
				//var _fileexten = fileUrl.Remove(fileUrl.LastIndexOf('.'));

				#region this is used by linq
				//var _fileexten1 = fileUrl.Split('.').Last();//
				#endregion

				var place = fileUrl.LastIndexOf('.');
				var _fileexten = fileUrl.Substring(place + 1);
				fileName = _fileName + "." + _fileexten;
				//int place = fileUrl.LastIndexOf('.');
				//var file = fileUrl.Remove(place);
				#endregion
				//await DisplayAlert("Alert", "Started Downloading", "Cancel");
				//var filePath2 = downloadService.DownladThis(fileUrl, fileName);
				//var filePath2 = await downloadService.Downlad_This(fileUrl, fileName);
				downloadService.DownladThis(fileUrl, fileName, this);
				//filePath1 = filePath2.ToString();
				/*while (filePath == null && filePath == "")
				{
				}*/
				//await DisplayAlert("Alert", "Your file is Downloaded to " + filePath1, "Ok", "Cancel");

			}
			catch (Exception ex)
			{
				var msg = ex.Message;
			}
		}

		public async void DownloadDetails(string filePath2)
		{
			filePath1 = filePath2.ToString();
			await DisplayAlert("Alert", "Your file is Downloaded to " + filePath1, "Ok", "Cancel");
		}



		async void DisplayClicked(object sender, EventArgs e)
		{
			try
			{
				if (filePath1 != null)
				{
					//Device.OpenUri(new Uri("https://developer.xamarin.com/guides/xamarin-forms/getting-started/offline.pdf"));
					//await Navigation.PushModalAsync(new DisplayDownloaded("https://developer.xamarin.com/guides/xamarin-forms/getting-started/offline.pdf"));
					//displayService.ShowPdf(filePath);
					//var uri = new Uri(String.Format("file://{0}", filePath));
					//Device.OpenUri(uri);
					//Device.OpenUri(new Uri(filePath));
					//Device.OpenUri(new Uri("https://www.google.com"));

					if (Device.OS == TargetPlatform.iOS)
					{
						//await Navigation.PushAsync(new DisplayDownloaded(filePath));
						await Navigation.PushModalAsync(new DisplayDownloaded(filePath1));
					}
					else if (Device.OS == TargetPlatform.Android)
					{
						displayService.ShowPdf(filePath1);
					}
					else if (Device.OS == TargetPlatform.WinPhone)
					{
						displayService.ShowPdf(filePath1);
					}
				}
			}
			catch (Exception ex)
			{
				var msg = ex.Message;
			}
		}

		public void ShowAllFiles(List<FileDataFolder> _files)
		{
			filesList = _files;
			//await Navigation.PushAsync(new FoldersList(filesList));
		}
		 async void FolderViewClicked(object sender, EventArgs e)
		{
			try
			{
				
				await Navigation.PushModalAsync(new FoldersList(filesList));
				//downloadService.RequestFilesData();

			}
			catch (Exception ex)
			{
				var msg = ex.Message;
			}
			//try
			//{
			//	//var folderData =
			//	downloadService.openFolders();
			//	var k = await DisplayAlert("Alert", "View Folder", "Ok", "Cancel");
			//	if (k)
			//	{
			//		//await Navigation.PushAsync(new FoldersList(folderData));
			//	}
			//}
			//catch (Exception ex)
			//{
			//	var msg = ex.Message;
			//}
		}
	}
	public class FileDataFolder
	{
		public string file_Name { set; get;} // { get; set:}
		public string path_data { set; get;}
	}
}
