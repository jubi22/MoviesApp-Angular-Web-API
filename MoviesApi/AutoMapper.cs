using MoviesApi.Authentication;
using MoviesApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace MoviesApi
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<ApplicationUser, UsersDTO>();
        }
    }
}
