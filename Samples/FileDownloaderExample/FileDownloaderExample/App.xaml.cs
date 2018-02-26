using Xamarin.Forms;

namespace FileDownloaderExample
{
    public partial class App : Application
    {
        public static double screenWidth, screenHeight;
        public App()
        {
            InitializeComponent();

            MainPage = new DownloadPage();
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
