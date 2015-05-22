using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PoliticalAppUI.Controllers
{
    public class BaseController : Controller
    {
        protected void SetAuthInfo(Guid Id, string Email, string FullName, string Role)
        {
            //  AuthUtil.SetAuthInfo(Id, Email, FullName, Role);
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            //write your custom code here
        }
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {           

            if (Session["UserName"] != null)
            {               
                ViewBag.UserName = Session["UserName"];
            }
            else
            {
                base.OnAuthorization(filterContext);

                // Redirect to login page
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary 
                    { 
                        { "controller", "Login" }, 
                        { "action", "Index" } 
                    });

            }

            //write your custom code here
        }

    }
}
