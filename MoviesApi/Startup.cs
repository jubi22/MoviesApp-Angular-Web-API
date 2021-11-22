using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MoviesApi.Authentication;
using MoviesApi.Models;
using MoviesApi.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<MovieDBContext>
                (t => t.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IMovies, MoviesService>();
            services.AddScoped<IUsers, UsersService>();

            services.AddIdentity<ApplicationUser, IdentityRole>(t=> { t.User.AllowedUserNameCharacters = string.Empty; }).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores
                <MovieDBContext>().AddDefaultTokenProviders();
            services.AddAuthentication(t =>
            {
                t.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                t.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                t.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(t =>
            {
                t.SaveToken = true;
                t.RequireHttpsMetadata = false;
                t.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    ClockSkew=TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(Configuration["JWT:key"]))
                };
            });
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory loggerFactory)
        {
            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"{path}\\Logs\\log.txt");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(t => t.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
