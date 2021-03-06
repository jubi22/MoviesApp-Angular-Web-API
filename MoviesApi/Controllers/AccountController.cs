using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MoviesApi.Authentication;
using MoviesApi.DTO;
using MoviesApi.Models;
using MoviesApi.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager <ApplicationUser> signManager;
        private readonly ILogger<AccountController> logger;
        private IUsers users;


        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager <ApplicationUser> signManager, IUsers users, ILogger<AccountController> logger)
        { 
       
            this.userManager = userManager;
            this.signManager = signManager;
            this.logger= logger;
            this.users = users;
        }
        [HttpPost("/register")]
        public async Task<ActionResult> Register(RegisterUser register )
        {
            var Role = "User";
            var user = new ApplicationUser()
            {
                UserName = register.UserName,
                Email = register.Email,
                DOB = register.DOB,
                //Role = register.Role
            };
            try
            {
                logger.LogInformation("New user has been created by Admin");
                var result = await userManager.CreateAsync(user, register.Password);
                await userManager.AddToRoleAsync(user, Role);
                return Ok(result);
            }
            catch(Exception x)
            {
                logger.LogError("Registration failed due to some error");
                throw x;
            }
        }

        [HttpPut("/change-pwd")]
        public async Task<ActionResult> ChangePassword(ChangePassword user)
        {
            try
            {
                var u = await userManager.FindByIdAsync(user.Id);
                if (u != null)
                {
                    logger.LogInformation("Password of a user has been changed");
                    await userManager.ChangePasswordAsync(u, user.CurrentPassword, user.NewPassword);
                   
                }
                return Ok(u);
            }
            catch(Exception e)
            {
                logger.LogError("Failed");
                throw e;
            }
        }

        [HttpPost("/login")]
        public async Task<ActionResult> LoginUser(Login login)
        {
            var u = await userManager.FindByEmailAsync(login.Email);
            if(u!=null && await userManager.CheckPasswordAsync(u, login.Password))
            {
                logger.LogInformation("user/admin logged in");
                var role = await userManager.GetRolesAsync(u);
                IdentityOptions options = new IdentityOptions();

                var token_write = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", u.Id.ToString()),
                        new Claim(options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes("BJHBSXYUbiuasbxansBHBJSKHIUDh8qwyduiaydiuYUYoiu")),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenhandler = new JwtSecurityTokenHandler();
                var security_token = tokenhandler.CreateToken(token_write);
                var token = tokenhandler.WriteToken(security_token);
                var username = u.UserName;
                var id = u.Id;
                return Ok(new { token ,username,id});
            }
            else 
            {
                logger.LogError("Login failed!");
                return Ok("Failed");
            }
        }

        [HttpGet("/getusers")]
        public  ActionResult<List<UsersDTO>> DisplayUsers()
        {
            logger.LogInformation("List of Actors is displayed.");
            var u=this.users.GetUsers();
            return Mapper.Map<List<UsersDTO>>(u);

        }
        [HttpPut("/update")]
        public ActionResult UpdateProfile(ApplicationUser user)
        {
            logger.LogInformation("Changes made in user profile");
            this.users.UpdateProfile(user);
            return Ok(user);
        }
        [HttpDelete("/delete/{id}")]
        public ActionResult DeleteProfile(string id)
        {
            logger.LogInformation("User profile is deleted");
            this.users.DeleteProfile(id);
            return Ok(users);
        }
        [HttpGet("/userid/{id}")]
        public ActionResult GetUserID(string id)
        {
            this.users.GetUserByID(id);
            return Ok("retrived id");
        }
    }
}
