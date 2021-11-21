using MoviesApi.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Services
{
    public interface IUsers
    {
        public List<ApplicationUser> GetUsers();
        public void UpdateProfile(ApplicationUser u);
        public void DeleteProfile(string id);

        public ApplicationUser GetUserByID(string id);
    }
}
