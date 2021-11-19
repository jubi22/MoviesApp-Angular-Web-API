using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Authentication;

namespace MoviesApi.Models
{
    public class MovieDBContext: IdentityDbContext
    {
        public MovieDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<Movies_Actors> Movies_Actors { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
