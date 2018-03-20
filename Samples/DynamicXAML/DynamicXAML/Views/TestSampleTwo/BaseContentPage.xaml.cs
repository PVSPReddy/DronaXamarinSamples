using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace DynamicXAML
{
    public partial class BasePage : ContentPage, INotifyPropertyChanged
    {

        public string naviType = "";
        public string NaviType
        {
            get
            {
                return naviType;
            }
            set
            {
                if (naviType != value)
                {
                    naviType = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("NaviType"));
                }

            }
        }

        public string pageTitle = "";
        public string PageTitle
        {
            get
            {
                return pageTitle;
            }
            set
            {
                if (pageTitle != value)
                {
                    pageTitle = value;
                    PageTitleValue.Text = pageTitle;
                    PropertyChanged(this, new PropertyChangedEventArgs("PageTitle"));
                }

            }
        }

        public string naviImage = "";
        public string NaviImage
        {
            get
            {
                return naviImage;
            }
            set
            {
                if (naviImage != value)
                {
                    naviImage = value;
                    NaviImageValue.Text = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("NaviImage"));
                }

            }
        }

        //public ContentView currentView = new ContentView();
        //public ContentView CurrentView
        //{
        //    get
        //    {
        //        return currentView;
        //    }
        //    set
        //    {
        //        if (currentView != value)
        //        {
        //            currentView = value;
        //            PropertyChanged(this, new PropertyChangedEventArgs("CurrentView"));
        //        }

        //    }
        //}


        public StackLayout currentView = new StackLayout();
        public StackLayout CurrentView
        {
            get
            {
                return currentView;
            }
            set
            {
                if (currentView != value)
                {
                    currentView = value;
                    stackDynamicBody.Content = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrentView"));
                }

            }
        }

        public bool ShowThirdButton { get; set; }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public BasePage()
        {
            BindingContext = this;
            InitializeComponent();

        }

        void OpenNavigationMenu(object sender, EventArgs args)
        {
            try
            {
                /*
                //var objectObtained = (ContentPage)PageDetails;
                //var d = PageDetails.Parent;
                if (NaviType == "Master")
                {
                    var objectObtained = (MasterDetailPage)((((this.Parent).Parent).Parent).Parent);
                    objectObtained.IsPresented = (objectObtained.IsPresented == false) ? true : false;
                    //var objectObtained = (MasterDetailPage)PageNavigation;
                    //objectObtained.IsPresented = (objectObtained.IsPresented == false) ? true : false;
                    //var ParentPage = (MasterDetailPage)this.Parent;
                    //ParentPage.IsPresented = (ParentPage.IsPresented == false) ? true : false;
                }
                else
                {
                    var objectObtained = (ContentPage)(((this.Parent).Parent).Parent);
                    objectObtained.Navigation.PopModalAsync();


                    //Type myType = PageNavigation.GetType();
                    //IList<System.Reflection.PropertyInfo> props = new List<System.Reflection.PropertyInfo>(myType.GetProperties());

                    //foreach (System.Reflection.PropertyInfo prop in props)
                    //{
                    //    object propValue = prop.GetValue(PageNavigation, null);
                    //    //propValue.Navigation.PopModalAsync();
                    //    // Do something with propValue
                    //}
                    //var objectObtained = ((BindableObject)PageNavigation).Navigation; //(INavigation)PageNavigation;
                    //objectObtained.PopModalAsync();
                }
                */
                //MessagingCenter.Send<GlobalHeader, string>(this, PageTitle, NaviType);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

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

        public static char HexToChar(string hex)
        {
            return (char)ushort.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        }

        public static string CharToHex(char c)
        {
            return ((ushort)c).ToString("X4");
        }
    }


    public enum NavigationType
    {
        BackNavigation,
        MasterNavigation
    }

    public class ExtraButtons
    {
        public string ButtonText { get; set; }

        public bool IsVisible { get; set; }
    }
}