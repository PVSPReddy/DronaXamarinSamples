using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DynamicXAML
{
    public partial class HomePageTestTwo : ContentPage
    {
        //public static HomePage homePage;
        //public HomePage
        HomePageViewModel homePageViewModel;

        public string NaviImage { get; set; }
        public string PageTitle { get; set; }
        public string NaviType { get; set; }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<GlobalHeaderOne, string>(this, "Memories Hub", (sender, args) =>//(GlobalHeader arg1, string arg2) =>
            {
                // do something whenever the "HomePage - key" message is sent
                try
                {
                    var ParentPage = (MasterDetailPage)this.Parent;
                    ParentPage.IsPresented = (ParentPage.IsPresented == false) ? true : false;
                }
                catch (Exception ex)
                {
                    var msg = ex.Message + "\n" + ex.StackTrace;
                    System.Diagnostics.Debug.WriteLine(msg);
                }
            });
            MessagingCenter.Subscribe<GlobalHeaderOne>(this, "Add", (sender) =>//(GlobalHeader arg1, string arg2) =>
            {
                // do something whenever the "HomePage - key" message is sent
                try
                {
                    homePageViewModel.AddMemoryClick.Execute(null);
                }
                catch (Exception ex)
                {
                    var msg = ex.Message + "\n" + ex.StackTrace;
                    System.Diagnostics.Debug.WriteLine(msg);
                }
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<GlobalHeaderOne, string>(this, "My Memories");
            MessagingCenter.Unsubscribe<GlobalHeaderOne>(this, "Add");
        }

        public HomePageTestTwo()
        {
            homePageViewModel = new HomePageViewModel(Navigation);
            NaviType = "Master";
            PageTitle = "Memories Hub";
            NaviImage = ValueConverters.TextToUniCodeSymbolCoverter.GetSymbolValue("HamBurger");
            BindingContext = homePageViewModel;

            InitializeComponent();
            //NaviType = "Master";
            //PageTitle = "Memories Hub";
            //NaviImage = ValueConverters.TextToUniCodeSymbolCoverter.GetSymbolValue("HamBurger");


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

        //void OpenNavigationMenu(object sender, EventArgs args)
        //{
        //    try
        //    {
        //        var ParentPage = (MasterDetailPage)this.Parent;
        //        ParentPage.IsPresented = (ParentPage.IsPresented == false) ? true : false;
        //        //HomePage.homePage.Navigation.PushModalAsync(new CreateMemory());
        //        //DisplayAlert("ALert", "HelloWorld", "Ok");
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message;
        //    }
        //}

        //void AddNewMemory(object sender, EventArgs args)
        //{
        //    try
        //    {
        //        //HomePage.homePage.Navigation.PushModalAsync(new CreateMemory());
        //        //DisplayAlert("ALert", "HelloWorld", "Ok");
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message;
        //    }
        //}

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
