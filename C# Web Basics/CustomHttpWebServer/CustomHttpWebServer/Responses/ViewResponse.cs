﻿using System.IO;
using System.Linq;
using CustomHttpWebServer.Http;

namespace CustomHttpWebServer.Responses
{
    public class ViewResponse : HttpResponse
    {
        private const char PathSeparator = '/';

        public ViewResponse(string viewName, string controllerName, object model) 
            : base(HttpStatusCode.OK)
        {
            this.GetHtml(viewName, controllerName, model);
        }

        private void GetHtml(string viewName, string controllerName, object model)
        {
            if (!viewName.Contains(PathSeparator))
            {
                viewName = controllerName + PathSeparator + viewName;
            }

            var viewPath = Path.GetFullPath("./Views/" + viewName.TrimStart(PathSeparator) + ".cshtml");

            if (!File.Exists(viewPath))
            {
                this.PrepareMissingViewError(viewPath);
                return;
            }

            var viewContent = File.ReadAllText(viewPath);

            if (model != null)
            {
                viewContent = PopulateModel(viewContent, model);
            }

            this.PrepareContent(viewContent,HttpContentType.Html);
        }

        private void PrepareMissingViewError(string viewPath)
        {
            this.StatusCode = HttpStatusCode.NotFound;
            var errorMessage = $"View '{viewPath}' was not found";
            this.PrepareContent(errorMessage, HttpContentType.PlainText);
        }

        private string PopulateModel(string viewContent, object model)
        {
            var data = model
                .GetType()
                .GetProperties()
                .Select(prop => new
                {
                    Name = prop.Name,
                    Value = prop.GetValue(model)
                });

            foreach (var entry in data)
            {
                viewContent = viewContent.Replace($"{{{{{entry.Name}}}}}", entry.Value.ToString());
            }

            return viewContent;
        }
    }
}
