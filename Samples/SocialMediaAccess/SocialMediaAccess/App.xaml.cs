using System;
using SocialMediaAccess.Views.SampleOne;
using Xamarin.Forms;

namespace SocialMediaAccess
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            try
            {
                MainPage = new IntroPage();
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
