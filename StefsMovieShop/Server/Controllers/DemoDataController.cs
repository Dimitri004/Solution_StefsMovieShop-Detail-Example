using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StefsMovieShop.Server.Data;
using StefsMovieShop.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StefsMovieShop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoDataController : ControllerBase
    {

        //fields
        private readonly ApplicationDbContext db;

        //ctor
        public DemoDataController(ApplicationDbContext db)
        {
            this.db = db;
        }

        //Methods
        [Route("/InsertMovies")]
        public string InsertMovies()
        {
            //Controle 
            if (db.Movies.Any())
            {
                return "Movies bevat reeds data !";
            }

            //1. Maak List met demo-data
            List<Movie> movies = new()
            {
                new Movie()
                {
                    Id = 1,
                    TmdbId = 581734,
                    Title = "Nomadland ",
                    Overview = "A woman in her sixties embarks on a journey through the western United States after losing everything in the Great Recession, living as a van-dwelling modern-day nomad.",
                    Tagline = "See you down the road. "
                },
                new Movie()
                {
                    Id = 2,
                    TmdbId = 600354,
                    Title = "The Father ",
                    Overview = "A man refuses all assistance from his daughter as he ages and, as he tries to make sense of his changing circumstances, he begins to doubt his loved ones, his own mind and even the fabric of his reality.",
                    Tagline = " "
                },
                new Movie()
                {
                    Id = 3,
                    TmdbId = 583406,
                    Title = "Judas and the Black Messiah ",
                    Overview = "Bill O'Neal infiltrates the Black Panthers on the orders of FBI Agent Mitchell and J. Edgar Hoover. As Black Panther Chairman Fred Hampton ascends—falling for a fellow revolutionary en route—a battle wages for O’Neal’s soul.",
                    Tagline = "You can kill a revolutionary but you can't kill the revolution. "
                },
                new Movie()
                {
                    Id = 4,
                    TmdbId = 614560,
                    Title = "Mank ",
                    Overview = "1930s Hollywood is reevaluated through the eyes of scathing social critic and alcoholic screenwriter Herman J. Mankiewicz as he races to finish the screenplay of Citizen Kane.",
                    Tagline = " "
                },
                new Movie()
                {
                    Id = 5,
                    TmdbId = 615643,
                    Title = "Minari ",
                    Overview = "A Korean-American family moves to Arkansas in search of their own American Dream. With the arrival of their sly, foul-mouthed, but incredibly loving grandmother, the stability of their relationships is challenged even more in this new life in the rugged Ozarks, testing the undeniable resilience of family and what really makes a home.",
                    Tagline = " "
                },
                new Movie()
                {
                    Id = 6,
                    TmdbId = 582014,
                    Title = "Promising Young Woman ",
                    Overview = "A young woman, traumatized by a tragic event in her past, seeks out vengeance against those who crossed her path.",
                    Tagline = "Revenge never looked so promising. "
                },
                new Movie()
                {
                    Id = 7,
                    TmdbId = 502033,
                    Title = "Sound of Metal ",
                    Overview = "Metal drummer Ruben begins to lose his hearing. When a doctor tells him his condition will worsen, he thinks his career and life is over. His girlfriend Lou checks the former addict into a rehab for the deaf hoping it will prevent a relapse and help him adapt to his new life. After being welcomed and accepted just as he is, Ruben must choose between his new normal and the life he once knew.",
                    Tagline = "Music was his world. Then silence revealed a new one. "
                },
                new Movie()
                {
                    Id = 8,
                    TmdbId = 556984,
                    Title = "The Trial of the Chicago 7 ",
                    Overview = "What was supposed to be a peaceful protest turned into a violent clash with the police. What followed was one of the most notorious trials in history.",
                    Tagline = "In 1968, democracy refused to back down. "
                },

            };

            //2. Toevoegen aan context en Save Changes         
            using (var transaction = db.Database.BeginTransaction())
            {
                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT MOVIES ON;");
                foreach (var item in movies)
                {
                    db.Movies.Add(item);
                }
                db.SaveChanges();
                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT MOVIES OFF");
                transaction.Commit();
            }

            //3. SaveChanges
            db.SaveChanges();

            //4. return
            return $"{movies.Count} Movies Inserted …";
        }

    }
}
