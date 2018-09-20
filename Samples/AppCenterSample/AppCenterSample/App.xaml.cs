using System;
using AppCenterSample.Utilities;
using AppCenterSample.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppCenterSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            #region for Microsoft.Appcenter
            //Android : { "secret" : "52100fb2-f588-4a7c-bc11-27596d249a2f" }
            //iOS :  { "secret" : "94bcb716-0c83-4f89-84c2-911e7ac3c90a" }
            AppCenter.Start("ios=94bcb716-0c83-4f89-84c2-911e7ac3c90a;android=52100fb2-f588-4a7c-bc11-27596d249a2f;", typeof(Analytics), typeof(Crashes));
            //AppCenter.Start("ios=3921e3ce-ba7f-4b35-8cb6-f1c6ef3ceba8;android=bcc840ad-a069-461b-bf45-469916be05ac;", typeof(Analytics), typeof(Crashes));
            #endregion
            AppErrorConsole console = new AppErrorConsole();
            OnAppStart();
            // Handle when your app starts
        }

        private void OnAppStart()
        {
            try
            {
                MainPage = new ErrorTestPage();
            }
            catch(Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
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
