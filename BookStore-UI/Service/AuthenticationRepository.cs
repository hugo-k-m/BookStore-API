using System.Text;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using BookStore_UI.Contracts;
using BookStore_UI.Models;
using BookStore_UI.Static;
using Newtonsoft.Json;

namespace BookStore_UI.Service
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IHttpClientFactory _client;

        public AuthenticationRepository(IHttpClientFactory client)
        {
            _client = client;
        }

        public async Task<bool> Register(RegistrationModel user)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Post, Endpoints.RegisterEndpoint
            );

            request.Content = new StringContent(
                JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"
            );

            HttpClient client = _client.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            return response.IsSuccessStatusCode;
        }
    }
}
