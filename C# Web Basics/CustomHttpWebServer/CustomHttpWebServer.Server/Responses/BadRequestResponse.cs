using CustomHttpWebServer.Server.Http;

namespace CustomHttpWebServer.Server.Responses
{
    public class BadRequestResponse : HttpResponse
    {
        public BadRequestResponse() 
            : base(HttpStatusCode.BadRequest)
        {
        }
    }
}
