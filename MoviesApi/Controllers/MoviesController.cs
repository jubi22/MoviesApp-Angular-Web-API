using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesApi.DTO;
using MoviesApi.Models;
using MoviesApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovies movies;
        private readonly ILogger<MoviesController> logger;
        public MoviesController(IMovies movies, ILogger<MoviesController> logger)
        {
            this.movies = movies;
            this.logger = logger;
        }
        [HttpGet("/getmovies")]
        public ActionResult<List<Movies>> Display()
        {
            logger.LogInformation("List of movies is displayed.");
           return  movies.GetMovies();           
        }
        [HttpPost("/addmovie")]
        public ActionResult<Movies> CreateMovie(Movies m)
        {
            logger.LogInformation("New movie has been added by Admin");
            this.movies.AddMovies(m);
            return Ok(m);
        }
        [HttpGet("/moviedetail/{id}")]
        public ActionResult DisplayMovieById(int id)
        {
            logger.LogInformation("List of actors casted for a particular movie is displayed");
            var  m=  this.movies.GetMovieByID(id);
            return Ok(m);
        }
        [HttpGet("/cast-actor/{id}")]
        public ActionResult DisplayActorById(string id)
        {
            logger.LogInformation("List of movies casted for a particular actor is displayed");
            var m = this.movies.GetActorByID(id);
            return Ok(m);
        }
        [HttpPost("/castmovie")]

        public ActionResult CastMovies(Movies_Actors movies_Actors)
        {
            logger.LogInformation("Actors are casted to a movie by Admin");
            this.movies.CastMovie(movies_Actors);
            return Ok(movies_Actors);
        }
        [HttpPut("/update-movie")]
        public ActionResult UpdateMovie(Movies movies)
        {
            logger.LogInformation("Movie details are updated by Admin");
            this.movies.UpdateMovie(movies);
            return Ok(movies);
        }
        [HttpDelete("/delete-movie/{id}")]
        public ActionResult DeleteMovie(int id)
        {
            logger.LogInformation("Movie is deleted by Admin");
            this.movies.DeleteMovie(id);
            return Ok("deleted");
        }
    }
}
