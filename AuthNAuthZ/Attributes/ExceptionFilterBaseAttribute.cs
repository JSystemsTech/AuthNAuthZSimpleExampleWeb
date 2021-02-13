using AuthNAuthZ.Extensions;
using System;
using System.Net;
using System.Web.Mvc;

namespace AuthNAuthZ.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public abstract class ExceptionFilterBaseAttribute : RedirectActionFilterAttribute, IExceptionFilter
    {
        public ExceptionFilterBaseAttribute(string action = "Error", string partialAction = "ErrorPartial", string controller = "Home", string partialController = "Home") : 
            base(action, partialAction, controller, partialController) { }


        protected virtual string OnException(Exception e) => e.Message;
        protected virtual void OnRedirect(Exception e,string message) => Redirect(new { Message = message });
        public void OnException(ExceptionContext filterContext)
        {
            OnFilterExecution(filterContext);

            filterContext.ExceptionHandled = true;
            string message = OnException(filterContext.Exception);
            if (Request.IsAjaxRequest() && !filterContext.IsPartialViewRequest())
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.InternalServerError, message);
            }
            else
            {
                OnRedirect(filterContext.Exception, message);
            }
        }
    }
    public class ExceptionFilterAttribute : ExceptionFilterBaseAttribute { }
}
