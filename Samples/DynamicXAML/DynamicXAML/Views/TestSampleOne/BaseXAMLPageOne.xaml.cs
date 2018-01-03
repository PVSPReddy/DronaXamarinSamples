using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace DynamicXAML
{
	public partial class BaseXAMLPageOne : ContentPage, INotifyPropertyChanged
	{
		public string titleText;
		public string TitleText
		{
			get
			{
				return titleText;
			}
			set
			{
				if (value != titleText)
				{
					titleText = value;
					PropertyChanged(this, new PropertyChangedEventArgs("TitleText"));
				}
			}
		}
		public Color headerColor;
		public Color HeaderColor
		{
			get
			{
				return headerColor;
			}
			set
			{
				if (value != headerColor)
				{
					headerColor = value;
					PropertyChanged(this, new PropertyChangedEventArgs("HeaderColor"));
				}
			}
		}
		public INavigation navigationAccess;
		public INavigation NavigationAccess
		{
			get
			{
				return navigationAccess;
			}
			set
			{
				if (value != navigationAccess)
				{
					navigationAccess = value;
					PropertyChanged(this, new PropertyChangedEventArgs("NavigationAccess"));
				}
			}
		}
		public event PropertyChangedEventHandler PropertyChanged = delegate { };
		public BaseXAMLPageOne()
		{
			//Navigation
			//titleText = "Hello World One";
			InitializeComponent();
			//titleText = "Hello World Two";
			BindingContext = this;
			//titleText = "Hello World Three";
		}

		//void BackClicked(object sender, EventArgs e)
		//{
		//	try
		//	{
		//		DisplayAlert("Alert", "Back-Button One CLicked", "OK");
		//	}
		//	catch (Exception ex)
		//	{
		//		var msg = ex.Message;
		//	}
		//}

		//void SignOutClicked(object sender, EventArgs e)
		//{
		//	try
		//	{
		//		DisplayAlert("Alert", "Sign-Out One CLicked", "OK"); 
		//	}
		//	catch (Exception ex)
		//	{
		//		var msg = ex.Message;
		//	}
		//}
	}
}
