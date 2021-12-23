using System;
using CustomHttpWebServer.Controllers;
using CustomHttpWebServer.Http;

namespace CustomHttpWebServer.App.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(HttpRequest request) 
            : base(request)
        {
        }

        public HttpResponse Index()
        {
            return Text("Hello from server!");
        }

        public HttpResponse ToSomeOtherLocation()
        {
            return Redirect("https://google.bg");
        }

        public HttpResponse Error()
        {
            throw new InvalidOperationException("Invalid action!");
        }
    }
}
