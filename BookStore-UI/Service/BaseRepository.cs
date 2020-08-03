﻿using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BookStore_UI.Contracts;
using Newtonsoft.Json;

namespace BookStore_UI.Service
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IHttpClientFactory _client;

        public BaseRepository(IHttpClientFactory client)
        {
            _client = client;
        }

        public async Task<bool> Create(string url, T obj)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

            if (obj == null)
            return false;

            request.Content = new StringContent(JsonConvert.SerializeObject(obj));
            HttpClient client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            return true;

            return false;
        }

        public async Task<bool> Delete(string url, int id)
        {
            if (id < 1)
            return false;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url + id);
            HttpClient client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            return true;

            return false;
        }

        public async Task<T> Get(string url, int id)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url + id);
            HttpClient client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                
                return JsonConvert.DeserializeObject<T>(content);
            }

            return null;
        }

        public async Task<IList<T>> Get(string url)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            HttpClient client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                
                return JsonConvert.DeserializeObject<IList<T>>(content);
            }

            return null;
        }

        public async Task<bool> Update(string url, T obj)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, url);

            if (obj == null)
            return false;

            request.Content = new StringContent(
                JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json"
            );

            HttpClient client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            return true;

            return false;
        }
    }
}