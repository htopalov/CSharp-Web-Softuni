/*
 This server is only with educational purposes and it's not optimized.
 The point is to understand how http protocol and a simple server works.
 Another part of training is to get used to async programming,
 understand state management, MVC, IoC and data binding
*/

using System.Threading.Tasks;
using CustomHttpWebServer.App.Controllers;
using CustomHttpWebServer.Controllers;

namespace CustomHttpWebServer.App
{
    public class StartUp
    {
        static async  Task Main()
            => await new HttpServer(routes => routes
                    .MapGet<HomeController>("/", c=> c.Index())
                    .MapGet<HomeController>("/Google", c => c.ToSomeOtherLocation())
                    .MapGet<AnimalsController>("/Cats", c=> c.Cats())
                    .MapGet<AnimalsController>("/Dogs", c=> c.Dogs()))
                .Start();
    }
}
