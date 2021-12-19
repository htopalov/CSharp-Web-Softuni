using System.Text;
using CustomHttpWebServer.Server.Common;
using CustomHttpWebServer.Server.Http;

namespace CustomHttpWebServer.Server.Responses
{
    public class TextResponse : HttpResponse
    {
        public TextResponse(string text)
            :this(text, "text/plain; charset=UTF-8")
        {
        }

        public TextResponse(string text,string contentType)
            :base(HttpStatusCode.OK)
        {
            Guard.AgainstNull(text);

            var contentLength = Encoding.UTF8.GetByteCount(text).ToString();

            this.Headers.Add("Content-Type", "text/plain; charset=UTF-8");
            this.Headers.Add("Content-Length", contentLength);

            this.Content = text;
        }
    }
}
