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
            var query = this.Request.Query;
            var catName = query.ContainsKey(nameKey)
                ? query[nameKey]
                : " the cats";

            var result = $"<h1>Hello from {catName} on the server!</h1>";

            return Html(result);
        }

        public HttpResponse Dogs()
        {
            return Html("<h1>Hello from dogs on the server!</h1>");
        }
    }
}
