using CustomHttpWebServer.Http;

namespace CustomHttpWebServer.Controllers
{
    public class HttpPostAttribute : HttpMethodAttribute
    {
        public HttpPostAttribute() 
            : base(HttpMethod.Post)
        {
        }
    }
}
