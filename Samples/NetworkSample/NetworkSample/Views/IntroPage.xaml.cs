using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NetworkSample
{
    public partial class IntroPage : ContentPage
    {
        public IntroPage()
        {
            InitializeComponent();
        }

        private void NavigationClicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushModalAsync(new WifiDetailsPage());
            }
            catch(Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
    }
}
