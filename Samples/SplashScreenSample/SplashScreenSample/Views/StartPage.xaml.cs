using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SplashScreenSample
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
            btnSubmit.Clicked += (object sender, EventArgs e) => 
            {
                try
                {
                    Navigation.PushModalAsync(new CheckViewOne());
                }
                catch(Exception ex)
                {
                    var msg = ex.Message;
                }
            };
        }
    }
}
