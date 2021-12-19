using System;
using System.Collections.Generic;
using CustomHttpWebServer.Server.Common;
using CustomHttpWebServer.Server.Http;
using CustomHttpWebServer.Server.Responses;

namespace CustomHttpWebServer.Server.Routing
{
    public class RoutingTable : IRoutingTable
    {
        private readonly Dictionary<HttpMethod, Dictionary<string, HttpResponse>> routes;

        public RoutingTable()
        {
            this.routes = new()
            {
                [HttpMethod.Get] = new(),
                [HttpMethod.Post] = new(),
                [HttpMethod.Put] = new(),
                [HttpMethod.Delete] = new()
            };
        }

        public IRoutingTable Map(HttpMethod method, string path, HttpResponse response)
        {
            Guard.AgainstNull(path, nameof(path));
            Guard.AgainstNull(response, nameof(response));

            this.routes[method][path] = response;

            return this;
        }

        public IRoutingTable MapGet(string path, HttpResponse response)
        {
            return Map(HttpMethod.Get, path, response);

        }

        public IRoutingTable MapPost(string path, HttpResponse response)
        {
            return Map(HttpMethod.Post, path, response);
        }

        public HttpResponse MatchRequest(HttpRequest request)
        {
            var requestMethod = request.Method;
            var requestPath = request.Path;

            if (!this.routes.ContainsKey(requestMethod)
                || !this.routes[requestMethod].ContainsKey(requestPath))
            {
                return new NotFoundResponse();
            }

            return this.routes[requestMethod][requestPath];
        }
    }
}
