using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections;

//Requieres JSON.NET from NuGet 

	public class SimpleRESTServiceClient<T> : ISimpleRestClient<T> where T : class
    {

		private string m_url; 

		public SimpleRESTServiceClient(string url){
			m_url = url; 
		}


		public async Task<T> Get(string id = null)
		{
			string response = await this.MakeAsyncRequest(id);
			var result = JsonConvert.DeserializeObject<T>(response);
			return result;
		}

		public async Task<IList<T>> GetAll()
		{
			var response = await this.MakeAsyncRequest();
			var result = JsonConvert.DeserializeObject<IList<T>>(response);
			return result;
		}

		private Task<string> MakeAsyncRequest(string urlAppend = null)
		{
			var request = (HttpWebRequest)WebRequest.Create(m_url + urlAppend);
			request.ContentType = "application/Json";
			return Task.Factory.FromAsync(request.BeginGetResponse, asyncResult => request.EndGetResponse(asyncResult), null)
				.ContinueWith(t => this.ReadFromStreamResponse(t.Result));
		}

		private string ReadFromStreamResponse(WebResponse response)
		{
			using (var responseStream = response.GetResponseStream())
			{
				using (var reader = new StreamReader(responseStream))
				{
					var content = reader.ReadToEnd();
					return content;
				}
			}
		}

    }
