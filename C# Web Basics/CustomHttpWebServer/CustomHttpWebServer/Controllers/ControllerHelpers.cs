using System;

namespace CustomHttpWebServer.Controllers
{
    public static class ControllerHelpers
    {
        public static string GetControllerName(this Type type)
        {
            return type.Name.Replace(nameof(Controller), string.Empty);
        }
    }
}
