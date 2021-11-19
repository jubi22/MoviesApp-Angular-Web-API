using MoviesApi.Authentication;
using MoviesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Services
{
    public class UsersService:IUsers
    {
        private readonly MovieDBContext dbcontext;
        public UsersService(MovieDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public List<ApplicationUser> GetUsers()
        {
            return dbcontext.ApplicationUsers.ToList(); 
        }
        public void UpdateProfile(ApplicationUser user)
        {
            ApplicationUser u = dbcontext.ApplicationUsers.Where(t => t.Id == user.Id).FirstOrDefault();
            if (u != null)
            {
                u.UserName = user.UserName;
                u.NormalizedUserName =  u.UserName.ToUpper();
                u.DOB = user.DOB;
            }
            dbcontext.SaveChanges();
        }
        public void ChangePassword(ApplicationUser user)
        {
            ApplicationUser u = dbcontext.ApplicationUsers.Where(t => t.Id == user.Id).FirstOrDefault();
            if (u != null)
            {
                u.PasswordHash = user.PasswordHash;
            }
            dbcontext.SaveChanges();
        }
        public void DeleteProfile(string id)
        {
            var u = this.dbcontext.ApplicationUsers.Where(t => t.Id == id).FirstOrDefault();
            dbcontext.Remove(u);
            dbcontext.SaveChanges();
        }

        public ApplicationUser GetUserByID(string id)
        {
            ApplicationUser u = this.dbcontext.ApplicationUsers.Where(t => t.Id == id).FirstOrDefault();
            return u;
        }
    }
}
