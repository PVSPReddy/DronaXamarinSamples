using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SocialMediaAccess.Views.SampleOne
{
    public partial class IntroPage : ContentPage
    {
        public IntroPage()
        {
            InitializeComponent();


            /*
            Request URL:
            POST https://api.twitter.com/oauth/request_token
            Request POST Body:
            N / A
            Authorization Header:
            OAuth oauth_nonce = "K7ny27JTpKVsTgdyLdDfmQQWVLERj2zAK5BslRsqyw",
            oauth_callback = "http%3A%2F%2Fmyapp.com%3A3005%2Ftwitter%2Fprocess_callback",
            oauth_signature_method = "HMAC-SHA1",
            oauth_timestamp = "1300228849",
            oauth_consumer_key = "OqEqJeafRSF11jBMStrZz",
            oauth_signature = "Pc%2BMLdv028fxCErFyi8KXFM%2BddU%3D",
            oauth_version = "1.0"
            */

        }

        #region for button submit clicked
        private void SubmitClicked(object sender, EventArgs e)
        {
            try
            {
                StartConversionSha1();
                //GetSHA1Base64(signature);
                //stackAccessPopUp.IsVisible = true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
        #endregion


        #region for close popup tapped event
        private void ClosePopupTapped(object sender, EventArgs e)
        {
            try
            {
                stackAccessPopUp.IsVisible = false;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        #endregion


        #region for URL Encoding of strings
        public async Task<string> getURLEncodedValue(string strConvert)
        {
            string returnValue = "";
            try
            {
                returnValue = System.Net.WebUtility.UrlEncode(strConvert);
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
            }
            return returnValue;
        }
        #endregion

        #region 
        public async void StartConversionSha1()
        {
            try
            {
                string signature = "";
                var url = "https://api.twitter.com/oauth/request_token";
                var oauth_nonce = entryNewKey.Text ?? "K7ny27JTpKVsTgdyLdDfmQQWVLERj2zAK5BslRsqyw";//"K7ny27JTpKVsTgdyLdDfmQQWVLERj2zAK5BslRsqyw";
                var callBackURL = "https://webuipractice.herokuapp.com/SampleOne/index2.html";
                var oauth_signature_method = "HMAC-SHA1";
                var oauth_timestamp = DateTime.Now.Ticks;//"1300228849";
                var oauth_consumer_key = TwitterAccessKeys.ConsumerKey;//"OqEqJeafRSF11jBMStrZz";
                var oauth_signature = "Pc%2BMLdv028fxCErFyi8KXFM%2BddU%3D";
                var oauth_version = "1.0";



                var step1 = "POST" + "&";
                var step2 = (await getURLEncodedValue(url)) + "&";
                var step3 = (await getURLEncodedValue(
                    "callBackURL" + "=" + callBackURL + "&" +
                    "oauth_consumer_key" + "=" + oauth_consumer_key + "&" +
                    "oauth_nonce" + "=" + oauth_nonce + "&" +
                    "oauth_signature_method" + "=" + oauth_signature_method + "&" +
                    "oauth_timestamp" + "=" + oauth_timestamp + "&" +
                    "oauth_version" + "=" + oauth_version
                ));
                var step4 = oauth_consumer_key + "&";



                signature = step1 + step2 + step3;
                oauth_signature = signature;
                var signKey2 = await DependencyService.Get<IEncryptionServices>().GetHMACSHA1EncryptedString(signature, step4);
                System.Diagnostics.Debug.WriteLine(signKey2);

                /*
                var signingKey = await GetSHA1Base64(signature);
                var finalString = "callBackURL" + ":"+ "\"" + callBackURL + "\"" + "\n" +
                    "oauth_consumer_key" + ":" + "\"" + oauth_consumer_key + "\"" + "\n" +
                    "oauth_nonce" + ":" + "\"" + oauth_nonce + "\"" + "\n" +
                    "oauth_signature_method" + ":" + "\"" + oauth_signature_method + "\"" + "\n" +
                    "oauth_timestamp" + ":" + "\"" + oauth_timestamp + "\"" + "\n" +
                    "oauth_version" + ":" + "\"" + oauth_version + "\"" + "\n" +
                    "oauth_signature" + ":" + "\"" + signingKey + "\"" ;

                System.Diagnostics.Debug.WriteLine(finalString);
                */


                using(ITwitterServices service = new TwitterServices())
                {
                    //var result = service.getAuthToken(signingKey);
                }
                //stackAccessPopUp.IsVisible = true;
                //webView.Source = new Uri(callBackURL);
            }
            catch(Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
            }
        }
        public async Task<string> GetSHA1Base64(string strConvert)
        {
            string returnValue = "";
            try
            {
                returnValue = await DependencyService.Get<IEncryptionServices>().GetSHA1EncryptedString(strConvert);
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
            }
            return returnValue;
        }

        //public async Task<string> GetHMACSHA1EncryptedString(string key, string data)
        //{
        //    string returnValue = "";
        //    try
        //    {
        //        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        //        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        //        //var algorithm = WinRTCrypto.MacAlgorithmProvider.OpenAlgorithm(MacAlgorithm.HmacSha1);
        //        //CryptographicHash hasher = algorithm.CreateHash(keyBytes);
        //        //hasher.Append(dataBytes);
        //        //byte[] mac = hasher.GetValueAndReset();

        //        //StringBuilder sBuilder = new StringBuilder();
        //        //for (int i = 0; i < mac.Length; i++)
        //        //{
        //        //    sBuilder.Append(mac[i].ToString("X2"));
        //        //}
        //        //return sBuilder.ToString().ToLower();
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message + "\n" + ex.StackTrace;
        //        System.Diagnostics.Debug.WriteLine(msg);
        //    }
        //    return returnValue;
        //}

        #endregion
    }


    public static class TwitterAccessKeys
    {
        //https://developer.twitter.com/en/docs/accounts-and-users/subscribe-account-activity/overview
        //https://apps.twitter.com/
        public const string ConsumerKey = "bCai4UbqEOUncO0ESTNJZd79p";
        public const string ConsumerSecret = "Qi3Xdv3NRGyoEWnaCkg7uDSGAapqPT76gXPP0LWMqDl3CqJTMe";
        public const string AccessToken = "161622333-prbFyukc3Qasv7whbgUzAYoMyVpWzuOL62CoeBdR";
        public const string AccessTokenSecret = "I5nY6nq5oLv8b80Ui6vqD3sbTO3PPSFqVtmGB2pFFWqs0";
    }


}
