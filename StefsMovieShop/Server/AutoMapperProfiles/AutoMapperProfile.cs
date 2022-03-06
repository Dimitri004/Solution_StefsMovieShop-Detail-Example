using AutoMapper;
using StefsMovieShop.Shared.Dtos;
using StefsMovieShop.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StefsMovieShop.Server.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        //ctor
        public AutoMapperProfile()
        {
            CreateMap<Movie, MovieDto>();
            CreateMap<MovieDto, Movie>();
        }

    }
}
