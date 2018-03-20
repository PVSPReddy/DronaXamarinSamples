using System;
using System.Collections.Generic;
using DynamicXAML.ValueConverters;
using Xamarin.Forms;

namespace DynamicXAML
{
    public partial class HomePageTest : BasePage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //MessagingCenter.Subscribe<GlobalHeader, string>(this, "My Memories", (sender, args) =>//(GlobalHeader arg1, string arg2) =>
            //{
            //    // do something whenever the "HomePage - key" message is sent
            //    try
            //    {
            //        var ParentPage = (MasterDetailPage)this.Parent;
            //        ParentPage.IsPresented = (ParentPage.IsPresented == false) ? true : false;
            //    }
            //    catch (Exception ex)
            //    {
            //        var msg = ex.Message + "\n" + ex.StackTrace;
            //        System.Diagnostics.Debug.WriteLine(msg);
            //    }
            //});
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //MessagingCenter.Unsubscribe<GlobalHeader, string>(this, "My Memories");
        }

        HomePageViewModel bindingModel;

        public HomePageTest()
        {
            bindingModel = new HomePageViewModel(Navigation);
            BindingContext = bindingModel;

            InitializeComponent();

            NaviType = "Master";
            this.NaviImage = TextToUniCodeSymbolCoverter.GetSymbolValue("HamBurger");
            PageTitle = "Memories Hub";
            ShowThirdButton = true;
            //currentView = bodyContent;

            listViewCompanions.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                try
                {
                    var selectedMemory = ((ListView)sender).SelectedItem as MemoriesData;
                    if (selectedMemory == null)
                    {
                        return;
                    }
                    else
                    {
                        //Navigation.PushModalAsync(new MemoryDetailPage(selectedMemory));
                    }
                    ((ListView)sender).SelectedItem = null;
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }

            };
        }

        void OpenNavigationMenu(object sender, EventArgs args)
        {
            try
            {
                var ParentPage = (MasterDetailPage)this.Parent;
                ParentPage.IsPresented = (ParentPage.IsPresented == false) ? true : false;
                //HomePage.homePage.Navigation.PushModalAsync(new CreateMemory());
                //DisplayAlert("ALert", "HelloWorld", "Ok");
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        void AddNewMemory(object sender, EventArgs args)
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

        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();
            //try
            //{
            //    return true;
            //}
            //catch(Exception ex)
            //{
            //    var msg = ex.Message;
            //    return true;
            //}
            return true;
        }
    }
}
