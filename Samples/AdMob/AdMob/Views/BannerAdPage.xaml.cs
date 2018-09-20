using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace AdMob.Views
{
    public partial class BannerAdPage : ContentPage
    {
        public BannerAdPage()
        {
            InitializeComponent();
        }

        private void BackButtonTapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
