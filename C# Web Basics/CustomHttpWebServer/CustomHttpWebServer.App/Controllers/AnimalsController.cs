using CustomHttpWebServer.App.Models.Animals;
using CustomHttpWebServer.Controllers;
using CustomHttpWebServer.Http;

namespace CustomHttpWebServer.App.Controllers
{
    public class AnimalsController : Controller
    {
        public HttpResponse Cats()
        {
            const string nameKey = "Name";
            const string ageKey = "Age";

            var query = this.Request.Query;
            var catName = query.Contains(nameKey)
                ? query[nameKey]
                : " the cats";

            var catAge = query.Contains(ageKey)
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
            return View(new DogViewModel
            {
                Name = "Balkan",
                Age = 12,
                Breed = "Boxer"
            });
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
