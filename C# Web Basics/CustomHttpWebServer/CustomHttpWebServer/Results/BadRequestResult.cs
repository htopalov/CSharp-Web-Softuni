using CustomHttpWebServer.Http;

namespace CustomHttpWebServer.Results
{
    public class BadRequestResult : HttpResponse
    {
        public BadRequestResult() 
            : base(HttpStatusCode.BadRequest)
        {
        }
    }
}
