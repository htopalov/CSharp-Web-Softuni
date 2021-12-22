using CustomHttpWebServer.App.Models.Animals;
using CustomHttpWebServer.Controllers;
using CustomHttpWebServer.Http;

namespace CustomHttpWebServer.App.Controllers
{
    public class AnimalsController : Controller
    {
        public AnimalsController(HttpRequest request)
            : base(request)
        {
        }

        public HttpResponse Cats()
        {
            const string nameKey = "Name";
            const string ageKey = "Age";

            var query = this.Request.Query;
            var catName = query.ContainsKey(nameKey)
                ? query[nameKey]
                : " the cats";

            var catAge = query.ContainsKey(ageKey)
                ? int.Parse(query[ageKey])
                : 0;

            var catViewModel = new CatViewModel
            {
                Name = catName,
                Age = catAge
            };

            return View(catViewModel);
        }

        public HttpResponse Dogs()
        {
            return View();
        }

        public HttpResponse Bunnies()
        {
            return View("Rabbits");
        }

        public HttpResponse Turtles()
        {
            return View("Animals/Wild/Turtles");
        }
    }
}
