using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly HttpClient _httpClient;

        public BookController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //[HttpGet]
        //public async Task<IActionResult> Home()
        //{
        //    HttpContext.Request.Headers.Add("Authorization",
        //        $"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiZmlyYXQxMiIsImp0aSI6ImRlYzJiNzBkLWIzMWItNDVlZS1iYjVlLWUyYWY1MWE2NDM1YyIsImV4cCI6MTY4MTAwNTc0OSwiaXNzIjoiRmlyYXRPcnRhYyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcyNzQifQ.sMrWRvZ4G_iYUhie_6YPIQdIjwvRL91kPKqNEuV7DmM");
        //    var result = await _httpClient.GetFromJsonAsync<List<BookDto>>("https://localhost:7274/api/books");
        //    return View();
        //}
    }
}
