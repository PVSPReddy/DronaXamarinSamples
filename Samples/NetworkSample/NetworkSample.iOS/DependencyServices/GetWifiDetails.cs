using System;
using System.Threading.Tasks;
using Foundation;
using NetworkSample.iOS;
using SystemConfiguration;
using Xamarin.Forms;

[assembly : Dependency(typeof(GetWifiDetails))]
namespace NetworkSample.iOS
{
    public class GetWifiDetails : IGetWifiDetails
    {
        public GetWifiDetails(){}

        public async Task<WifiDetails> GetAllWifiDetails()
        {
            bool withMacAddress = true;
            WifiDetails wifiDetails = null;
            try
            {

                NSDictionary dict = null;

                var status = CaptiveNetwork.TryCopyCurrentNetworkInfo("en0", out dict);

                if (status == StatusCode.NoKey)
                {
                    return wifiDetails;
                }

                var bssid = dict[CaptiveNetwork.NetworkInfoKeyBSSID];
                var ssid = dict[CaptiveNetwork.NetworkInfoKeySSID];

                wifiDetails = new WifiDetails()
                {
                    WifiSSID = ssid.ToString(),
                    WifiBSSID = bssid.ToString()
                };

                System.Diagnostics.Debug.WriteLine(bssid);
                System.Diagnostics.Debug.WriteLine(ssid);


                //if (withMacAddress)
                //    return string.Format("{0} [{1}]", ssid, bssid);
                //else
                    //return ssid.ToString();
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            return wifiDetails;
        }
    }
}

