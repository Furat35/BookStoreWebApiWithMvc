using BookStore.WebApi.Extensions.WebApi;
using NLog.Web;
using WebApi.DataAccess.Extensions.DataLayerExtensions;
using WebApi.Service.Extensions.Service;

var logger = NLog.Web.NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "/nlog.config").GetCurrentClassLogger();

try
{
    logger.Debug("init main");
    var builder = WebApplication.CreateBuilder(args);

    #region NLog
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    #endregion

    builder.Services.AddWebApi();
    builder.Services.AddDataAccess(builder.Configuration);
    builder.Services.AddService();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    if (!app.Environment.IsDevelopment())
    {
        app.UseHsts();
    }

    app.UseWebApi(builder.Services);
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}