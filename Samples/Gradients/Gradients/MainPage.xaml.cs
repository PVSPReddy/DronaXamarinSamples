using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gradients.Views;
using Xamarin.Forms;

namespace Gradients
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void SubmitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushModalAsync(new GradientTestPage());
            }
            catch(Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
            }
        }

    }
}
