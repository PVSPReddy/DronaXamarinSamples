using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FileDownloaderExample
{
	public partial class DisplayDownloaded : ContentPage
	{
		//Button back, Download, Print ;
		StackLayout stackL, holder1, holding ;
		string fullPath;
		public DisplayDownloaded(string viewData)
		{
			CustomProperties cp = new CustomProperties();
			var screenHeight = cp.AppScreenHeight;
			var screenWidth = cp.AppScreenWidth;
			WebView browser;


			InitializeComponent();
			var uri = new Uri(String.Format("file://{0}", viewData));
			fullPath = uri.ToString();
			browser = new WebView();
			//browser = new MyWebView();
			browser.Source = fullPath;
			browser.HeightRequest = ((screenHeight * 9) / 10) - ((screenHeight * 1) / 500);
			browser.WidthRequest = ((screenWidth * 9) / 10) - ((screenHeight * 1) / 500);
			holder1 = new StackLayout()
			{
				HeightRequest = (screenHeight * 9) / 10,
				WidthRequest = (screenWidth * 9) / 10,
				BackgroundColor = Color.Green,
				Padding = 30,
				Children = { browser },
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill
			};
			/*Download = new Button()
			{
				Text = "Download",
				TextColor = Color.Green,
				BackgroundColor = Color.Gray,
				WidthRequest = (screenWidth * 2) / 5,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};
			Print = new Button()
			{
				Text = "Print",
				TextColor = Color.Green,
				BackgroundColor = Color.Gray,
				WidthRequest = (screenWidth * 2) / 5,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};*/


			#region to attach multiple files
			//if (files != null && files.Count > 0)
			//{
			//	foreach (var file in files)
			//		mail.Attachments.Add(new Attachment(file));
			//}

			//To select a picture I call this method:
			//private async Task SelectPicture()
			//{
			//	try
			//	{
			//		var mediaFile = await this._mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions
			//		{
			//			DefaultCamera = CameraDevice.Front,
			//			MaxPixelDimension = 400
			//		});
			//		return mediaFile.Path;

			//	}
			//	catch (System.Exception ex)
			//	{
			//		return string.Empty;
			//	}
			//}
			#endregion

			Image Download = new Image()
			{
				Source = "Download.png",
				//HeightRequest = 
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.EndAndExpand
			};
			TapGestureRecognizer download_Data = new TapGestureRecognizer();
			download_Data.NumberOfTapsRequired = 1;
			download_Data.Tapped += (object sender, EventArgs e) =>
			{
				//DependencyService.Get<IEmail>().Sendmail(new[] { "foo@example.com" }, "Test", "Hello, World");
				DependencyService.Get<IEmail>().ShareFile(fullPath, "mimeType");
			};
			Download.GestureRecognizers.Add(download_Data);

			Image Print = new Image()
			{
				Source = "Print.png",
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.EndAndExpand
			};

			TapGestureRecognizer print_Data = new TapGestureRecognizer();
			print_Data.NumberOfTapsRequired = 1;
			print_Data.Tapped += (object sender, EventArgs e) =>
			{
				DependencyService.Get<IPdfView>().PrintPdf(fullPath);
			};
			Print.GestureRecognizers.Add(print_Data);
			stackL = new StackLayout()
			{
				BackgroundColor = Color.Blue,
				//HeightRequest = (screenHeight * 1) / 10,
				//WidthRequest = screenWidth - 30,
				Orientation = StackOrientation.Horizontal,
				Children = { Download, Print },
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.EndAndExpand
			};

			holding = new StackLayout()
			{
				HeightRequest = screenHeight,
				WidthRequest = screenWidth,
				Children = { stackL ,holder1 },
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};


			Content = holding;

		}

		public DisplayDownloaded(Task<string> viewData)
		{
			CustomProperties cp = new CustomProperties();
			var screenHeight = cp.AppScreenHeight;
			var screenWidth = cp.AppScreenWidth;
			WebView browser;
			InitializeComponent();
			var uri = new Uri(String.Format("file://{0}", viewData));
			var fullPath = uri.ToString();
			browser = new WebView();
			browser.Source = fullPath;
		}
	}
}

