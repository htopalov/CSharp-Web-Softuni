﻿/*
 This server is only with educational purposes and it's not optimized.
 The point is to understand how http protocol and a simple server works.
 Another part of training is to get used to async programming,
 understand state management, MVC, IoC and data binding
*/

using System.Threading.Tasks;
using CustomHttpWebServer.App.Controllers;
using CustomHttpWebServer.Controllers;

namespace CustomHttpWebServer.App
{
    public class StartUp
    {
        static async  Task Main()
            => await new HttpServer(routes => routes
                    .MapGet<HomeController>("/", c=> c.Index())
                    .MapGet<HomeController>("/Google", c => c.ToSomeOtherLocation())
                    .MapGet<HomeController>("/Error", c => c.Error())
                    .MapGet<AnimalsController>("/Cats", c=> c.Cats())
                    .MapGet<AnimalsController>("/Dogs", c=> c.Dogs())
                    .MapGet<AnimalsController>("/Bunnies", c=> c.Bunnies())
                    .MapGet<AnimalsController>("/Turtles", c=> c.Turtles())
                    .MapGet<AccountController>("/Cookies", c=> c.CookiesCheck())
                    .MapGet<AccountController>("/Session", c=> c.SessionCheck())
                    .MapGet<AccountController>("/Login", c=> c.Login())
                    .MapGet<AccountController>("/Logout", c=> c.Logout())
                    .MapGet<AccountController>("/Authentication", c=> c.AuthenticationCheck())
                    .MapGet<CatsController>("/Cats/Create", c=> c.Create())
                    .MapPost<CatsController>("/Cats/Save", c=> c.Save()))
                .Start();
    }
}
