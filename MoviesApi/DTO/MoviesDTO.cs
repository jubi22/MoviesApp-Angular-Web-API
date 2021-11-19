using MoviesApi.Authentication;
using MoviesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.DTO
{
    public class MoviesDTO
    {
        public ApplicationUser ApplicationUser { get; set; }
        public Movies Movies { get; set; }
        public Movies_Actors Movies_Actors { get; set; }
    }
}
