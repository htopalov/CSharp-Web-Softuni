﻿using CustomHttpWebServer.Controllers;
using CustomHttpWebServer.Http;

namespace CustomHttpWebServer.App.Controllers
{
    public class CatsController : Controller
    {
        public CatsController(HttpRequest request)
            : base(request)
        {
        }

        public HttpResponse Create()
        {
            return View();
        }

        public HttpResponse Save()
        {
            var name = this.Request.Form["Name"];
            var age = this.Request.Form["Age"];
            
            return Text($"{name} - {age}");
        }
    }
}
