using Mailprotector_Api.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mailprotector_Api
{
    public class MailprotectorApi
    {
        private static readonly HttpClient client = new HttpClient();

        private readonly string Token;

        public MailprotectorApi(string baseUrl, string token)
        {
            Token = token;
            client.BaseAddress = new Uri(baseUrl);
        }

        /// <summary>
        /// Create a new customer
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="accountEmail"></param>
        /// <param name="resellerId"></param>
        /// <returns></returns>
        public async Task<Customer> CreateCustomer(string accountName, string accountEmail, int resellerId)
        {
            var body  = new {
                name = accountName,
                email = accountEmail
            };

            var response = await client.Initialize(Token).PostAsync($"/api/v1/resellers/{resellerId}/customers", SerializeObject(body));
            var stringResult = await response.Content.ReadAsStringAsync();

            Console.WriteLine(stringResult);
            if (response.StatusCode != System.Net.HttpStatusCode.Created)
            {
                throw new Exception($"[{response.StatusCode}]: {stringResult}");
            }

            return JsonConvert.DeserializeObject<Customer>(stringResult);
        }

        /// <summary>
        /// Edit an existing customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="newName"></param>
        /// <param name="newEmail"></param>
        /// <returns></returns>
        public async Task<Customer> EditCustomer(int customerId, string newName, string newEmail)
        {
            var body = new
            {
                name = newName,
                email = newEmail
            };

            var response = await client.Initialize(Token).PutAsync($"/api/v1/customers/{customerId}", SerializeObject(body)).ConfigureAwait(false);
            var stringResult = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"[{response.StatusCode}]: {stringResult}");
            }

            return JsonConvert.DeserializeObject<Customer>(stringResult);
        }

        /// <summary>
        /// Deletes a customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<int> DeleteCustomer(int customerId)
        {
            var response = await client.Initialize(Token).DeleteAsync($"/api/v1/customers/{customerId}").ConfigureAwait(false);
            var stringResult = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"[{response.StatusCode}]: {stringResult}");
            }

            return (int)response.StatusCode;
        }

        /// <summary>
        /// Serialize an object and return a StringContent object to be used with a request
        /// </summary>
        /// <param name="objectToSerialize"></param>
        /// <returns></returns>
        private StringContent SerializeObject(object objectToSerialize)
        {
            string payload = JsonConvert.SerializeObject(objectToSerialize);
            return new StringContent(payload, Encoding.UTF8, "application/json");
        }
    }
}
