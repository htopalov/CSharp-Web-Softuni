using CustomHttpWebServer.Http;

namespace CustomHttpWebServer.Results
{
    public class NotFoundResult : ActionResult
    {
        public NotFoundResult(HttpResponse response) 
            : base(response)
        {
            this.StatusCode = HttpStatusCode.NotFound;
        }
    }
}
