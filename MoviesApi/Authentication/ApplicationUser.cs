using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Authentication
{
    public class ApplicationUser:IdentityUser
    {
        public DateTime? DOB { get; set; }


    }
}
