/*
 This server is only with educational purposes and it's not optimized.
 The point is to understand how http protocol and a simple server works.
 Another part of training is to get used to async programming,
 understand state management, MVC, IoC and data binding
*/

using System.Threading.Tasks;
using CustomHttpWebServer.Controllers;
using CustomHttpWebServer.Server;
using CustomHttpWebServer.Server.Http;
using CustomHttpWebServer.Server.Responses;

namespace CustomHttpWebServer
{
    public class StartUp
    {
        static async  Task Main()
            => await new HttpServer(routes => routes
                    .MapGet("/", new TextResponse("Hello from server!"))
                    .MapGet("/Cats", request =>
                    {
                        const string nameKey = "Name";
                        var query = request.Query;
                        var catName = query.ContainsKey(nameKey)
                            ? query[nameKey]
                            : " the cats";

                        var result = $"<h1>Hello from {catName} on the server!</h1>";

                        return new HtmlResponse(result);
                    })
                    .MapGet("/Dogs", new HtmlResponse("<h1>Hello from dogs on the server!</h1>")))
                .Start();
    }
}
