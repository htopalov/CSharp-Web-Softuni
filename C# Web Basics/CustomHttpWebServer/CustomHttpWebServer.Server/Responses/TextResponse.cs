using System.Text;
using CustomHttpWebServer.Server.Common;
using CustomHttpWebServer.Server.Http;

namespace CustomHttpWebServer.Server.Responses
{
    public class TextResponse : ContentResponse
    {
        public TextResponse(string text)
            :base(text, "text/plain; charset=UTF-8")
        {
        }
    }
}
