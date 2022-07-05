using DongHui.OAuth.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DongHui.OAuth.Core.Helper
{
	public class HttpRequestApi
	{
		public static string DEFAULT_USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66";

		public static bool EnableDebugLog = false;
		private static void DebugLog(string msg)
		{
			if (EnableDebugLog)
			{
				Console.WriteLine(msg);
			}
		}

		public static HttpClient CreateHttpClient()
		{
			return new HttpClient(new HttpClientHandler
			{
				AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate),
				ClientCertificateOptions = ClientCertificateOption.Automatic,
				ServerCertificateCustomValidationCallback = (HttpRequestMessage message, X509Certificate2 cert, X509Chain chain, SslPolicyErrors error) => true
			});
		}

		public static async Task<string> GetStringAsync(string api, Dictionary<string, string> query = null, Dictionary<string, string> header = null)
		{
			using HttpClient httpClient = CreateHttpClient();
			if (query == null)
			{
				query = new Dictionary<string, string>();
			}
			query.RemoveEmptyValueItems();
			api = api + (api.Contains("?") ? "&" : "?") + query.ToQueryString();
			DebugLog("GET [" + api + "]");
			if (header == null)
			{
				header = new Dictionary<string, string>();
			}
			if (!header.ContainsKey("accept"))
			{
				header.Add("accept", "application/json");
			}
			if (!header.ContainsKey("User-Agent"))
			{
				header.Add("User-Agent", DEFAULT_USER_AGENT);
			}
			foreach (KeyValuePair<string, string> item in header)
			{
				httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
				DebugLog("GET Header [" + item.Key + "]=[" + item.Value + "]");
			}
			string text = await (await httpClient.GetAsync(api)).Content.ReadAsStringAsync();
			DebugLog("GET [" + api + "], reponse=[" + text + "]");
			return text;
		}

		public static async Task<T> GetAsync<T>(string api, Dictionary<string, string> query = null, Dictionary<string, string> header = null)
		{
			return JsonSerializer.Deserialize<T>(await GetStringAsync(api, query, header), (JsonSerializerOptions)null);
		}

		public static async Task<string> PostStringAsync(string api, Dictionary<string, string> form = null, Dictionary<string, string> header = null)
		{
			using HttpClient httpClient = CreateHttpClient();
			if (form == null)
			{
				form = new Dictionary<string, string>();
			}
			form.RemoveEmptyValueItems();
			DebugLog("POST [" + api + "]");
			foreach (KeyValuePair<string, string> item in form)
			{
				DebugLog("POST [" + item.Key + "]=[" + item.Value + "]");
			}
			if (header == null)
			{
				header = new Dictionary<string, string>();
			}
			if (!header.ContainsKey("accept"))
			{
				header.Add("accept", "application/json");
			}
			if (!header.ContainsKey("User-Agent"))
			{
				header.Add("User-Agent", DEFAULT_USER_AGENT);
			}
			foreach (KeyValuePair<string, string> item2 in header)
			{
				httpClient.DefaultRequestHeaders.Add(item2.Key, item2.Value);
				DebugLog("POST Header [" + item2.Key + "]=[" + item2.Value + "]");
			}
			string text = await (await httpClient.PostAsync(api, new FormUrlEncodedContent(form))).Content.ReadAsStringAsync();
			DebugLog("POST [" + api + "], reponse=[" + text + "]");
			return text;
		}

		public static async Task<string> PostJsonBodyAsync(string api, Dictionary<string, string> form = null, Dictionary<string, string> header = null)
		{
			using HttpClient httpClient = CreateHttpClient();
			httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
			if (form == null)
			{
				form = new Dictionary<string, string>();
			}
			form.RemoveEmptyValueItems();
			DebugLog("POST [" + api + "]");
			string text = JsonSerializer.Serialize<Dictionary<string, string>>(form, (JsonSerializerOptions)null);
			DebugLog("POST Body=[" + text + "]");
			if (header == null)
			{
				header = new Dictionary<string, string>();
			}
			if (!header.ContainsKey("accept"))
			{
				header.Add("accept", "application/json");
			}
			if (!header.ContainsKey("User-Agent"))
			{
				header.Add("User-Agent", DEFAULT_USER_AGENT);
			}
			foreach (KeyValuePair<string, string> item in header)
			{
				httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
				DebugLog("POST Header [" + item.Key + "]=[" + item.Value + "]");
			}
			string text2 = await (await httpClient.PostAsync(api, new StringContent(text, Encoding.UTF8, "application/json"))).Content.ReadAsStringAsync();
			DebugLog("POST [" + api + "], reponse=[" + text2 + "]");
			return text2;
		}

		public static async Task<T> PostAsync<T>(string api, Dictionary<string, string> form = null, Dictionary<string, string> header = null)
		{
			return JsonSerializer.Deserialize<T>(await PostStringAsync(api, form, header), (JsonSerializerOptions)null);
		}

		public static async Task<T> PostJsonAsync<T>(string api, Dictionary<string, string> form = null, Dictionary<string, string> header = null)
		{
			return JsonSerializer.Deserialize<T>(await PostJsonBodyAsync(api, form, header), (JsonSerializerOptions)null);
		}
	}
}
