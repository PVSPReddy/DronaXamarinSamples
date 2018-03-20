using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DynamicXAML
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void MoveNextClicked(object sender, EventArgs e)
        {
            try
            {
                //Navigation.PushModalAsync(new HomePage());
                //Navigation.PushModalAsync(new HomePageTest());
                Navigation.PushModalAsync(new HomePageTestTwo());
                //Navigation.PushModalAsync(new TestXAMLOne());
            }
            catch(Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
    }
}
