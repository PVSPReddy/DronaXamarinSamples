using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace AdMob.Views
{
    public partial class IntroPage : ContentPage
    {
        public IntroPage()
        {
            InitializeComponent();
        }

        void BannerAdShowClick(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new BannerAdPage());
        }

        void InterstitialAdShowClick(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new InterstitialAdPage());
        }
    }
}
