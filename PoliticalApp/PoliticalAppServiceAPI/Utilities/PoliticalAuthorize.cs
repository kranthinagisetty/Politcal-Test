using Server.PoliticalAppDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace PoliticalAppServiceAPI.Utilities
{
    public class PoliticalAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() ||
                actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                base.OnAuthorization(actionContext);
                return;
            }
        
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                string authToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                string username = decodedToken.Substring(0, decodedToken.IndexOf(":"));
                string password = decodedToken.Substring(decodedToken.IndexOf(":") + 1);

                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    Politician user = dbContext.Politicians.FirstOrDefault(x => x.MoblieNumber == username && x.Password == password);

                    if (user != null)
                    {
                        HttpContext.Current.User = new GenericPrincipal(new ApiPoliticalIdentity(user), new string[] { });
                      //  base.OnAuthorization(actionContext);
                        return;
                    }
                    else
                    {
                        actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                    }
                }
            }
        }
    }

    public class ApiPoliticalIdentity : IIdentity
    {
        public Politician User { get; private set; }

        public ApiPoliticalIdentity(Politician user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            this.User = user;
        }

        public string Name
        {
            get { return this.User.PoliticianName; }
        }

        public int ID
        {
            get { return this.User.PoliticianID; }
        }

        public string AuthenticationType
        {
            get { return "Basic"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }
    }
}