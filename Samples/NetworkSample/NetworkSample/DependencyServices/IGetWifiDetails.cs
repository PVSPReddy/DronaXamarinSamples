using System;
using System.Threading.Tasks;

namespace NetworkSample
{
    public interface IGetWifiDetails
    {
        Task<WifiDetails> GetAllWifiDetails();
    }

    public class WifiDetails
    {
        public string WifiName { get; set; }

        public string WifiSSID { get; set; }

        public string WifiBSSID { get; set; }

        public string WifiSSIDData { get; set; }
    }
}
