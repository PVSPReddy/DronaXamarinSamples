using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NetworkSample
{
    public partial class WifiDetailsPage : ContentPage
    {
        public WifiDetailsPage()
        {
            InitializeComponent();
        }

        private async void GetDetailsButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var wifiDetailsResponse = await DependencyService.Get<IGetWifiDetails>().GetAllWifiDetails();
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
    }
}
