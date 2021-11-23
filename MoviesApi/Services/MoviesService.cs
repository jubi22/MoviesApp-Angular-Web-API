using MoviesApi.Authentication;
using MoviesApi.DTO;
using MoviesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Services
{
    
    public class MoviesService:IMovies
    {
        private  MovieDBContext dBContext;
        public MoviesService(MovieDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public void DeleteMovie(int id)
        {
            var m = this.dBContext.Movies.Where(t => t.MovieID == id).FirstOrDefault();
            dBContext.Remove(m);
            dBContext.SaveChanges();
        }
        public void CastMovie(Movies_Actors movies_Actors)
        {
            this.dBContext.Movies_Actors.Add(movies_Actors);
            dBContext.SaveChanges();
        }

        public List<Movies> GetMovies()
        {
            return dBContext.Movies.ToList();
        }

        public void AddMovies(Movies movies)
        {
            this.dBContext.Add(movies);
            dBContext.SaveChanges();
        }

        public List<MoviesDTO> GetMovieByID(int id)
        {
            List<ApplicationUser> users = this.dBContext.ApplicationUsers.ToList();
            List<Movies> movies = this.dBContext.Movies.ToList();
            List<Movies_Actors> movies_Actors = this.dBContext.Movies_Actors.ToList();
            var temp = from u in users
                       join mo in movies_Actors on
                       u.Id equals mo.UserID into table1
                       from mo in table1.ToList()
                       join m in movies on
                       mo.MovieID equals m.MovieID into table2
                       where mo.MovieID == id
                       from m in table2.ToList() 
                       select new MoviesDTO
                       {
                           ApplicationUser = u
                       };
            return temp.ToList();
        }
        public List<MoviesDTO> GetActorByID(string id)
        {
            List<ApplicationUser> users = this.dBContext.ApplicationUsers.ToList();
            List<Movies> movies = this.dBContext.Movies.ToList();
            List<Movies_Actors> movies_Actors = this.dBContext.Movies_Actors.ToList();
            var temp = from u in users
                       join mo in movies_Actors on
                       u.Id equals mo.UserID into table1
                       from mo in table1.ToList()
                       join m in movies on
                       mo.MovieID equals m.MovieID into table2
                       where u.Id== id
                       from m in table2.ToList()
                       select new MoviesDTO
                       {
                           Movies = m
                       };
            return temp.ToList();
        }
        public void UpdateMovie(Movies movies)
        {
            Movies u = this.dBContext.Movies.Where(t => t.MovieID == movies.MovieID).FirstOrDefault();
            if (u != null)
            {
                u.MovieName = movies.MovieName;
                u.Description = movies.Description;
            }
            dBContext.SaveChanges();
        }
        public void DeleteCastedMovie(int id)
        {
            var m = this.dBContext.Movies_Actors.Where(t => t.MovieID == id).FirstOrDefault();
            dBContext.Remove(m);
            dBContext.SaveChanges();
        }
    }
}
