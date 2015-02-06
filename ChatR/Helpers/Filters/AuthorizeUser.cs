using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ChatR.Helpers.Filters
{
    public class AuthorizeUser : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext actionContext)
        {
            //bool authorized = true;
            //string url = HttpContext.Current.Request.Url.LocalPath;

            //ChatR.Entities.User user = (ChatR.Entities.User)HttpContext.Current.Session["User"];

            //if (user == null)
            //    authorized = false;

            //if (!authorized)
            //{
            //    RouteValueDictionary route = new RouteValueDictionary { { "Controller", "Account" }, { "Action", "Login" }, { "returnUrl", url } };
            //    actionContext.Result = new RedirectToRouteResult(route);
            //}

            this.OnActionExecuting(actionContext);
                
        }
    }
}