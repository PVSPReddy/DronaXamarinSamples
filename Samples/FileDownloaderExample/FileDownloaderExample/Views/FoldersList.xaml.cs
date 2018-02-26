using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FileDownloaderExample
{
	public partial class FoldersList : ContentPage
	{
		ListView foldersList;
		public FoldersList(List<FileDataFolder> folders)
		{
			InitializeComponent();
			foldersList = new ListView()
			{
				//RowHeight = 30,
				ItemsSource = folders,
				ItemTemplate = new DataTemplate(typeof(DisplayFiles)),
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand

			};
			//foldersList.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) =>
			//{
			//	var a = sender as ListView;
			//	var selectitem = (FileDataFolder)a.SelectedItem;
			//	string Display_Data = "Display Data";
			//	string fName = selectitem.file_Name;
			//	string fPath = selectitem.path_data;
			//	string fDocPath = fPath+"/"+fName;
			//	var choice = await DisplayActionSheet("Alert", "Cancel", null, "Send Through Email", Display_Data);
			//	if (choice == Display_Data)
			//	{
			//		//send_File = 
			//		await Navigation.PushAsync(new DisplayDownloaded(fDocPath));
			//	}
			//};

			foldersList.ItemTapped += async (object sender, ItemTappedEventArgs e) =>
			{
				var a = sender as ListView;
				var selectitem = (FileDataFolder)a.SelectedItem;
				string Display_Data = "Display Data";
				string fName = selectitem.file_Name;
				string fPath = selectitem.path_data;
				string fDocPath = fPath + "/" + fName;
				var choice = await DisplayActionSheet("Alert", "Cancel", null, "Send Through Email", Display_Data);
				if (choice == Display_Data)
				{
					//send_File = 
					await Navigation.PushModalAsync(new DisplayDownloaded(fDocPath));
				}
			};
			//ItemsSource = folders;
			Content = foldersList;

		}
	}

	public class DisplayFiles : ViewCell
	{
		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			dynamic c = BindingContext;
			Label _name = new Label()
			{
				TextColor = Color.Green,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
			_name.Text = c.file_Name;
			StackLayout holder = new StackLayout()
			{
				Children = { _name },
				BackgroundColor = Color.Accent,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
			View = holder;
		}
	}
}

