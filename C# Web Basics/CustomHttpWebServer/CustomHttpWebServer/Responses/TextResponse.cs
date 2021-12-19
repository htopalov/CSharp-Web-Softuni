using System.Text;
using CustomHttpWebServer.Common;
using CustomHttpWebServer.Http;

namespace CustomHttpWebServer.Responses
{
    public class TextResponse : ContentResponse
    {
        public TextResponse(string text)
            :base(text, "text/plain; charset=UTF-8")
        {
        }
    }
}
