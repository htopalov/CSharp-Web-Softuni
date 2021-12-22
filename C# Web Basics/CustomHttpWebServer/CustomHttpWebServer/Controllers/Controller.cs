using System.Runtime.CompilerServices;
using CustomHttpWebServer.Http;
using CustomHttpWebServer.Responses;

namespace CustomHttpWebServer.Controllers
{
    public abstract class Controller
    {
        protected Controller(HttpRequest request)
        {
            this.Request = request;
        }
        protected HttpRequest Request { get; private set; }

        protected HttpResponse Text(string text)
        {
            return new TextResponse(text);
        }

        protected HttpResponse Html(string html)
        {
            return new HtmlResponse(html);
        }

        protected HttpResponse Redirect(string location)
        {
            return new RedirectResponse(location);
        }

        protected HttpResponse View([CallerMemberName] string viewName = "")
        {
            return new ViewResponse(viewName, this.GetControllerName(), null);
        }

        protected HttpResponse View(string viewName, object model)
        {
            return new ViewResponse(viewName, this.GetControllerName(), model);
        }

        protected HttpResponse View(object model, [CallerMemberName] string viewName = "")
        {
            return new ViewResponse(viewName, this.GetControllerName(), model);
        }

        private string GetControllerName()
        {
            return this.GetType().Name.Replace(nameof(Controller), string.Empty);
        }
    }
}
