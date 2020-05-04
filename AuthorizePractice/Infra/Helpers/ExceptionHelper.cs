using System;
using System.Text;

namespace AuthorizePractice.Infra
{
    public static class ExceptionHelper
    {
        public static string GenerateExceptionLogMessage(this Exception exception)
        {
            var result = new StringBuilder();

            if (exception.InnerException != null)
            {
                result.AppendLine(GenerateExceptionLogMessage(exception.InnerException));
            }
            
            result.AppendLine($"Message:{exception.Message}");
            result.AppendLine($"StackTrace:{exception.StackTrace}");

            return result.ToString();
        }
    }
}