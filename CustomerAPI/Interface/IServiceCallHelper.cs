namespace CustomerAPI.Interface
{
    public interface IServiceCallHelper
    {
        Task<string> Post(Uri uri, HttpMethod httpMethod, StringContent content);
        Task<object> Get(string uri);
    }
}
