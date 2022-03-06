using StefsMovieShop.Client.Interfaces;
using StefsMovieShop.Shared.Dtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StefsMovieShop.Client.Repos
{
    public class MoviesRepo : IMoviesRepo
    {
        //fields
        private readonly HttpClient http;
        private readonly HttpClient https;
        private string url = "/api/movies";

        //ctor   
        public MoviesRepo(IHttpClientFactory httpClientFactory)
        {
            http = httpClientFactory.CreateClient("NotSecured");
            https = httpClientFactory.CreateClient("Secured");
        }

        public async Task<IEnumerable<MovieDto>> GetMovies()
        {
            return await http.GetFromJsonAsync<IEnumerable<MovieDto>>($"{url}");
        }

        public async Task<MovieDto> GetMovie(int id)
        {
            return await https.GetFromJsonAsync<MovieDto>($"{url}/{id}");
        }

  
    }
}
