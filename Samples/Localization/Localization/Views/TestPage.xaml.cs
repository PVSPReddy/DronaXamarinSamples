using System;
using System.Collections.Generic;
using UsingResxLocalization.Resx;
using Xamarin.Forms;

namespace Localization.Views
{
    public partial class TestPage : ContentPage
    {
        public TestPage()
        {
            InitializeComponent();
            submitButton.Text = AppResources.NotesLabel;
        }

        private void BackButtonClicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PopModalAsync();
            }
            catch(Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
    }
}
