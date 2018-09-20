using System;
using System.Collections.Generic;
using AppCenterSample.Utilities;
using Xamarin.Forms;

namespace AppCenterSample.Views
{
    public partial class ErrorTestPage : ContentPage
    {
        public ErrorTestPage()
        {
            InitializeComponent();

            errorButton.Clicked += (object sender, EventArgs e) =>
            {
                try
                {
                    //throw new Exception("This is a test exception message");

                    int zero = 0;
                    int result = 100 / zero;
                }
                catch (Exception ex)
                {
                    try
                    {
                        AppErrorConsole.appErrorConsole.SendErrorReport(ex);
                    }
                    catch (Exception exs)
                    {
                        var msg = ex.Message + "\n" + ex.StackTrace;
                        System.Diagnostics.Debug.WriteLine(msg);
                    }
                }
            };

            crashButton.Clicked += async (object sender, EventArgs e) =>
            {
                var shallCrash = await DisplayAlert("Alert", "Let app to crash for testing", "Ok", "Cancel");
                if(shallCrash)
                {
                    int zero = 0;
                    int result = 100 / zero;
                }
                else
                {
                    
                }
            };
        }
    }
}
