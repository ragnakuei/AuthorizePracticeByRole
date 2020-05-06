using System;
using System.Net;
using System.Web;
using SharedLibrary.Models;

namespace AuthorizePracticeByRole.Infra.Helpers
{
    public static class HttpResponseHelper
    {
        public static void CreateCustomExceptionResponse(this HttpResponse response, CustomException customException)
        {
            HttpStatusCode httpStatus = HttpStatusCode.OK;

            switch (customException.ErrorCode)
            {
                case HttpStatusCode.Unauthorized:
                    httpStatus = customException.IsAuthenticated
                                     ? HttpStatusCode.NotFound
                                     : HttpStatusCode.Unauthorized;
                    break;
                case HttpStatusCode.Forbidden:
                    break;
                case HttpStatusCode.NotFound:
                    break;
                case HttpStatusCode.ServiceUnavailable:
                    break;
                case HttpStatusCode.InternalServerError:
                default:
                    httpStatus = HttpStatusCode.InternalServerError;
                    break;
            }

            CreateCommonResponse(response, httpStatus);
        }

        public static void CreateCommonResponse(this HttpResponse response, HttpStatusCode httpStatus)
        {
            response.Status     = $"{(int)httpStatus} {httpStatus.ToString()}";
            response.StatusCode = (int)httpStatus;
        }
    }
}