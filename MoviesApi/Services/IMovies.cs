using MoviesApi.DTO;
using MoviesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Services
{
    public interface IMovies
    {
        public List<Movies> GetMovies();
        public void AddMovies(Movies movies);
        public List<MoviesDTO> GetMovieByID(int id);
        public List<MoviesDTO> GetActorByID(string id);
        public void CastMovie(Movies_Actors movies_Actors);
        public void UpdateMovie(Movies movies);
        public void DeleteMovie(int id);
        public void DeleteCastedMovie(int movieid, string userid);
    }
}
