using System;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;
using System.Linq;

namespace MapsSample
{
    public partial class ExternalMapsCallPage : ContentPage
    {
        string strSelectedUrlScheme = "";
        Dictionary<string, string> dictionaryUrlScheme;

        public ExternalMapsCallPage()
        {
            InitializeComponent();

            double originLatitude = 17.4286, originLongitude = 78.4331, destinationLatitude = 17.3615636, destinationLongitude = 78.47466450000002;
            dictionaryUrlScheme = new Dictionary<string, string>()
            {
                {"Route_Lat-Long_Lat-Long", string.Format("http://maps.google.com/?saddr=" + originLatitude + "," + originLongitude + "&daddr=" + destinationLatitude + "," + destinationLongitude + "")},
                {"SearchLocation", string.Format("http://maps.google.com/?q=" + destinationLatitude + "," + destinationLongitude + "")}
            };
            List<string> listPickerItems = new List<string>();
            foreach (var items in dictionaryUrlScheme)
            {
                listPickerItems.Add(items.Key);
            }
            urlSchemePicker.ItemsSource = listPickerItems;
        }

        void PickerItemSelected(object sender, EventArgs e)
        {
            try
            {
                var owner = ((Picker)sender);
                if (owner.SelectedIndex <= -1)
                {
                    return;
                }
                else
                {
                    //strSelectedUrlScheme = owner.SelectedItem.ToString();
                    strSelectedUrlScheme = dictionaryUrlScheme[owner.SelectedItem.ToString()];
                    //var selectedKey = dictionaryUrlScheme.Keys.Where(X => X == owner.SelectedItem.ToString()).ToString();
                    //int IndexofSelectedValue = dictionaryUrlScheme.Keys.ToList().IndexOf(owner.SelectedItem.ToString());
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        void GoToInbuiltMaps(object sender, EventArgs e)
        {
            string mapUrl = "";
            double latitude = 17.3615636, longitude = 78.47466450000002;
            try
            {
                switch (Device.RuntimePlatform)
                {
                    case "Android":
                        mapUrl = string.Format("http://maps.google.com/?daddr=" + latitude + "," + longitude + "");
                        break;
                    case "iOS":
                        //var _url = WebUtility.UrlEncode("http://maps.apple.com/?ll" + latitude + "," + longitude + "");
                        mapUrl = string.Format("http://maps.google.com/?daddr=" + latitude + "," + longitude + "");
                        //mapUrl = string.Format("http://maps.apple.com/?q" + latitude + "," + longitude + "");
                        break;
                    case "Windows":
                        mapUrl = string.Format("http://maps.google.com/?daddr=" + latitude + "," + longitude + "");
                        break;
                    default:
                        mapUrl = string.Format("http://maps.google.com/?daddr=" + 17.4286 + "," + 78.4331 + "");
                        break;
                }
                //Device.OpenUri(new Uri(mapUrl));
                Device.OpenUri(new Uri(strSelectedUrlScheme));
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}
