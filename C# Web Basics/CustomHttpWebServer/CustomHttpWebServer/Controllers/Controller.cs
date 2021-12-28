using System.Runtime.CompilerServices;
using CustomHttpWebServer.Http;
using CustomHttpWebServer.Identity;
using CustomHttpWebServer.Results;

namespace CustomHttpWebServer.Controllers
{
    public abstract class Controller
    {
        public const string UserSessionKey = "AuthenticatedUserId";

        private UserIdentity userIdentity;

        protected Controller()
        {
            this.Response = new HttpResponse(HttpStatusCode.OK);
        }
        protected HttpRequest Request { get; init; }

        protected HttpResponse Response { get; private init; }

        protected UserIdentity User
        {
            get
            {
                if (this.userIdentity == null)
                {
                    this.userIdentity = this.Request.Session.ContainsKey(UserSessionKey)
                        ? new UserIdentity { Id = this.Request.Session[UserSessionKey] }
                        : new UserIdentity();
                }

                return this.userIdentity;
            }
        }

        protected void SignIn(string userId)
        {
            this.Request.Session[UserSessionKey] = userId;
            this.userIdentity = new UserIdentity() {Id = userId};
        }

        protected void SignOut()
        {
            this.Request.Session.Remove(UserSessionKey);
            this.User = this.Request.Session.ContainsKey(UserSessionKey)
                ? new UserIdentity {Id = this.Request.Session[UserSessionKey]}
                : new UserIdentity();
        }

        protected ActionResult Text(string text)
        {
            return new TextResult(this.Response, text);
        }

        protected ActionResult Html(string html)
        {
            return new HtmlResult(this.Response, html);
        }

        protected ActionResult Redirect(string location)
        {
            return new RedirectResult(this.Response, location);
        }

        protected ActionResult View([CallerMemberName] string viewName = "")
        {
            return new ViewResult(this.Response, viewName, this.GetType().GetControllerName(), null);
        }

        protected ActionResult View(string viewName, object model)
        {
            return new ViewResult(this.Response, viewName, this.GetType().GetControllerName(), model);
        }

        protected ActionResult View(object model, [CallerMemberName] string viewName = "")
        {
            return new ViewResult(this.Response, viewName, this.GetType().GetControllerName(), model);
        }
    }
}
