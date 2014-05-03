namespace Portable.ServiceConsumer
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    public class JsonServiceClient<T> : IRestServiceClient<T> where T : class
    {
        private string url;

        public JsonServiceClient(string url)
        {
            this.url = url;
        }

        public async Task<T> Get(string id)
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

        public async Task<IList<T>> GetAll(string serviceParameter)
        {
            var tempUrl = this.url;
            this.url += serviceParameter;
            var response = await this.MakeAsyncRequest();
            var result = JsonConvert.DeserializeObject<IList<T>>(response);
            this.url = tempUrl;
            return result;
        }

            private Task<string> MakeAsyncRequest(string urlAppend = "")
            {
                var request = (HttpWebRequest)WebRequest.Create(this.url + urlAppend);
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

        public async Task<T> Post(T dto)
        {
            var postData = JsonConvert.SerializeObject(dto);
            var request = (HttpWebRequest)WebRequest.Create(this.url);
            request.ContentType = "application/Json";
            request.Method = "POST";
            return await request.CreateWebRequestTask<T>(() => Encoding.UTF8.GetBytes(postData));
        }

        public async Task<T> Put(T dto)
        {
            var postData = JsonConvert.SerializeObject(dto);
            var request = (HttpWebRequest)WebRequest.Create(this.url);
            request.ContentType = "application/Json";
            request.Method = "PUT";
            return await request.CreateWebRequestTask<T>(() => Encoding.UTF8.GetBytes(postData));
        }
    }
}
