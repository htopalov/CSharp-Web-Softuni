﻿using System.Text;
using CustomHttpWebServer.Common;
using CustomHttpWebServer.Http;

namespace CustomHttpWebServer.Responses
{
    public class ContentResponse : HttpResponse
    {
        public ContentResponse(string content, string contentType)
            : base(HttpStatusCode.OK)
        {
            this.PrepareContent(content,contentType);
        }
    }
}
