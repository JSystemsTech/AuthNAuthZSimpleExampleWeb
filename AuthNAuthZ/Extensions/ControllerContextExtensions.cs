using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace AuthNAuthZ.Extensions
{
    public static class ControllerContextExtensions
    {
        private static RouteValueDictionary AddUpdate(this RouteValueDictionary route, string key, object value)
        {
            route.Remove(key);
            route.Add(key, value);
            return route;
        }
        private static RouteValueDictionary BuildRouteValueDictionary(string actionName) => new RouteValueDictionary(new { action = actionName });
        private static RouteValueDictionary BuildRouteValueDictionary(string actionName, object routeValues)
            => new RouteValueDictionary(routeValues).AddUpdate("action", actionName);
        private static RouteValueDictionary BuildRouteValueDictionary(string actionName, RouteValueDictionary routeValues)
            => routeValues.AddUpdate("action", actionName);
        private static RouteValueDictionary BuildRouteValueDictionary(string actionName, string controllerName)
            => new RouteValueDictionary(new { action = actionName, controller = controllerName });
        private static RouteValueDictionary BuildRouteValueDictionary(string actionName, string controllerName, object routeValues)
            => new RouteValueDictionary(routeValues).AddUpdate("action", actionName).AddUpdate("controller", controllerName);


        public static string ActionName(this ControllerContext context) => context.RouteData.Values["action"] is string action ? action : null;
        public static MethodInfo MethodInfo(this ControllerContext context) => context.Controller.GetType().GetMethods().FirstOrDefault(x => x.DeclaringType == context.Controller.GetType()
                                && x.Name == context.ActionName());
        public static bool IsPartialViewRequest(this ControllerContext context) => context.MethodInfo().ReturnType == typeof(PartialViewResult);
        public static bool IsJsonRequest(this ControllerContext context) => context.MethodInfo().ReturnType == typeof(JsonResult);

        public static void RedirectToAction(this ExceptionContext context, string actionName)
            => context.Result = new RedirectToRouteResult(BuildRouteValueDictionary(actionName));
        public static void RedirectToAction(this ExceptionContext context, string actionName, object routeValues)
            => context.Result = new RedirectToRouteResult(BuildRouteValueDictionary(actionName, routeValues));
        public static void RedirectToAction(this ExceptionContext context, string actionName, RouteValueDictionary routeValues)
            => context.Result = new RedirectToRouteResult(BuildRouteValueDictionary(actionName, routeValues));
        public static void RedirectToAction(this ExceptionContext context, string actionName, string controllerName)
            => context.Result = new RedirectToRouteResult(BuildRouteValueDictionary(actionName, controllerName));
        public static void RedirectToAction(this ExceptionContext context, string actionName, string controllerName, object routeValues)
            => context.Result = new RedirectToRouteResult(BuildRouteValueDictionary(actionName, controllerName, routeValues));

        public static void RedirectToAction(this AuthorizationContext context, string actionName)
            => context.Result = new RedirectToRouteResult(BuildRouteValueDictionary(actionName));
        public static void RedirectToAction(this AuthorizationContext context, string actionName, object routeValues)
            => context.Result = new RedirectToRouteResult(BuildRouteValueDictionary(actionName, routeValues));
        public static void RedirectToAction(this AuthorizationContext context, string actionName, RouteValueDictionary routeValues)
            => context.Result = new RedirectToRouteResult(BuildRouteValueDictionary(actionName, routeValues));
        public static void RedirectToAction(this AuthorizationContext context, string actionName, string controllerName)
            => context.Result = new RedirectToRouteResult(BuildRouteValueDictionary(actionName, controllerName));
        public static void RedirectToAction(this AuthorizationContext context, string actionName, string controllerName, object routeValues)
            => context.Result = new RedirectToRouteResult(BuildRouteValueDictionary(actionName, controllerName, routeValues));

        public static void RedirectToAction(this AuthenticationContext context, string actionName)
            => context.Result = new RedirectToRouteResult(BuildRouteValueDictionary(actionName));
        public static void RedirectToAction(this AuthenticationContext context, string actionName, object routeValues)
            => context.Result = new RedirectToRouteResult(BuildRouteValueDictionary(actionName, routeValues));
        public static void RedirectToAction(this AuthenticationContext context, string actionName, RouteValueDictionary routeValues)
            => context.Result = new RedirectToRouteResult(BuildRouteValueDictionary(actionName, routeValues));
        public static void RedirectToAction(this AuthenticationContext context, string actionName, string controllerName)
            => context.Result = new RedirectToRouteResult(BuildRouteValueDictionary(actionName, controllerName));
        public static void RedirectToAction(this AuthenticationContext context, string actionName, string controllerName, object routeValues)
            => context.Result = new RedirectToRouteResult(BuildRouteValueDictionary(actionName, controllerName, routeValues));
    }
}
