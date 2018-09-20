using System;
using System.Collections.Generic;
using AdMob.DependencyServices;
using Xamarin.Forms;

namespace AdMob.Views
{
    public partial class InterstitialAdPage : ContentPage
    {
        public InterstitialAdPage()
        {
            InitializeComponent();
        }

        private void BackButtonTapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        void InterstitialAdShowClick(object sender, EventArgs e)
        {
            DependencyService.Get<IAdInterstitial>().ShowAd();
        }
    }
}
