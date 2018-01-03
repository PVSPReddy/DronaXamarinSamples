using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DynamicXAML
{
	public partial class TestXAMLTwo : BaseXAMLPageOne
	{
		public TestXAMLTwo()
		{
			InitializeComponent();
			TitleText = "Hello World Six";
			HeaderColor = Color.Green;
			NavigationAccess = Navigation;
		}

		void BackClicked(object sender, EventArgs e)
		{
			try
			{
				//DisplayAlert("Alert", "Back-Button Three CLicked", "OK");
				Navigation.PopModalAsync();
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
				DisplayAlert("Alert", "Sign-Out Four CLicked", "OK");
			}
			catch (Exception ex)
			{
				var msg = ex.Message;
			}
		}
	}
}
