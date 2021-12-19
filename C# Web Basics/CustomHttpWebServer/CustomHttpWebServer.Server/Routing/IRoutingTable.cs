using CustomHttpWebServer.Server.Http;

namespace CustomHttpWebServer.Server.Routing
{
    public interface IRoutingTable
    {
        IRoutingTable Map(HttpMethod method, string path, HttpResponse response);

        IRoutingTable MapGet(string path, HttpResponse response);
    }
}
