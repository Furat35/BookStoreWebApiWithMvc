using BookStore.WebApi.Utilities.NLog;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApi.Core.Exceptions;
using WebApi.Core.Models;

namespace BookStore.WebApi.Extensions.WebApi
{
    public static class AddWebApiServices
    {
        public static void AddWebApi(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.Configure<ApiBehaviorOptions>(_ =>
            {
                _.SuppressModelStateInvalidFilter = true;
            });

            #region IOC
            services.AddScoped<ILoggerService, LoggerService>();
            #endregion

            #region Cors

            services.AddCors(builder =>
            {
                builder.DefaultPolicyName = "CorsPolicyAllHosts";
                builder.AddDefaultPolicy(_ =>
                {
                    _.AllowAnyHeader();
                    _.AllowAnyOrigin();
                    _.AllowAnyMethod();
                });
            });

            #endregion

            #region Caching

            services.AddMemoryCache();

            #endregion
        }

        public static void UseWebApi(this WebApplication app, IServiceCollection services)
        {
            #region Error Handling

            var logger = services.BuildServiceProvider().GetService<ILoggerService>();
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            BadRequestException => StatusCodes.Status400BadRequest,
                            UnauthorizedException => StatusCodes.Status401Unauthorized,
                            UnprocessableEntityException => StatusCodes.Status422UnprocessableEntity,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        }));
                    }
                });
            });

            #endregion

            #region Cors

            app.UseCors("CorsPolicyAllHosts");

            #endregion

        }
    }
}
