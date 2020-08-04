using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookStore_UI.Contracts;
using BookStore_UI.Models;

namespace BookStore_UI.Service
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        private IHttpClientFactory _client;

        public AuthorRepository(IHttpClientFactory client) : base(client)
        {
            _client = client;
        }
    }
}
