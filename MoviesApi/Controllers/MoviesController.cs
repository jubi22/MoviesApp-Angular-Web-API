using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public MoviesController(IMovies movies)
        {
            this.movies = movies;
        }
        [HttpGet("/getmovies")]
        public ActionResult<List<Movies>> Display()
        {
           return  movies.GetMovies();           
        }
        [HttpPost("/addmovie")]
        public ActionResult<Movies> CreateMovie(Movies m)
        {
            this.movies.AddMovies(m);
            return Ok(m);
        }
        [HttpGet("/moviedetail/{id}")]
        public ActionResult DisplayMovieById(int id)
        {
            var  m=  this.movies.GetMovieByID(id);
            return Ok(m);
        }
        [HttpGet("/cast-actor/{id}")]
        public ActionResult DisplayActorById(string id)
        {
            var m = this.movies.GetActorByID(id);
            return Ok(m);
        }
        [HttpPost("/castmovie")]

        public ActionResult CastMovies(Movies_Actors movies_Actors)
        {
            this.movies.CastMovie(movies_Actors);
            return Ok(movies_Actors);
        }
        [HttpPut("/update-movie")]
        public ActionResult UpdateMovie(Movies movies)
        {
            this.movies.UpdateMovie(movies);
            return Ok(movies);
        }
        [HttpDelete("/delete-movie/{id}")]
        public ActionResult DeleteMovie(int id)
        {
            this.movies.DeleteMovie(id);
            return Ok("deleted");
        }
    }
}
