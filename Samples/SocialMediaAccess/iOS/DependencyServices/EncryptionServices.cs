using System;
using System.Text;
using System.Threading.Tasks;
using SocialMediaAccess.iOS;
using Xamarin.Forms;

[assembly : Dependency(typeof(EncryptionServices))]
namespace SocialMediaAccess.iOS
{
    public class EncryptionServices : IEncryptionServices
    {
        public EncryptionServices(){}






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

    }
}

