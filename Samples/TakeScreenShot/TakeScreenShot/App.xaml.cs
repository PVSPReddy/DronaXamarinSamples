using System;
using TakeScreenShot.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TakeScreenShot
{
    public partial class App : Application
    {
        public static int screenWidth, screenHeight;
        public App()
        {
            InitializeComponent();

            MainPage = new ScreenShotSource();
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
