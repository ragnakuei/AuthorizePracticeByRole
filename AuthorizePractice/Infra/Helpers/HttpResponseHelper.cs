using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Routing;
using AuthorizePractice.Controllers;
using SharedLibrary.Models;

namespace AuthorizePractice.Infra
{
    public static class HttpResponseHelper
    {
        public static void CreateCustomExceptionResponse(this HttpResponse response, CustomException customException)
        {
            switch (customException.ErrorCode)
            {
                case HttpStatusCode.Unauthorized:
                    response.Create401UnauthorizedResponse(customException);
                    break;
                case HttpStatusCode.Forbidden:
                    break;
                case HttpStatusCode.NotFound:
                    break;
                case HttpStatusCode.ServiceUnavailable:
                    break;
                case HttpStatusCode.InternalServerError:
                default:
                    Create500ErrorResponse(response, customException);
                    break;
            }
        }

        private static void Create401UnauthorizedResponse(this HttpResponse response, CustomException customException)
        {
            var httpStatus = HttpStatusCode.Unauthorized;
            CreateCommonResponse(response, httpStatus);
        }

        public static void Create500ErrorResponse(this HttpResponse response, Exception exception)
        {
            var httpStatus = HttpStatusCode.InternalServerError;
            CreateCommonResponse(response, httpStatus);
        }

        private static void CreateCommonResponse(HttpResponse response, HttpStatusCode httpStatus)
        {
            response.Status     = $"{(int)httpStatus} {httpStatus.ToString()}";
            response.StatusCode = (int)httpStatus;
        }
    }
}