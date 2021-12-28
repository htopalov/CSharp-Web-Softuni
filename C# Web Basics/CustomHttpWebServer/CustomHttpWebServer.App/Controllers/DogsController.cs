using CustomHttpWebServer.App.Models.Animals;
using CustomHttpWebServer.Controllers;
using CustomHttpWebServer.Http;

namespace CustomHttpWebServer.App.Controllers
{
    public class DogsController : Controller
    {
        public DogsController(HttpRequest request) 
            : base(request)
        {
        }

        [HttpGet]
        public HttpResponse Create()
        {
            return View();
        }

        [HttpPost]
        public HttpResponse Create(DogFormModel model)
        {
            return Text($"Dogo: {model.Name} - {model.Age} - {model.Breed}");
        }
    }
}
