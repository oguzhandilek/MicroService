using ProductAPI.Interface;
using System.Net;

namespace ProductAPI.Helper
{
    public class ServiceCallHelper : IServiceCallHelper
    {
        public async Task<string> Post(Uri uri, HttpMethod httpMethod, StringContent content)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = httpMethod,
                    Content = content,
                    RequestUri = uri
                };
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }
        public async Task<object> Get(string uri)
        {
            using (WebClient webClient = new WebClient())
            {
                var response = webClient.DownloadString(uri);
                return response;
            }
        }


    }
}
