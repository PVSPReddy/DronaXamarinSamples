using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DynamicXAML
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageViewCell : ViewCell
    {
        public HomePageViewCell()
        {
            InitializeComponent();
        }

        void AddMemoryToPerson(object sender, EventArgs args)
        {
            try
            {
                //HomePage.homePage.Navigation.PushModalAsync(new CreateMemory());
                //DisplayAlert("ALert", "HelloWorld", "Ok");
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}