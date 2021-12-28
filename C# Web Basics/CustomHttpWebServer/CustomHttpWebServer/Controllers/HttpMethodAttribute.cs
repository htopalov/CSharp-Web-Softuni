using System;
using CustomHttpWebServer.Http;

namespace CustomHttpWebServer.Controllers
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class HttpMethodAttribute : Attribute
    {
        public HttpMethodAttribute(HttpMethod httpMethod)
        {
            this.HttpMethod = httpMethod;
        }

        public HttpMethod HttpMethod { get; }
    }
}
