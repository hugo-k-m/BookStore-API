﻿using System.Net.Http;
using Blazored.LocalStorage;
using BookStore_UI.Contracts;
using BookStore_UI.Models;

namespace BookStore_UI.Service
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        private readonly IHttpClientFactory _client;
        private readonly ILocalStorageService _localStorage;

        public AuthorRepository(
            IHttpClientFactory client,
            ILocalStorageService localStorage
        ) : base(client, localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }
    }
}
