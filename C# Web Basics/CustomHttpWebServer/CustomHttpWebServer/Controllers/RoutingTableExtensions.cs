using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CustomHttpWebServer.Http;
using CustomHttpWebServer.Routing;

namespace CustomHttpWebServer.Controllers
{
    public static class RoutingTableExtensions
    {
        private static Type httpResponseType = typeof(HttpResponse);
        private static Type stringType = typeof(string);

        public static IRoutingTable MapGet<TController>(
            this IRoutingTable routingTable,
            string path,
            Func<TController, HttpResponse> controllerFunction)
            where TController : Controller
        {
            return routingTable.MapGet(path, request => controllerFunction(CreateController<TController>(request)));
        }


        public static IRoutingTable MapPost<TController>(
            this IRoutingTable routingTable,
            string path,
            Func<TController, HttpResponse> controllerFunction)
            where TController : Controller
        {
            return routingTable.MapPost(path, request => controllerFunction(CreateController<TController>(request)));
        }

        public static IRoutingTable MapControllers(
            this IRoutingTable routingTable)
        {
            var controllerActions = GetControllerActions();

            foreach (var controllerAction in controllerActions)
            {
                var controllerType = controllerAction.DeclaringType;
                var controllerName = controllerType.GetControllerName();
                var actionName = controllerAction.Name;

                var path = $"/{controllerName}/{actionName}";

                var responseFunction = GetResponseFunction(controllerType, controllerAction, path);

                var httpMethod = HttpMethod.Get;

                var httpMethodAttribute = controllerAction.GetCustomAttribute<HttpMethodAttribute>();

                if (httpMethodAttribute != null)
                {
                    httpMethod = httpMethodAttribute.HttpMethod;
                }

                routingTable.Map(httpMethod, path, responseFunction);
                MapDefaultRoutes(routingTable,httpMethod, actionName,controllerName, responseFunction);
            }

            return routingTable;
        }

        private static IEnumerable<MethodInfo> GetControllerActions()
        {
            return Assembly
                   .GetEntryAssembly()
                   .GetExportedTypes()
                   .Where(t => t.IsAssignableTo(typeof(Controller))
                            && t.Name.EndsWith(nameof(Controller)))
                   .SelectMany(t => t.GetMethods(BindingFlags.Instance | BindingFlags.Public))
                   .Where(m=> m.ReturnType.IsAssignableTo(httpResponseType))
                   .ToList();
        }

        private static Func<HttpRequest, HttpResponse> GetResponseFunction(
            Type controllerType,
            MethodInfo controllerAction,
            string path)
        {
            return request =>
            {
                if (!UserIsAuthorized(controllerAction, request.Session))
                {
                    return new HttpResponse(HttpStatusCode.Unauthorized);
                }

                var controllerInstance = CreateController(controllerType, request);

                var parameterValues = GetParameterValues(controllerAction,request);

                return (HttpResponse)controllerAction.Invoke(controllerInstance, parameterValues);
            };
        }

        private static Controller CreateController(Type controller, HttpRequest request)
        {
            return (Controller)Activator.CreateInstance(controller, new[] { request });
        }

        private static TController CreateController<TController>(HttpRequest request)
            where TController : Controller 
        {
            return (TController)CreateController(typeof(TController), request);
        }

        private static void MapDefaultRoutes(
            IRoutingTable routingTable,
            HttpMethod httpMethod,
            string actionName,
            string controllerName,
            Func<HttpRequest,HttpResponse> responseFunction)
        {
            const string defaultActionName = "Index";
            const string defaultControllerName = "Home";

            if (actionName == defaultActionName)
            {
                routingTable.Map(httpMethod, $"/{controllerName}", responseFunction);
                if (controllerName == defaultControllerName)
                {
                    routingTable.Map(httpMethod, "/", responseFunction);
                }
            }
        }

        private static bool UserIsAuthorized(MethodInfo controllerAction, HttpSession session)
        {
            var authorizationRequired = controllerAction
                                            .DeclaringType
                                            .GetCustomAttribute<AuthorizeAttribute>()
                                            ?? controllerAction
                                            .GetCustomAttribute<AuthorizeAttribute>();

            if (authorizationRequired != null)
            {
                var userIsAuthorized = session.ContainsKey(Controller.UserSessionKey)
                                       && session[Controller.UserSessionKey] != null;

                if (!userIsAuthorized)
                {
                    return false;
                }
            }

            return true;
        }

        private static object[] GetParameterValues(
            MethodInfo controllerAction,
            HttpRequest request)
        {
            var actionParameters = controllerAction
                .GetParameters()
                .Select(p => new
                {
                    p.Name,
                    Type = p.ParameterType
                })
                .ToArray();

            var parameterValues = new object[actionParameters.Length];

            for (int i = 0; i < actionParameters.Length; i++)
            {
                var parameter = actionParameters[i];

                var parameterName = parameter.Name;

                var parameterType = parameter.Type;

                if (parameterType.IsPrimitive || parameterType == stringType)
                {
                    var parameterValue = request.GetValue(parameterName);
                    parameterValues[i] = Convert.ChangeType(parameterValue, parameterType);
                }
                else
                {
                    var parameterValue = Activator.CreateInstance(parameterType);

                    var parameterProperties = parameterType.GetProperties();

                    foreach (var property in parameterProperties)
                    {
                        var propertyValue = property.GetValue(property.Name);

                        property.SetValue(parameterValue, propertyValue);
                    }

                    parameterValues[i] = parameterValue;
                }

       
            }

            return parameterValues;
        }

        private static string GetValue(this HttpRequest request, string name)
        {
            return request.Query.GetValueOrDefault(name) ??
                    request.Form.GetValueOrDefault(name);
        }
    }
}
