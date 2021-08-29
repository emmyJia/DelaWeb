using System;
using System.Web.Helpers;
using System.Web.Mvc;

namespace DelaWeb.Attributes
{
    //public class CustomValidateAntiForgeryToken : FilterAttribute, IExceptionFilter
    //{
    //    public override void IExceptionFilter.OnException(ExceptionContext filterContext)
    //    {
    //        Log(filterContext.Exception);

    //        base.OnException(filterContext);
    //    }

    //    private void Log(Exception exception)
    //    {
    //        //log exception here..

    //    }
    //}
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class CustomValidateAntiForgeryToken : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var httpContext = filterContext.HttpContext;
            var cookie = httpContext.Request.Cookies[AntiForgeryConfig.CookieName];
            AntiForgery.Validate(cookie != null ? cookie.Value : null, httpContext.Request.Headers["__RequestVerificationToken"]);
        }
    }
}