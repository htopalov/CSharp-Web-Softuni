using System.Linq;
using CustomHttpWebServer.App.Data;
using CustomHttpWebServer.Controllers;
using CustomHttpWebServer.Http;

namespace CustomHttpWebServer.App.Controllers
{
    public class CatsController : Controller
    {
        private readonly IData data;

        public CatsController(IData data)
        {
            this.data = data;
        }

        public HttpResponse All()
        {
            var cats = this.data.Cats.ToList();
            return View(cats);
        }

        [HttpGet]
        public HttpResponse Create()
        {
            return View();
        }

        [HttpPost]
        public HttpResponse Save(string name, int age)
        {
            return Text($"{name} - {age}");
        }
    }
}
