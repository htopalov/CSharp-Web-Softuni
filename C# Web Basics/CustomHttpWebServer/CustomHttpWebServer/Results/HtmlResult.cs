using CustomHttpWebServer.Http;

namespace CustomHttpWebServer.Results
{
    public class HtmlResult : ContentResult
    {
        public HtmlResult(HttpResponse response, string html) 
            : base(response, html, HttpContentType.Html)
        {
        }
    }
}
