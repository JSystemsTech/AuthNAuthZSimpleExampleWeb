using AuthNAuthZ.Extensions;
using AuthNAuthZ.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web.Mvc;

namespace AuthNAuthZ.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public abstract class AuthorizationFilterBaseAttribute : RedirectActionFilterAttribute, IAuthorizationFilter
    {
        protected IPrincipal User => HttpContext.User;
        protected SampleIdentity Identity => User is IPrincipal && HttpContext.User is SampleIdentity identity? identity:null;

        public AuthorizationFilterBaseAttribute(string action = "Unauthorized", string partialAction = "UnauthorizedPartial", string controller = "Home", string partialController = "Home"):
            base(action, partialAction, controller, partialController)
        { }
        protected bool IsInRole(string role) => Identity != null && User.IsInRole(role);
        protected abstract bool IsAuthorized();
        protected virtual void OnUnauthorized() => Redirect();
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            OnFilterExecution(filterContext);
            if (!IsAuthorized())
            {
                if (Request.IsAjaxRequest() && !Context.IsPartialViewRequest())
                {
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }else
                {
                    OnUnauthorized();
                }                
            }
        }
    }
    public class IsInRoleAttribute : AuthorizationFilterBaseAttribute
    {
        private IEnumerable<IEnumerable<string>> RoleGroups { get; set; }
        public IsInRoleAttribute(string role) : base() { RoleGroups = new string[][] { new string[] { role } }; }
        public IsInRoleAttribute(params string[] roles) : base() { RoleGroups = new string[][] { roles }; }
        public IsInRoleAttribute(IEnumerable<string> roles) : base() { RoleGroups = new IEnumerable<string>[] { roles }; }
        public IsInRoleAttribute(params IEnumerable<string>[] roleGroups) : base() { RoleGroups = roleGroups; }
        protected override void OnUnauthorized() => Redirect(new { Message="User does not have the Role to access this view"});
        protected sealed override bool IsAuthorized()=> IsInRole("Admin") || RoleGroups.Any(roles => roles.All(role => IsInRole(role)));
    }
    public class IsOneAttribute : IsInRoleAttribute
    {
        public IsOneAttribute() : base("One") { }
    }
    public class IsTwoAttribute : IsInRoleAttribute
    {
        public IsTwoAttribute() : base("Two") { }
    }
    public class IsThreeAttribute : IsInRoleAttribute
    {
        public IsThreeAttribute() : base("Three") { }
    }
}
