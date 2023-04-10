using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.DataAccess.Abstract;
using WebApi.DataAccess.Concrete;
using WebApi.DataAccess.EfDataAccess.Context;
using WebApi.DataAccess.UnitOfWorks;
using WebApi.Entity.Entities;

namespace WebApi.DataAccess.Extensions.DataLayerExtensions
{
    public static class AddDataLayerServices
    {
        public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddDbContext<EfContext>(_ => _.UseSqlServer(configuration.GetConnectionString("EfConnectionString")));

            #region Identity

            services.AddIdentity<AppUser, AppRole>(_ =>
                {
                    _.Password.RequireDigit = false;
                    _.Password.RequireDigit = false;
                    _.Password.RequireNonAlphanumeric = false;
                    _.Password.RequireUppercase = false;
                    _.Password.RequiredLength = 2;
                    _.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<EfContext>()
                .AddDefaultTokenProviders();

            #endregion

            #region Jwt Auth Configuration

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });

            #endregion
        }
    }
}
