using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebApi.Service.Abstract;
using WebApi.Service.Concrete;
using WebApi.Service.Mappers;

namespace WebApi.Service.Extensions.Service
{
    public static class AddServiceLayerServices
    {
        public static void AddService(this IServiceCollection services)
        {
            #region IOC

            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAppRoleService, AppRoleService>();
            services.AddScoped<IAppUserService, AppUserService>();

            #endregion

            #region AutoMapper

            services.AddAutoMapper(typeof(BookMapper));

            #endregion

            #region FluentValidation

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            #endregion
        }
    }
}
