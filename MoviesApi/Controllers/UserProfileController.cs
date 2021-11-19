using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController:ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        public UserProfileController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        [HttpGet("/userdetail")]

        [Authorize]
        public async Task<Object> GetUserDetail()
        {
            string userid = User.Claims.First(t => t.Type == "UserID").Value;
            var user = await userManager.FindByIdAsync(userid);
            return new
            {
                user.UserName,
                user.Email
            };
        }
        //[HttpGet("/admin")]
        //[Authorize(Roles ="Admin")]
        //public string GetAdmin()
        //{
        //    return "Admin authorizattion";
        //}
        //[HttpGet("/user")]
        //[Authorize(Roles = "User")]
        //public string GetUser()
        //{
        //    return "User authorizattion";
        //}
    }
}
