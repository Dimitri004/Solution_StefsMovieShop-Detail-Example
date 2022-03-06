using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StefsMovieShop.Shared.Dtos
{
    public class MovieDto
    {
        //properties
        public int Id { get; set; }
        public int TmdbId { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Tagline { get; set; }

    }
}
