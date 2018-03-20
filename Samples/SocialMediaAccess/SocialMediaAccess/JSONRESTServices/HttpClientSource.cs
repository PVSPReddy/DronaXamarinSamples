using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SocialMediaAccess
{
	public class HttpClientSource<T> where T : class
	{
		static HttpClient client;

		static HttpClientSource()
		{
            client = new HttpClient();
			client.BaseAddress = new Uri(Constants.Apiurl);
			client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Add("oauth_nonce", Constants.oauth_nonce);
            client.DefaultRequestHeaders.Add("callBackURL", Constants.callBackURL);
            client.DefaultRequestHeaders.Add("oauth_signature_method", Constants.oauth_signature_method);
            client.DefaultRequestHeaders.Add("oauth_timestamp", Constants.oauth_timestamp);
            client.DefaultRequestHeaders.Add("oauth_consumer_key", Constants.oauth_consumer_key);

            client.DefaultRequestHeaders.Add("oauth_version", Constants.oauth_version);
            /*
			client.DefaultRequestHeaders.Add("X-Parse-Application-Id", Constants.ApplicationID);
			client.DefaultRequestHeaders.Add("X-Parse-REST-API-Key", Constants.ApiKey);
			//client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(new UTF8Encoding().GetBytes(Constants.Username + ":" + Constants.Password)));
            */

			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			//client.DefaultRequestHeaders.Add("secret", Constants.ServerSecretCode);
			//client.DefaultRequestHeaders.Add("securitycode", Constants.ServerSecurityCode);
            
		}

        public static async Task<T> RetriveTwitterDataWithPostAsync(string methodName)
        {
            //var jsonString = JsonConvert.SerializeObject("{}"); 
            //var postData = new StringContent("");
            client.DefaultRequestHeaders.Add("oauth_signature", methodName);
            var postData = new StringContent("", Encoding.UTF8, "application/json");
            T t = null;
            HttpResponseMessage response = await client.PostAsync("", postData);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                t = JsonConvert.DeserializeObject<T>(str);
            }
            return t;
        }


		public static async Task<string> CreateOrUpdateItemWithPostAsync(string methodName, T t)
		{
			var postData = new StringContent(JsonConvert.SerializeObject(t));
			string returnVal = "";
			try
			{
				//var postData = new StringContent(JsonConvert.SerializeObject(t));

				HttpResponseMessage response = await client.PostAsync(methodName, postData);

				response.EnsureSuccessStatusCode();
				if (response.IsSuccessStatusCode)
				{
					returnVal = await response.Content.ReadAsStringAsync();
					//t = JsonConvert.DeserializeObject<T>(str);
				}
			}
			catch (Exception ex)
			{
				var msg = ex.Message;
				returnVal = null;
			}
			return returnVal;
		}



		public static async Task<T> RetriveDataWithPostAsync(string methodName)
		{
			//var jsonString = JsonConvert.SerializeObject("{}"); 
			//var postData = new StringContent("");
            var postData = new StringContent("", Encoding.UTF8, "application/json");
			T t = null;
			HttpResponseMessage response = await client.PostAsync(methodName, postData);
			response.EnsureSuccessStatusCode();
			if (response.IsSuccessStatusCode)
			{
				var str = await response.Content.ReadAsStringAsync();
				t = JsonConvert.DeserializeObject<T>(str);
			}
			return t;
		}

		public static async Task<T> RetriveDataWithGetAsync(string methodName)
		{
			T t = null;
			HttpResponseMessage response = await client.GetAsync(methodName);
			if (response.IsSuccessStatusCode)
			{
				var str = await response.Content.ReadAsStringAsync();
				t = JsonConvert.DeserializeObject<T>(str);
			}
			return t;
		}

		public static async Task<string> UpdateItemWithPutAsync(T t, string methodNameWithIdParam)
		{
			var postData = new StringContent(JsonConvert.SerializeObject(t));
			var status = "";
			//HttpResponseMessage response = await client.PutAsync($"api/products/{Id}", postData);
			HttpResponseMessage response = await client.PutAsync(methodNameWithIdParam, postData);
			response.EnsureSuccessStatusCode();
			if (response.IsSuccessStatusCode)
			{
				status = "Success";
			}
			else
			{
				status = "Failed";
			}
			return status;
		}

		public static async Task<string> DestroyItemWithDeleteAsync(string methodNameWithIdParam)
		{
			var status = "";
			HttpResponseMessage response = await client.DeleteAsync(methodNameWithIdParam);
			if (response.StatusCode == HttpStatusCode.OK)
			{
				status = "Success";
			}
			else
			{
				status = "Failed";
			}
			return status;
		}
	}
}
