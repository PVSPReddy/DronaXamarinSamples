using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Calibrations.Views
{
    public partial class ScreenLayoutCalibrations : ContentPage
    {
        public ScreenLayoutCalibrations()
        {
            InitializeComponent();
        }


        void BackButtonTapped(object sender, EventArgs e)
        {
            try
            {
                Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        async void SubmitClicked(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

    }
}
