using System;
using Localization.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Localization
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new IntroPage();//XFLoginAnimation();//IntroPage();
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
