using CustomHttpWebServer.Server.Http;

namespace CustomHttpWebServer.Server.Routing
{
    public interface IRoutingTable
    {
        IRoutingTable Map(string url, HttpMethod method, HttpResponse response);

        IRoutingTable MapGet(string url, HttpResponse response);
    }
}
