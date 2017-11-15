﻿using Xamarin.Forms;

namespace PunchCardExample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new PunchCardTestPage(20, 0, 5, 30);
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
