using System;
using System.Text;
using System.Threading.Tasks;
using SocialMediaAccess.Droid;
using Xamarin.Forms;

//[assembly: Dependency(typeof(EncryptionServices))]
namespace SocialMediaAccess.Droid
{
    public class EncryptionServices //: IEncryptionServices
    {
        public EncryptionServices() { }


        public async Task<string> GetSHA1EncryptedString(string strConvert)
        {
            string returnValue = "";
            try
            {
                //var SHA1Object = System.Security.Cryptography.SHA1.Create(strConvert);
                var SHA1Object = System.Security.Cryptography.SHA1.Create();
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                byte[] bytes = encoding.GetBytes(strConvert);
                byte[] result = SHA1Object.ComputeHash(bytes);

                var base64StringValue = Convert.ToBase64String(result, 0, result.Length);

                returnValue = base64StringValue.ToString();
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            return returnValue;
        }

        public async Task<string> GetHMACSHA1EncryptedString(string data, string key)
        {
            string returnValue = "";
            try
            {

                var baseString = data;
                var keyss = key;
                var hashAlgo = new System.Security.Cryptography.HMACSHA1(Encoding.UTF8.GetBytes(keyss));
                var hash = hashAlgo.ComputeHash(Encoding.UTF8.GetBytes(baseString));

                var base64StringValue = Convert.ToBase64String(hash, 0, hash.Length);
                returnValue = base64StringValue.ToString();


                //var SHA1Object = new System.Security.Cryptography.HMACSHA1();
                //System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                //byte[] keyBytes = encoding.GetBytes(key);
                //byte[] dataBytes = encoding.GetBytes(data);
                //var algorithm = WinRTCrypto.MacAlgorithmProvider.OpenAlgorithm(MacAlgorithm.HmacSha1);
                //CryptographicHash hasher = algorithm.CreateHash(keyBytes);
                //hasher.Append(dataBytes);
                //byte[] mac = hasher.GetValueAndReset();

                //StringBuilder sBuilder = new StringBuilder();
                //for (int i = 0; i < mac.Length; i++)
                //{
                //    sBuilder.Append(mac[i].ToString("X2"));
                //}
                //return sBuilder.ToString().ToLower();
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            return returnValue;
        }

        public async Task<DateTime> GetNTPTime()
        {
            DateTime returnValue = DateTime.Now;
            try
            {
                //default Windows time server
                const string ntpServer = "time.windows.com";

                // NTP message size - 16 bytes of the digest (RFC 2030)
                var ntpData = new byte[48];

                //Setting the Leap Indicator, Version Number and Mode values
                ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

                var addresses = System.Net.Dns.GetHostEntry(ntpServer).AddressList;

                //The UDP port number assigned to NTP is 123
                var ipEndPoint = new System.Net.IPEndPoint(addresses[0], 123);
                //NTP uses UDP

                using (var socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp))
                {
                    socket.Connect(ipEndPoint);

                    //Stops code hang if NTP is blocked
                    socket.ReceiveTimeout = 3000;

                    socket.Send(ntpData);
                    socket.Receive(ntpData);
                    socket.Close();
                }

                //Offset to get to the "Transmit Timestamp" field (time at which the reply 
                //departed the server for the client, in 64-bit timestamp format."
                const byte serverReplyTime = 40;

                //Get the seconds part
                ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);

                //Get the seconds fraction
                ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

                //Convert From big-endian to little-endian
                intPart = SwapEndianness(intPart);
                fractPart = SwapEndianness(fractPart);

                var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

                //**UTC** time
                var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);

                //return networkDateTime.ToLocalTime();
                returnValue = networkDateTime.ToLocalTime();

                /*
                const string ntpServer = "pool.ntp.org";
                var ntpData = new byte[48];
                ntpData[0] = 0x1B; //LeapIndicator = 0 (no warning), VersionNum = 3 (IPv4 only), Mode = 3 (Client Mode)

                var addresses = Dns.GetHostEntry(ntpServer).AddressList;
                var ipEndPoint = new IPEndPoint(addresses[0], 123);
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                socket.Connect(ipEndPoint);
                socket.Send(ntpData);
                socket.Receive(ntpData);
                socket.Close();

                ulong intPart = (ulong)ntpData[40] << 24 | (ulong)ntpData[41] << 16 | (ulong)ntpData[42] << 8 | (ulong)ntpData[43];
                ulong fractPart = (ulong)ntpData[44] << 24 | (ulong)ntpData[45] << 16 | (ulong)ntpData[46] << 8 | (ulong)ntpData[47];

                var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
                var networkDateTime = (new DateTime(1900, 1, 1)).AddMilliseconds((long)milliseconds);

                return networkDateTime;
                */
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            return returnValue;
        }

        // stackoverflow.com/a/3294698/162671
        static uint SwapEndianness(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) +
                           ((x & 0x0000ff00) << 8) +
                           ((x & 0x00ff0000) >> 8) +
                           ((x & 0xff000000) >> 24));
        }
    }
}

