using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DynamicXAML
{
	public partial class TestXAMLOne : BaseXAMLPageOne
	{
		public TestXAMLOne()
		{
			InitializeComponent();
			//titleText = "Hello World Four";
			TitleText = "Hello World Five";
			HeaderColor = Color.Teal;
			NavigationAccess = Navigation;
		}
		void BackClicked(object sender, EventArgs e)
		{
			try
			{
				//DisplayAlert("Alert", "Back-Button Two CLicked", "OK");
				Navigation.PushModalAsync(new TestXAMLTwo());
			}
			catch (Exception ex)
			{
				var msg = ex.Message;
			}
		}

		void SignOutClicked(object sender, EventArgs e)
		{
			try
			{
				DisplayAlert("Alert", "Sign-Out Two CLicked", "OK");
			}
			catch (Exception ex)
			{
				var msg = ex.Message;
			}
		}
	}
}
