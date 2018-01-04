using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListViewExample
{

	public static class Constants
	{ 

		// INCALERT DEVELOPMENT CREDENTIALS
		public const string ApplicationID = "";
		public const string ApiKey = ""; 
        public const string Apiurl = "http://trailswebhost.gearhostpreview.com/index.php";
		public const string MasterKey = "";


		public const string ServerSecretCode = "8c87b37245229d86d55b8b5ffe513fd1";
		public static string ServerSecurityCode ;
		public static bool IsNotified;
		public static string rem_Ids;
		//public static IDatabaseMethods dbmethods;

		//Used in Installation for push notification.
		public const string AppIdentifier = "com.incapp.incalertdev";
		public const string AppName = "Corp Alert";
		public const string AppVersion = "1";
		public const string ParseVersion = "1.6.2.0";
		public const string GCMSenderId = "983395246302"; 


		//SportzBee PrivacyPolicy URL.
		public const string PrivacyPolicyURL = "";

		//Error Message to show.
		public const string ReportErrorMessage = "Sorry for error, please contact us incase this error repeats";

		//public static Constants()
		//{
		//	//dbmethods = new DatabaseMethods();
		//}
	}
}
