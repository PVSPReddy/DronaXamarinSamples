using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SocketsExample
{
    public partial class SocketPageTestOne : ContentPage
    {
        public SocketPageTestOne()
        {
            InitializeComponent();
            sendButton.Clicked += SendButtonClicked;
            socketStartButton.Clicked += SocketStartButtonClicked;
            socketStopButton.Clicked += SocketStopButtonClicked;
        }

        void SocketStartButtonClicked(object sender, EventArgs e)
        {
            try
            {
                DependencyService.Get<ISocketTestOne>().InitalizeSocket();
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
            }
        }

        void SocketStopButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(entryText.Text))
                {
                    DependencyService.Get<ISocketTestOne>().StopSocket();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
            }
        }

        void SendButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(entryText.Text))
                {
                    DisplayAlert("Alert", "Text cannot be empty", "ok");
                }
                else
                {
                    DependencyService.Get<ISocketTestOne>().SendMessage(entryText.Text);
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
            }
        }
    }
}
