using System;
using System.Collections.Generic;
using System.ComponentModel;
using DynamicXAML.ValueConverters;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DynamicXAML
{
    public partial class GlobalHeaderOne : ContentView, INotifyPropertyChanged
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
                    //labelPageTitle.Text = value;
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
                    //labelNaviText.Text = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("NaviImage"));
                }

            }
        }

        public bool ShowThirdButton { get; set; }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        //public object PageNavigation { get; set; }
        //public GlobalHeader(string naviType, string naviImage, string pageTitle, bool showThirdButton )

        public GlobalHeaderOne(bool showThirdButton)
        {
            /*
            MessagingCenter.Subscribe<HeaderValues, string[]>(this, "headerValues", (sender, args) =>
            {
                var allValues = (string[])args;
                NaviType = allValues[0];
                PageTitle = allValues[1];
                NaviImage = allValues[2];
            });
            */
            /*
            NaviType = naviType;
            if(NaviType == "Master")
            {
                NaviImage = TextToUniCodeSymbolCoverter.GetSymbolValue("HamBurger");
            }
            else
            {
                NaviImage = TextToUniCodeSymbolCoverter.GetSymbolValue("LeftArrow");
            }
            //if (NaviType == "Master")
            //{
            //    NaviImage = "HamBurger";
            //}
            //else
            //{
            //    NaviImage = "LeftArrow";
            //}
            PageTitle = pageTitle;
            */
            //NaviImage = naviImage;
            ShowThirdButton = showThirdButton;
            //PageNavigation = pageNavigation;

            BindingContext = this;

            //HeightRequest = 50;
            this.SetBinding(ContentView.HeightRequestProperty, new Binding(".", BindingMode.Default, new HeightConverter(), 10, null, null));
            //this.SetBinding(NavigationProperty, new );

            InitializeComponent();
            /*
            if(naviType == "Master")
            {
                labelNaviText.Text = HexToChar("2630").ToString();//@"\u2630;";
            }
            else
            {
                labelNaviText.Text = HexToChar("2190").ToString();//@"\u2190;";
            }

            labelPageTitle.Text = pageTitle;
            labelThirdButton.IsVisible = showThirdButton;
            */
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
                MessagingCenter.Send<GlobalHeaderOne, string>(this, PageTitle, NaviType);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        void AddMemoryClick(object sender, EventArgs args)
        {
            try
            {
                MessagingCenter.Send<GlobalHeaderOne>(this, "Add");
                //HomePage.homePage.Navigation.PushModalAsync(new CreateMemory());
                //DisplayAlert("ALert", "HelloWorld", "Ok");
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public static char HexToChar(string hex)
        {
            return (char)ushort.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        }

        public static string CharToHex(char c)
        {
            return ((ushort)c).ToString("X4");
        }
    }


    //public enum NavigationType
    //{
    //    BackNavigation,
    //    MasterNavigation
    //}

    //public class ExtraButtons
    //{
    //    public string ButtonText { get; set; }

    //    public bool IsVisible { get; set; }
    //}

    public class HeaderValues
    {
        public HeaderValues(string pageType, string pageTitles, string pageImage)
        {
            string[] headerValues = new string[]
            {
                pageType,
                pageTitles,
                pageImage
            };

            MessagingCenter.Send<HeaderValues, string[]>(this, "headerValues", headerValues );
        }
    }
}
