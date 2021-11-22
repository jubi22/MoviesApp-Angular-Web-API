using Microsoft.AspNetCore.Identity;
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
            this.SeedUsers(builder);
            this.SeedRoles(builder);
            this.SeedUserRoles(builder);
        }
        private void SeedUsers(ModelBuilder builder)
        {
            ApplicationUser users = new ApplicationUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                LockoutEnabled = false

            };
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            users.PasswordHash = passwordHasher.HashPassword(users, "Admin@123");
            //passwordHasher.HashPassword(users, "Admin@123");
            builder.Entity<ApplicationUser>().HasData(users);
        }
        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole() { Id = "2", Name = "User", NormalizedName = "USER" }
                );
        }

        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "1", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" }
                );
        }

    }
}
