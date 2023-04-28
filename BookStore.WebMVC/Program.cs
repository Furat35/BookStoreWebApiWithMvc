using BookStore.WebMVC.Extensions.WebMvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddWebService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseStatusCodePagesWithRedirects();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "AdminPanel",
        areaName: "Admin",
        pattern: "{area:exists}/{controller=Book}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Book}/{action=Index}/{id?}"
    );
});

app.Run();
