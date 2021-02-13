using AuthNAuthZ.Extensions;
using AuthNAuthZ.Identity;
using AuthNAuthZ.Principal;
using System;
using System.Net;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace AuthNAuthZ.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public abstract class AuthenticationFilterBaseAttribute : RedirectActionFilterAttribute, IAuthenticationFilter
    {
        private bool RequiresAuthentication { get; set; }
        public AuthenticationFilterBaseAttribute(bool requiresAuthentication = true, string action = "Logout", string controller = "Home") :
        base(action, action, controller, controller) { RequiresAuthentication = requiresAuthentication; }
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            OnFilterExecution(filterContext);
            if (RequiresAuthentication)
            {
                AuthenticatePrincipal();
                if (HttpContext.User == null || HttpContext.User.Identity == null || !HttpContext.User.Identity.IsAuthenticated)
                {
                    if (Request.IsAjaxRequest())
                    {
                        filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
                    }
                    else
                    {
                        OnAuthenticationFail();
                    }
                }
            }
        }
        protected abstract void AuthenticatePrincipal();
        protected virtual void OnAuthenticationFail() => Redirect(new {Message="Logging off"});


        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext) { }/*boilerplate code required by interface but unused*/
    }

    public class AuthenticationFilterAttribute : AuthenticationFilterBaseAttribute
    {
        public AuthenticationFilterAttribute(bool requiresAuthentication = true) : base(requiresAuthentication) { }
        protected override void AuthenticatePrincipal()
        {
            /*Do stuff here like checking for cookie values */
            if(Context.ActionName() == "ForbiddenTest")
            {
                HttpContext.User = null;
                return;
            }
            HttpContext.User = new SamplePrinciapl(new SampleIdentity("Test Name", new string[] { "One", "Two" }));
        }
    }

}
