﻿using System;
using CustomHttpWebServer.Controllers;
using CustomHttpWebServer.Http;

namespace CustomHttpWebServer.App.Controllers
{
    public class AccountController : Controller
    {
        public HttpResponse Login()
        {
            //var user = this.db.Users.Find(username, password);
            //if(user != null)
            //{
            //  this.SignIn(user.Id);
            //  return Text("User is logged in");
            //}
            //return Text("Invalid credentials");

            var someUserId = "MyUserId";

            this.SignIn(someUserId);
            return Text("User authenticated");

        }

        public HttpResponse Logout()
        {
            this.SignOut();
            return Text("User signed out");
        }

        public HttpResponse AuthenticationCheck()
        {
            if (this.User.IsAuthenticated)
            {
                return Text($"Authenticated user: {this.User.Id}");
            }

            return Text("User is not authenticated");
        }

        [Authorize]
        public HttpResponse AuthorizationCheck()
        {
            return Text($"Current user: {this.User.Id}");
        }

        public HttpResponse CookiesCheck()
        {
            const string cookieName = "My-Cookie";

            if (this.Request.Cookies.Contains(cookieName))
            {
                var cookie = this.Response.Cookies[cookieName];

                return Text($"Cookies already exist - {cookie}");
            }

            this.Response.Cookies.Add(cookieName, "My-Value");
            this.Response.Cookies.Add("My-Second-Cookie", "My-Second-Value");

            return Text("Cookies set");
        }

        public HttpResponse SessionCheck()
        {
            const string currentDateKey = "CurrentDate";

            if (this.Request.Session.ContainsKey(currentDateKey))
            {
                var currentDate = this.Request.Session[currentDateKey];

                return Text($"Stored date: {currentDate}");
            }

            this.Request.Session[currentDateKey] = DateTime.UtcNow.ToString();

            return Text("Date stored");
        }
    }
}