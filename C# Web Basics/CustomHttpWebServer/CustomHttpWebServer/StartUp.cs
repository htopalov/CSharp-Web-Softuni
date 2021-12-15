/*
 This server is only with educational purposes and it's not optimized.
 The point is to understand how http protocol and a simple server works.
 Another part of training is to get used to async programming,
 understand state management, MVC, IoC and data binding
*/

using System.Threading.Tasks;
using CustomHttpWebServer.Server;

namespace CustomHttpWebServer
{
    public class StartUp
    {
        static async  Task Main()
        {
            var server = new HttpServer("127.0.0.1", 9090);

            await server.Start();
        }
    }
}
