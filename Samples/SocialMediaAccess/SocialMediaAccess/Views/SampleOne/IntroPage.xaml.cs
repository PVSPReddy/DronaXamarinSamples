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
        private async void SubmitClicked(object sender, EventArgs e)
        {
            try
            {
                //var vals = await DependencyService.Get<IWebClientServices>().getAuthKeys();
                //System.Diagnostics.Debug.WriteLine(vals);
                StartConversionSha1();
                //GetSHA1Base64(signature);
                //stackAccessPopUp.IsVisible = true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
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
                var oauth_timestamp = ((Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();
                //(Convert.ToInt64(TimeSpan.FromTicks((await DependencyService.Get<IEncryptionServices>().GetNTPTime()).Ticks).TotalSeconds)).ToString();//new TimeSpan(DateTime.Now.Ticks).TotalSeconds.ToString();//DateTime.Now.;//"1300228849";
                var oauth_consumer_key = TwitterAccessKeys.ConsumerKey;//"OqEqJeafRSF11jBMStrZz";
                var oauth_signature = "Pc%2BMLdv028fxCErFyi8KXFM%2BddU%3D";
                var oauth_version = "1.0";
                var oauth_token = TwitterAccessKeys.AccessToken;



                var step1 = "POST" + "&";
                var step2 = (await getURLEncodedValue(url)) + "&";
                var step3 = (await getURLEncodedValue(
                    "oauth_callBackURL" + "=" + callBackURL + "&" +
                    "oauth_consumer_key" + "=" + oauth_consumer_key + "&" +
                    "oauth_nonce" + "=" + oauth_nonce + "&" +
                    "oauth_signature_method" + "=" + oauth_signature_method + "&" +
                    "oauth_timestamp" + "=" + oauth_timestamp + "&" +
                    "oauth_token" + "=" + oauth_token + "&" +
                    "oauth_version" + "=" + oauth_version
                ));
                var step4 = oauth_consumer_key + "&";



                signature = step1 + step2 + step3;
                oauth_signature = signature;
                var signKey2 = await DependencyService.Get<IEncryptionServices>().GetHMACSHA1EncryptedString(signature, step4);
                System.Diagnostics.Debug.WriteLine(signKey2);

                var time1 = DateTime.Now;
                var time2 = new DateTime(1990, 01, 01);
                var meanTime = time1.Subtract(time2);
                var totalSeconds = meanTime.TotalSeconds.ToString();
                System.Diagnostics.Debug.WriteLine(totalSeconds);

                /*
                var signingKey = await GetSHA1Base64(signature);

                var finalString = "callBackURL" + ":"+ "\"" + callBackURL + "\"" + "\n" +
                    "oauth_consumer_key" + ":" + "\"" + oauth_consumer_key + "\"" + "\n" +
                    "oauth_nonce" + ":" + "\"" + oauth_nonce + "\"" + "\n" +
                    "oauth_signature_method" + ":" + "\"" + oauth_signature_method + "\"" + "\n" +
                    "oauth_timestamp" + ":" + "\"" + oauth_timestamp + "\"" + "\n" +
                    "oauth_version" + ":" + "\"" + oauth_version + "\"" + "\n" +
                    "oauth_signature" + ":" + "\"" + signKey2 + "\"" ;

                */

                /*
                var finalString = "callBackURL" + ":" + callBackURL + "\n" +
                    "oauth_consumer_key" + ":" + oauth_consumer_key + "\n" +
                    "oauth_nonce" + ":" + oauth_nonce + "\n" +
                    "oauth_signature_method" + ":" + oauth_signature_method + "\n" +
                    "oauth_timestamp" + ":" + oauth_timestamp + "\n" +
                    "oauth_version" + ":" + oauth_version + "\n" +
                    "oauth_signature" + ":" + signKey2 ;
                */



                


                var _callBackURL = await getURLEncodedValue(callBackURL);
                var _signKey2 = await getURLEncodedValue(signKey2);


                var finalString = "OAuth " +
                    "oauth_callBackURL" + "=" + "\"" + _callBackURL + "\"" + ", " +
                    "oauth_consumer_key" + "=" + "\"" + oauth_consumer_key + "\"" + ", " +
                    "oauth_nonce" + "=" + "\"" + oauth_nonce + "\"" + ", " +
                    "oauth_signature_method" + "=" + "\"" + oauth_signature_method + "\"" + ", " +
                    "oauth_timestamp" + "=" + "\"" + oauth_timestamp + "\"" + ", " +
                    "oauth_version" + "=" + "\"" + oauth_version + "\"" + ", " +
                    "oauth_token" + "=" + "\"" + oauth_token + "\"" + ", " +
                    "oauth_signature" + "=" + "\"" + _signKey2 + "\"";

                /*
                var finalString = "OAuth " +
                    "oauth_callBackURL" + "=" + "\"" + _callBackURL + "\"" + ", " +
                    "oauth_consumer_key" + "=" + "\"" + oauth_consumer_key + "\"" + ", " +
                    "oauth_nonce" + "=" + "\"" + oauth_nonce + "\"" + ", " +
                    "oauth_signature" + "=" + "\"" + _signKey2 + "\"" + ", " +
                    "oauth_signature_method" + "=" + "\"" + oauth_signature_method + "\"" + ", " +
                    "oauth_timestamp" + "=" + "\"" + oauth_timestamp + "\"" + ", " +
                    "oauth_version" + "=" + "\"" + oauth_version + "\"" ;
                */




                System.Diagnostics.Debug.WriteLine(finalString);

                /*

                var dd = Convert.ToUInt64(TimeSpan.FromTicks(DateTime.Now.Ticks).TotalMilliseconds);
                var dddd = await ConvertToNtp(dd);
                string str = "";
                foreach(var i in dddd)
                {
                    str += i.ToString();
                }
                System.Diagnostics.Debug.WriteLine(str);


                var aa = Convert.ToInt64(DateTime.Now.Ticks);
                Ticks2Ntp(aa);


                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                //var bbb = 
                System.Diagnostics.Debug.WriteLine(unixTimestamp);
                */


                using (ITwitterServices service = new TwitterServices())
                {
                    var result = service.getAuthToken(finalString);
                }
                //stackAccessPopUp.IsVisible = true;
                //webView.Source = new Uri(callBackURL);
            }
            catch (Exception ex)
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
        #endregion

        #region
        public async Task<byte[]> ConvertToNtp(ulong milliseconds)
        {
            ulong intpart = 0, fractpart = 0;
            var ntpData = new byte[8];

            intpart = milliseconds / 1000;
            fractpart = ((milliseconds % 1000) * 0x100000000L) / 1000;

            System.Diagnostics.Debug.WriteLine("intpart:      " + intpart);
            System.Diagnostics.Debug.WriteLine("fractpart:    " + fractpart);
            System.Diagnostics.Debug.WriteLine("milliseconds: " + milliseconds);

            var temp = intpart;
            for (var i = 3; i >= 0; i--)
            {
                ntpData[i] = (byte)(temp % 256);
                temp = temp / 256;
            }

            temp = fractpart;
            for (var i = 7; i >= 4; i--)
            {
                ntpData[i] = (byte)(temp % 256);
                temp = temp / 256;
            }
            return ntpData;
        }

        static long ntpEpoch = (new DateTime(1900, 1, 1)).Ticks;
        static public long Ntp2Ticks(UInt64 a)
        {
            var b = (decimal)a * 1e7m / (1UL << 32);
            return (long)b + ntpEpoch;
        }
        //static public async Task<UInt64> Ticks2Ntp(long a)
        //{
        //    decimal b = a - ntpEpoch;
        //    b = (decimal)b / 1e7m * (1UL << 32);
        //    return (UInt64)b;
        //}
        static public void Ticks2Ntp(long a)
        {
            decimal b = a - ntpEpoch;
            b = (decimal)b / 1e7m * (1UL << 32);
            System.Diagnostics.Debug.WriteLine(b.ToString());
        }

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
