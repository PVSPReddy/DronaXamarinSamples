using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DateTimeExample
{
    public partial class OTPStopWatch : ContentPage
    {
        Stopwatch stopWatch;
        TimeSpan totalTime;
        bool shallRunTimer = true;
        public OTPStopWatch(int time, string format)
        {
            InitializeComponent();
            totalTime = TimeSpan.FromSeconds(time);
            stopWatch = new Stopwatch();
            stopWatch.Start();
            Device.StartTimer(TimeSpan.FromSeconds(1), StopWatchTimerChanged);
        }

        bool StopWatchTimerChanged()
        {
            try
            {
                var timeRemaining = totalTime - stopWatch.Elapsed;
                if (timeRemaining.Seconds >= 0 || timeRemaining.Minutes > 0)
                {
                    string strSecondsTimer = "", strMinutesTimer = "";
                    if (timeRemaining.Seconds < 10)
                    {
                        strSecondsTimer = "0" + timeRemaining.Seconds.ToString();
                    }
                    else
                    {
                        strSecondsTimer = timeRemaining.Seconds.ToString();
                    }
                    if (timeRemaining.Minutes < 10)
                    {
                        strMinutesTimer = "0" + timeRemaining.Minutes.ToString();
                    }
                    else
                    {
                        strMinutesTimer = timeRemaining.Minutes.ToString();
                    }

                    labelStopTimer.Text = strMinutesTimer + ":" + strSecondsTimer;
                }
                else
                {
                    labelStopTimer.Text = "00:00";
                    shallRunTimer = false;
                    stopWatch.Stop();
                    entryOTP.IsEnabled = false;
                    labelReSendOTP.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                shallRunTimer = false;
            }
            return shallRunTimer;
        }

        void ResendClicked(object sender, EventArgs e)
        {
            try
            {
                stopWatch.Reset();
                stopWatch.Start();
                shallRunTimer = true;
                entryOTP.IsEnabled = true;
                labelReSendOTP.IsVisible = false;
                Device.StartTimer(TimeSpan.FromSeconds(1), StopWatchTimerChanged);
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
                if (string.IsNullOrEmpty(entryOTP.Text))
                {

                }
                else if (!(Int32.TryParse(entryOTP.Text, out outValue)))
                {

                }
                else
                {
                    Navigation.PopModalAsync();
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}
