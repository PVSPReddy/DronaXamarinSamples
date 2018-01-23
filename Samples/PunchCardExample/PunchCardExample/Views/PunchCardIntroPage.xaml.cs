using System;
using System.Collections.Generic;
using PunchCardExample.Views;
using Xamarin.Forms;

namespace PunchCardExample
{
    public partial class PunchCardIntroPage : ContentPage
    {
        public PunchCardIntroPage()
        {
            InitializeComponent();
        }

        void SubmitClicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushModalAsync(new PunchCardTest());

                /*
                int outValue = 0;
                if (string.IsNullOrEmpty(entryPunchesRequired.Text))
                {
                    DisplayAlert("Alert", "No of punches Cannot be empty", "Ok");
                }
                else if (!(int.TryParse(entryPunchesRequired.Text, out outValue)))
                {
                    DisplayAlert("Alert", "Punched required should be of a numeric value", "Ok");
                }
                else if ((!string.IsNullOrEmpty(entryPunchesRequired.Text)) && (!int.TryParse(entryPunchesRequired.Text, out outValue)))
                {
                    DisplayAlert("Alert", "Punched Punches should be of a numeric value", "Ok");
                }
                else if ((!string.IsNullOrEmpty(entryPunchesColumns.Text)) && (!int.TryParse(entryPunchesColumns.Text, out outValue)))
                {
                    DisplayAlert("Alert", "Punch Columns should be of a numeric value", "Ok");
                }
                else if ((!string.IsNullOrEmpty(entryPunchHeight.Text)) && (!int.TryParse(entryPunchHeight.Text, out outValue)))
                {
                    DisplayAlert("Alert", "Punch Height should be of a numeric value", "Ok");
                }
                else
                {
                    int punchesRequired = (string.IsNullOrEmpty(entryPunchesRequired.Text)) ? 25 : Convert.ToInt32(entryPunchesRequired.Text);
                    int punchesFilled = (string.IsNullOrEmpty(entryPunchesPunched.Text)) ? 0 : Convert.ToInt32(entryPunchesPunched.Text);
                    int punchesColumnLimitTo = (string.IsNullOrEmpty(entryPunchesColumns.Text)) ? 5 : Convert.ToInt32(entryPunchesColumns.Text);
                    int puncheHeight = (string.IsNullOrEmpty(entryPunchHeight.Text)) ? 30 : Convert.ToInt32(entryPunchHeight.Text);
                    Navigation.PushModalAsync(new PunchCardTestPage(punchesRequired, punchesFilled, punchesColumnLimitTo, puncheHeight));
                }
                */
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}
