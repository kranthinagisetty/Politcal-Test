using Server.PoliticalAppDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Http;

namespace PoliticalAppServiceAPI.Utilities
{
    
    public class CitizenAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
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
                Citizen user = new Citizen();
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                     user = dbContext.Citizens.FirstOrDefault(x => x.MobileNumber == username && x.Password == password);                    
                }

                if (user != null)
                {
                    HttpContext.Current.User = new GenericPrincipal(new ApiCitizenIdentity(user), new string[] { });
                   // base.OnAuthorization(actionContext);
                    return;
                }
                else
                {
                    actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                }
            }
        }
    }

    public class ApiCitizenIdentity : IIdentity
    {
        public Citizen User { get; private set; }

        public ApiCitizenIdentity(Citizen user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            this.User = user;
        }

        public int ID
        {
            get { return this.User.CitizenID; }
        }

        public string Name
        {
            get { return this.User.CitizenName; }
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