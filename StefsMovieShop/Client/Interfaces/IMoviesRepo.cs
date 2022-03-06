using StefsMovieShop.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StefsMovieShop.Client.Interfaces
{
    public interface IMoviesRepo
    {
        Task<MovieDto> GetMovie(int id);
        Task<IEnumerable<MovieDto>> GetMovies();
    }
}
