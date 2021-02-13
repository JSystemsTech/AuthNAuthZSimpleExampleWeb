using AuthNAuthZ.Extensions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace AuthNAuthZ.Attributes
{
    public abstract class RedirectActionFilterAttribute : ActionFilterAttribute
    {
        protected ControllerContext Context { get; set; }
        protected HttpContextBase HttpContext => Context.HttpContext;
        protected HttpRequestBase Request => HttpContext.Request;
        protected HttpResponseBase Response => HttpContext.Response;
        private string Action { get; set; }
        private string PartialAction { get; set; }
        private string Controller { get; set; }
        private string PartialController { get; set; }
        public RedirectActionFilterAttribute(string action, string partialAction, string controller, string partialController)
        {
            Action = action;
            PartialAction = partialAction;
            Controller = controller;
            PartialController = partialController;
        }
        protected void OnFilterExecution(ControllerContext context)
        {
            Context = context;
        }
        protected void Redirect() => Redirect(new { });
        protected void Redirect(object routeValues)
        {
            if (Context.IsPartialViewRequest()) { RedirectToPartialAction(routeValues); }
            else { RedirectToAction(routeValues); }
        }
        private void RedirectToPartialAction(object routeValues)
        {
            if (Context is ExceptionContext exceptionContext) { exceptionContext.RedirectToAction(PartialAction, PartialController, routeValues); }
            else if (Context is AuthorizationContext authorizationContext) { authorizationContext.RedirectToAction(PartialAction, PartialController, routeValues); }
            else if (Context is AuthenticationContext authenticationContext) { authenticationContext.RedirectToAction(PartialAction, PartialController, routeValues); }
        }
        private void RedirectToAction(object routeValues)
        {
            if (Context is ExceptionContext exceptionContext) { exceptionContext.RedirectToAction(Action, Controller, routeValues); }
            else if (Context is AuthorizationContext authorizationContext) { authorizationContext.RedirectToAction(Action, Controller, routeValues); }
            else if (Context is AuthenticationContext authenticationContext) { authenticationContext.RedirectToAction(Action, Controller, routeValues); }
        }

    }
}
