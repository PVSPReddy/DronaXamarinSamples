using System;
using System.Collections.Generic;
using ARCoreXamSample.DependencyServices;
using Xamarin.Forms;

namespace ARCoreXamSample.Views
{
    public partial class AppStartPage : ContentPage
    {
        public AppStartPage()
        {
            InitializeComponent();

            AccessArKit.Clicked += ARActionButtonCLicked;
        }

        private void ARActionButtonCLicked(object sender, EventArgs e)
        {
            try
            {
                DependencyService.Get<IARKitAccessService>().OpenARKit();
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

        public void GoToARKit()
        {
            try
            {
                DependencyService.Get<IARKitAccessService>().OpenARKit();
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}
