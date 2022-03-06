using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StefsMovieShop.Server.Data;
using StefsMovieShop.Shared.Dtos;
using StefsMovieShop.Shared.Models;

namespace StefsMovieShop.Server.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        //fields
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        //ctor
        public MoviesController(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        //Methods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            //Get data
            var movies = await db.Movies.ToListAsync();

            //Get dtos (via AutoMapper)
            var dtos = mapper.Map<IEnumerable<MovieDto>>(movies);

            //return
            return Ok(dtos);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            //Get Data
            var movie = await db.Movies.FindAsync(id);
            if (movie == null) return NotFound();

            //Get Dto
            var dto = mapper.Map<MovieDto>(movie);

            //return
            return dto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieDto dto)
        {

            //Let's use a try catch
            try
            {
                //Zoek domain-object
                if (id != dto.Id) return BadRequest();
                var movie = await db.Movies.FindAsync(id);
                if (movie == null) return NotFound();

                //Update
                mapper.Map(dto, movie);

                //Save Changes
                await db.SaveChangesAsync();  //changes are automatic detected

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Put Failure");
            }

            //if all ok
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<MovieDto>> PostMovie(MovieDto dto)
        {
            //convert dto to movie-object 
            var movie = mapper.Map<Movie>(dto);

            //Add en Save
            db.Movies.Add(movie);
            await db.SaveChangesAsync();

            //Get new Id
            dto.Id = movie.Id;

            //If all Ok 
            return CreatedAtAction("GetMovie", new { id = movie.Id }, dto);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await db.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            db.Movies.Remove(movie);
            await db.SaveChangesAsync();

            return NoContent();
        }

        //Extra
        private bool MovieExists(int id)
        {
            return db.Movies.Any(e => e.Id == id);
        }
    }
}
