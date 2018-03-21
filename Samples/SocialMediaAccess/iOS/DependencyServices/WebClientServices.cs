using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SocialMediaAccess.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(WebClientServices))]
namespace SocialMediaAccess.iOS
{
    public class WebClientServices : IWebClientServices
    {
        public WebClientServices() { }

        public async Task<string> getAuthKeys()
        {
            string url = "https://api.twitter.com/oauth/request_token";
            string oauth_nonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture)));
            string oauth_timestamp = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds).ToString(CultureInfo.InvariantCulture);
            //string oauth_callback = "http://localhost:11761/iRouter.aspx";
            string oauth_consumer_key = "bCai4UbqEOUncO0ESTNJZd79p";
            string oauth_token = "161622333-prbFyukc3Qasv7whbgUzAYoMyVpWzuOL62CoeBdR";
            string oauth_signature_method = "HMAC-SHA1";
            string oauth_version = "1.0";
            string oauth_signature = CreateSignature(url, oauth_nonce, oauth_timestamp, oauth_token, oauth_consumer_key, oauth_signature_method, oauth_version);
            string auth_header = CreateAuthorizationHeaderParameter(oauth_signature, oauth_timestamp, oauth_nonce, oauth_signature_method, oauth_consumer_key, oauth_token, oauth_version);
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12; ;
                WebClient wc = new WebClient();
                wc.Headers.Add("Content-Type: " + "application/x-www-form-urlencoded;charset=UTF-8");
                wc.Headers.Add("Authorization: " + auth_header);
                Stream data = wc.OpenRead(url);
                StreamReader reader = new StreamReader(data);
                string retirnedJson = reader.ReadToEnd();
                data.Close();
                reader.Close();

                return retirnedJson;
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            string error = reader.ReadToEnd();
                            //TODO: use JSON.net to parse this string and look at the error message
                        }
                    }
                }
                return null;
            }
        }


        public string CreateSignature(string url, string _oauthNonce, string _oathTimestamp, string OauthToken, string OauthConsumerKey, string OauthSignatureMethod, string OathVersion)
        {
            //string builder will be used to append all the key value pairs
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("POST&");
            stringBuilder.Append(Uri.EscapeDataString(url));
            stringBuilder.Append("&");

            //the key value pairs have to be sorted by encoded key
            var dictionary = new SortedDictionary<string, string>
                             {
                                 {"oauth_nonce", _oauthNonce},
                                 {"oauth_signature_method", OauthSignatureMethod},
                                 {"oauth_timestamp", _oathTimestamp},
                                 {"oauth_consumer_key", OauthConsumerKey},
                                 {"oauth_token", OauthToken},
                                 {"oauth_version", OathVersion},
                             };

            foreach (var keyValuePair in dictionary)
            {
                //append a = between the key and the value and a & after the value
                stringBuilder.Append(Uri.EscapeDataString(string.Format("{0}={1}&", keyValuePair.Key, keyValuePair.Value)));
            }
            string signatureBaseString = stringBuilder.ToString().Substring(0, stringBuilder.Length - 3);

            //generation the signature key the hash will use
            string signatureKey =
                Uri.EscapeDataString(OauthConsumerKey) + "&" +
                Uri.EscapeDataString(OauthToken);

            var hmacsha1 = new HMACSHA1(
                new ASCIIEncoding().GetBytes(signatureKey));

            //hash the values
            string signatureString = Convert.ToBase64String(
                hmacsha1.ComputeHash(
                    new ASCIIEncoding().GetBytes(signatureBaseString)));

            return signatureString;
        }

        public string CreateAuthorizationHeaderParameter(string oauth_signature, string oauth_timestamp, string oauth_nonce, string oauth_signature_method, string oauth_consumer_key, string oauth_token, string oauth_version)
        {
            string authorizationHeaderParams = String.Empty;
            authorizationHeaderParams += "OAuth ";
            authorizationHeaderParams += "oauth_nonce=" + "\"" +
                                         Uri.EscapeDataString(oauth_nonce) + "\",";

            authorizationHeaderParams +=
                "oauth_signature_method=" + "\"" +
                Uri.EscapeDataString(oauth_signature_method) +
                "\",";

            authorizationHeaderParams += "oauth_timestamp=" + "\"" +
                                         Uri.EscapeDataString(oauth_timestamp) + "\",";

            authorizationHeaderParams += "oauth_consumer_key="
                                         + "\"" + Uri.EscapeDataString(oauth_consumer_key) + "\",";

            authorizationHeaderParams += "oauth_token=" + "\"" +
                                         Uri.EscapeDataString(oauth_token) + "\",";

            authorizationHeaderParams += "oauth_signature=" + "\""
                                         + Uri.EscapeDataString(oauth_signature) + "\",";

            authorizationHeaderParams += "oauth_version=" + "\"" +
                                         Uri.EscapeDataString(oauth_version) + "\"";
            return authorizationHeaderParams;
        }
    }
}

