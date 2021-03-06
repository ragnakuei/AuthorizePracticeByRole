﻿using System;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using AuthorizePracticeByRole.DI;
using AuthorizePracticeByRole.Infra.Helpers;
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
            DependencyResolver.SetResolver(new DiResolver());
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
    }
}