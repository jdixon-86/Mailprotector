using System.Net.Http;

namespace Mailprotector_Api
{
    public static class Extensions
    {
        public static HttpClient Initialize(this HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", "Know More IT Mailprotector API");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            return client;
        }
    }
}
