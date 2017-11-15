using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PunchCardExample
{
    public partial class PunchCardTestPage : ContentPage
    {
        public static PunchCardTestPage punchCardTestPage;
        public PunchCardTestPage(int punchedRequired, int punches_Filled, int punch_Columns, int punch_Height)
        {
            InitializeComponent();
            punchCardTestPage = this;
            int punchHeight = (punch_Height == 0) ? 20 : punch_Height;
            int punchColumns = (punch_Columns == 0) ? 8 : punch_Columns;
            //AssignPunches(punchedRequired, punches_Filled, punchHeight, punchColumns);
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

        public void FillLabelData(string fillableData)
        {
            try
            {
                lblObtainedData.Text = fillableData;
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
                int outValue = 0;
                if (string.IsNullOrEmpty(entryPunchesRequired.Text))
                {
                    await DisplayAlert("Alert", "No of punches Cannot be empty", "Ok");
                }
                else if (!(int.TryParse(entryPunchesRequired.Text, out outValue)))
                {
                    await DisplayAlert("Alert", "Punched required should be of a numeric value", "Ok");
                }
                else if ((!string.IsNullOrEmpty(entryPunchesRequired.Text)) && (!int.TryParse(entryPunchesRequired.Text, out outValue)))
                {
                    await DisplayAlert("Alert", "Punched Punches should be of a numeric value", "Ok");
                }
                else if ((!string.IsNullOrEmpty(entryPunchesColumns.Text)) && (!int.TryParse(entryPunchesColumns.Text, out outValue)))
                {
                    await DisplayAlert("Alert", "Punch Columns should be of a numeric value", "Ok");
                }
                else if ((!string.IsNullOrEmpty(entryPunchHeight.Text)) && (!int.TryParse(entryPunchHeight.Text, out outValue)))
                {
                    await DisplayAlert("Alert", "Punch Height should be of a numeric value", "Ok");
                }
                else
                {
                    int punchesRequired = (string.IsNullOrEmpty(entryPunchesRequired.Text)) ? 25 : Convert.ToInt32(entryPunchesRequired.Text);
                    int punchesFilled = (string.IsNullOrEmpty(entryPunchesPunched.Text)) ? 0 : Convert.ToInt32(entryPunchesPunched.Text);
                    int punchesColumnLimitTo = (string.IsNullOrEmpty(entryPunchesColumns.Text)) ? 5 : Convert.ToInt32(entryPunchesColumns.Text);
                    int puncheHeight = (string.IsNullOrEmpty(entryPunchHeight.Text)) ? 30 : Convert.ToInt32(entryPunchHeight.Text);

                    if (switchStartLoop.IsToggled == true)
                    {
                        for (int i = 1; i <= punchesRequired; i++)
                        {
                            await AssignPunches(i, punchesFilled, punchesColumnLimitTo, puncheHeight);
                        }
                    }
                    else
                    {
                        await AssignPunches(punchesRequired, punchesFilled, punchesColumnLimitTo, puncheHeight);
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        async Task<bool> AssignPunches(int punchedRequired, int punches_Filled, int punch_Columns, int punch_Height)
        {
            try
            {
                punchCardTestPage = this;
                int punchHeight = (punch_Height == 0) ? 20 : punch_Height;
                int punchColumns = (punch_Columns == 0) ? 8 : punch_Columns;
                //contentPunchHolder.Content = new PunchCardTwo(punchedRequired, punches_Filled, punchHeight, punchColumns);
                contentPunchHolder.Content = new PunchCardThree(punchedRequired, punches_Filled, punchHeight, punchColumns);
                await Task.Delay(1000);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                //await DisplayAlert("Alert", msg, "Ok");
            }
            return true;
        }
    }
}
