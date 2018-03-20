using System;

#region Created by Sivaprasad

namespace SocialMediaAccess
{
	public static class Constants
	{
		// INCALERT DEVELOPMENT CREDENTIALS
		public const string ApplicationID = "";
		public const string ApiKey = "";
        public const string Apiurl = "https://api.twitter.com/oauth/request_token";
        //"http://phpprojectservices.gearhostpreview.com/ProjFiles/PHPServices/CountriesBasicData/ReceiveService.php?userRequest=GetCountriesDialCodesOnly";
		public const string MasterKey = "";


        public const string url = "https://api.twitter.com/oauth/request_token";
        public const string oauth_nonce = "K7ny27JTpKVsTgdyLdDfmQQWVLERj2zAK5BslRsqyw";
        public const string callBackURL = "https://api.twitter.com/oauth/request_token";
        public const string oauth_signature_method = "HMAC-SHA1";
        public const string oauth_timestamp = "1300228849";
        public const string oauth_consumer_key = "bCai4UbqEOUncO0ESTNJZd79p";//"OqEqJeafRSF11jBMStrZz";
        public const string oauth_signature = "Pc%2BMLdv028fxCErFyi8KXFM%2BddU%3D";
        public const string oauth_version = "1.0";

        public const string ConsumerKey = "bCai4UbqEOUncO0ESTNJZd79p";
        public const string ConsumerSecret = "Qi3Xdv3NRGyoEWnaCkg7uDSGAapqPT76gXPP0LWMqDl3CqJTMe";
        public const string AccessToken = "161622333-prbFyukc3Qasv7whbgUzAYoMyVpWzuOL62CoeBdR";
        public const string AccessTokenSecret = "I5nY6nq5oLv8b80Ui6vqD3sbTO3PPSFqVtmGB2pFFWqs0";


		public const string ServerSecretCode = "8c87b37245229d86d55b8b5ffe513fd1";
		public static string ServerSecurityCode;
		public static bool IsNotified;
		public static string rem_Ids;
		//public static IDatabaseMethods dbmethods;

		//Used in Installation for push notification.
		public const string AppIdentifier = "com.incapp.incalertdev";
		public const string AppName = "OnePosInventory";
		public const string AppVersion = "1";
		public const string ParseVersion = "1.6.2.0";
		public const string GCMSenderId = "558839287361";


		//SportzBee PrivacyPolicy URL.
		public const string PrivacyPolicyURL = "";

		//Error Message to show.
		public const string ReportErrorMessage = "Sorry for error, please contact us incase this error repeats";

        //public static Constants()
        //{
        //	//dbmethods = new DatabaseMethods();
        //}



        //stored user details
        public static string userCountry = "";
        public static string userMobileNumber = "";
	}
}

#endregion