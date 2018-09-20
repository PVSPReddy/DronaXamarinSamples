using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PageAbsoluteDimensions.Views
{
    public partial class TestPage : ContentPage
    {
        public TestPage()
        {
            InitializeComponent();
            #region for dimension Adjustments
            submitButton.HeightRequest = (App.screenHeight * 20) / 100;
            submitButton.WidthRequest = (App.screenWidth * 70) / 100;
            #endregion
        }

        private async void GetFullDimensions(object sender, EventArgs e)
        {
            await DisplayAlert("Page Dimensions Are", "Height = " + (App.screenHeight.ToString()) + ". \n Width = " + (App.screenWidth) + ". ", "Ok");
        }
    }
}
