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

                routingTable.MapGet(path, responseFunction);
                MapDefaultRoutes(routingTable,actionName,controllerName, responseFunction);
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
                var controllerInstance = CreateController(controllerType, request);

                if (controllerAction.ReturnType != httpResponseType)
                {
                    throw new InvalidOperationException(
                        $"Controller action '{path}' does not return http response object");
                }

                return (HttpResponse)controllerAction.Invoke(controllerInstance, Array.Empty<object>());
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

        private static void MapDefaultRoutes(IRoutingTable routingTable, 
            string actionName,
            string controllerName,
            Func<HttpRequest,HttpResponse> responseFunction)
        {
            const string defaultActionName = "Index";
            const string defaultControllerName = "Home";

            if (actionName == defaultActionName)
            {
                routingTable.MapGet($"/{controllerName}", responseFunction);
                if (controllerName == defaultControllerName)
                {
                    routingTable.MapGet("/", responseFunction);
                }
            }
        }
    }
}
