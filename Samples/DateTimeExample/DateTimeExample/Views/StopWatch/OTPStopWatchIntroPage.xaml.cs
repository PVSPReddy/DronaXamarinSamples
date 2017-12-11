using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DateTimeExample
{
    public partial class OTPStopWatchIntroPage : ContentPage
    {
        public OTPStopWatchIntroPage()
        {
            InitializeComponent();
            List<string> pickerStopWatchFormatItems = new List<string>()
            {
                "Milli-Seconds", "Seconds", "Minutes", "Hours", "Days"
            };
            pickerTimeFormat.ItemsSource = pickerStopWatchFormatItems;
            pickerTimeFormat.SelectedIndexChanged += TimeFormatSelected;
        }

        public string timeFormat;
        void TimeFormatSelected(object sender, EventArgs e)
        {
            try
            {
                if (pickerTimeFormat.SelectedIndex == -1)
                {
                    return;
                }
                else
                {
                    timeFormat = pickerTimeFormat.SelectedItem.ToString();
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        void SubmitClicked(object sender, EventArgs e)
        {
            try
            {
                int outValue = 0;
                if (string.IsNullOrEmpty(entryTimeSet.Text))
                {
                    DisplayAlert("Alert", "No of punches Cannot be empty", "Ok");
                }
                else if (!(int.TryParse(entryTimeSet.Text, out outValue)))
                {
                    DisplayAlert("Alert", "Punched required should be of a numeric value", "Ok");
                }
                else
                {
                    int stopWatchTime = (string.IsNullOrEmpty(entryTimeSet.Text)) ? 60 : Convert.ToInt32(entryTimeSet.Text);
                    Navigation.PushModalAsync(new OTPStopWatch(stopWatchTime, timeFormat));
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}
