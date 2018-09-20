using System;
using System.Threading.Tasks;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace AppCenterSample.Utilities
{
    public class AppErrorConsole
    {
        public static AppErrorConsole appErrorConsole;
        public AppErrorConsole()
        {
            appErrorConsole = this;
        }

        public async Task<bool> SendEventOccured(string message)
        {
            try
            {
                Analytics.TrackEvent(message);
            }
            catch (Exception ex)
            {
                var msg = "Error is in report Page : \n " + ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            return true;
        }

        public async Task<bool> SendErrorReport(Exception exception)
        {
            try
            {
                Analytics.TrackEvent(exception.Message);
                //Crashes.TrackError(exception, null);
            }
            catch (Exception ex)
            {
                var msg = "Error is in report Page : \n " + ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            return true;
        }

        public async Task<bool> SendErrorReport(Exception exception, string pageName)
        {
            try
            {
                //Analytics.TrackEvent(exception.Message);
                var msg = pageName + "\n" + "Error is in report Page : \n " + exception.Message + "\n" + exception.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
                //Crashes.TrackError(exception, null);

            }
            catch (Exception ex)
            {
                var msg = "Error is in report Page : \n " + ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            return true;
        }
    }
}

