using System;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using AuthorizePracticeByRole.DI;
using AuthorizePracticeByRole.Infra.Helpers;
using Newtonsoft.Json;
using NLog;
using SharedLibrary.Models;

namespace AuthorizePracticeByRole
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger(typeof(MvcApplication));
        
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DependencyResolver.SetResolver(new DIResolver());
        }

        void Application_Error(object sender, EventArgs e)
        {
            // var errorException = Server.GetLastError();
            var errorException = (sender as System.Web.HttpApplication).Context.Error;
            Logger.LogException(errorException);
            
            Server.ClearError();
            Response.Clear();

            switch (errorException)
            {
                case CustomException customException:
                    Response.CreateCustomExceptionResponse(customException);
                    break;
                case Exception exception:
                    Response.CreateCommonResponse(HttpStatusCode.InternalServerError);
                    break;
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            AssignCurrentUser();
        }

        private static void AssignCurrentUser()
        {
            if ((HttpContext.Current.User?.Identity.IsAuthenticated ?? false)
             && HttpContext.Current.User.Identity is FormsIdentity identity)
            {
                var ticket   = identity.Ticket;
                var userData = JsonConvert.DeserializeObject<UserDto>(ticket.UserData);
                var roles    = userData.Roles;
                HttpContext.Current.User = new GenericPrincipal(identity, roles);
            }
        }
    }
}