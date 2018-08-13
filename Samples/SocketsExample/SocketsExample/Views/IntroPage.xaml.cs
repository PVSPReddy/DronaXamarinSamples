using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SocketsExample
{
    public partial class IntroPage : ContentPage
    {
        public IntroPage()
        {
            InitializeComponent();

            submitButton.Clicked += SubmitButtonClicked;
        }

        void SubmitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushModalAsync(new SocketPageTestOne());
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }

    }
}
