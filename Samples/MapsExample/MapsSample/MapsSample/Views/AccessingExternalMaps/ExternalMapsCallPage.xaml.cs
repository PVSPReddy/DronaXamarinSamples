using System;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;
using System.Linq;

namespace MapsSample
{
    public partial class ExternalMapsCallPage : ContentPage
    {
        string strSelectedUrlScheme = "", strSelectedMapType = "", strSelectedTransportType = "";
        Dictionary<string, string> dictionaryUrlScheme, dictionaryMapType, dictionaryTransportType;

        public ExternalMapsCallPage()
        {
            InitializeComponent();

            double originLatitude = 17.4286, originLongitude = 78.4331, destinationLatitude = 17.3615636, destinationLongitude = 78.47466450000002;
            dictionaryUrlScheme = new Dictionary<string, string>()
            {
                {"Route_Lat-Long_Lat-Long", string.Format("http://maps.google.com/?saddr=" + originLatitude + "," + originLongitude + "&daddr=" + destinationLatitude + "," + destinationLongitude + "")},
                {"Route from Current location to Lat-Long", string.Format("http://maps.google.com/?daddr=" + destinationLatitude + "," + destinationLongitude + "")},
                {"Display Location at center using Lat_Long", string.Format("http://maps.google.com/?ll=" + destinationLatitude + "," + destinationLongitude + "")},
                {"Display Location at center using Address", string.Format("http://maps.google.com/?address=" + "Charminar,+Hyderabad")},
                {"SearchLocation", string.Format("http://maps.google.com/?q=" + destinationLatitude + "," + destinationLongitude + "")},
                {"Search a thing at this Location", string.Format("http://maps.google.com/?q=" + "Stores"+ "&sll=" + destinationLatitude + "," + destinationLongitude + "")}
            };
            List<string> listURLPickerItems = new List<string>();
            foreach (var items in dictionaryUrlScheme)
            {
                listURLPickerItems.Add(items.Key);
            }
            urlSchemePicker.ItemsSource = listURLPickerItems;


            dictionaryMapType = new Dictionary<string, string>()
            {
                {"standard", "&t=m"},
                {"satellite", "&t=k"},
                {"hybrid", "&t=h"},
                {"transit", "&t=r"}
            };
            List<string> listMapTypePickerItems = new List<string>();
            foreach (var items in dictionaryMapType)
            {
                listMapTypePickerItems.Add(items.Key);
            }
            pickerMapType.ItemsSource = listMapTypePickerItems;


            dictionaryTransportType = new Dictionary<string, string>()
            {
                {"car", "&dirflg=d"},
                {"foot", "&dirflg=w"},
                {"public transit", "&dirflg=r"}
            };
            List<string> listTransportTypePickerItems = new List<string>();
            foreach (var items in dictionaryTransportType)
            {
                listTransportTypePickerItems.Add(items.Key);
            }
            pickerTransportType.ItemsSource = listTransportTypePickerItems;
        }

        void URLSchemeItemSelected(object sender, EventArgs e)
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

        void MapTypeItemSelected(object sender, EventArgs e)
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
                    strSelectedMapType = dictionaryMapType[owner.SelectedItem.ToString()];
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        void TransportTypeItemSelected(object sender, EventArgs e)
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
                    strSelectedTransportType = dictionaryTransportType[owner.SelectedItem.ToString()];
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
                var validUrl = strSelectedUrlScheme + strSelectedTransportType + strSelectedMapType;
                //Device.OpenUri(new Uri(mapUrl));
                Device.OpenUri(new Uri(validUrl));
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}
