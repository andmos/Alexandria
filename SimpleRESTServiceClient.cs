using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

//Requires Newtonsofts JSON.net package from NUGet
//PCL? Yes we can!
namespace Common.Services
{
    public class SimpleRESTServiceClient
    {
	public async Task<object> GetAsync(string url)
        {
        var request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
	    WebResponse responseObject = await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, 
					request.EndGetResponse, request);
            var responseStream = responseObject.GetResponseStream();
            var sr = new StreamReader(responseStream);
            string content = await sr.ReadToEndAsync();
            
			return JsonConvert.DeserializeObject(content);
        }

    }
}
