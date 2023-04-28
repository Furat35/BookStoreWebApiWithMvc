using AuthorStore.WebMVC.ApiServices.Abstract;
using BookStore.ApiServices.Concrete;
using BookStore.WebMVC.ApiServices.Abstract;
using BookStore.WebMVC.ApiServices.Concrete;
using BookStore.WebMVC.Delegeates;
using GenreStore.WebMVC.ApiServices.Abstract;
using Microsoft.AspNetCore.Authentication.Cookies;
using PublisherStore.WebMVC.ApiServices.Abstract;
using System.Reflection;
using UserStore.WebMVC.ApiServices.Abstract;

namespace BookStore.WebMVC.Extensions.WebMvc
{
    public static class AddWebMvcServices
    {
        public static void AddWebService(this IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddHttpClient("HttpClientConfig", options =>
            {
                options.BaseAddress = new Uri("https://localhost:7274/");
            })
               .AddHttpMessageHandler<HttpRequestHandler>();

            #region IOC Container

            services.AddTransient<IHttpClientService, HttpClientService>();
            services.AddTransient<HttpRequestHandler>();
            services.AddHttpContextAccessor();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProfileService, ProfileService>();

            #endregion

            #region Automapper

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #endregion

            #region Cookie Authentication

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(_ =>
                {
                    _.LoginPath = "/Admin/Authentication/Login";
                    _.Cookie.Name = "AspNetCore.Auth";
                    _.Cookie.SameSite = SameSiteMode.Strict;
                    _.ExpireTimeSpan = TimeSpan.FromMinutes(50);
                    _.SlidingExpiration = true;
                });

            #endregion
        }
    }
}
