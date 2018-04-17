using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using AT_ASP.Web.Models;

namespace WebApp_Api.Controllers.HttpClientApi
{
    public class HttpApiClient
    {
        private readonly HttpClient _client;

        public HttpApiClient()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:50898/")
            };

            //Clean HeaderRequest default
            _client.DefaultRequestHeaders.Accept.Clear();

            //Config HeaderRequest to JSON format
            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(mediaType);
        }

        //Autores
        public async Task<HttpResponseMessage> GetAutoresAsync()
        {
            return await _client.GetAsync("api/Autores");
        }

        public async Task<HttpResponseMessage> GetAutorAsync(int id)
        {
            return await _client.GetAsync($"api/Autores/{id}");
        }

        public async Task<HttpResponseMessage> PutAutorAsync(AutorDetails model)
        {
            return await _client.PutAsJsonAsync($"api/Autores/{model.Id}", model);
        }

        public async Task<HttpResponseMessage> PostAutorAsync(AutorDetails model)
        {
            return await _client.PostAsJsonAsync("api/Autores", model);
        }

        public async Task<HttpResponseMessage> DeleteAutorAsync(int id)
        {
            return await _client.DeleteAsync($"api/Autores/{id}");
        }


        //Livros
        public async Task<HttpResponseMessage> GetLivrosAsync()
        {
            return await _client.GetAsync("api/Livros");
        }

        public async Task<HttpResponseMessage> GetLivroByIdAsync(int id)
        {
            return await _client.GetAsync($"api/Livros/{id}");
        }

        public async Task<HttpResponseMessage> PutLivroAsync(LivroDetails model)
        {
            return await _client.PutAsJsonAsync($"api/Livros/{model.Id}", model);
        }

        public async Task<HttpResponseMessage> PostLivroAsync(LivroDetails model)
        {
            return await _client.PostAsJsonAsync("api/Livros", model);
        }

        public async Task<HttpResponseMessage> DeleteLivroAsync(int id)
        {
            return await _client.DeleteAsync($"api/Livros/{id}");
        }
    }
}