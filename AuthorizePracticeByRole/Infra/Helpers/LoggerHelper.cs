using System;
using NLog;

namespace AuthorizePracticeByRole.Infra.Helpers
{
    public static class LoggerHelper
    {
        public static void LogException(this ILogger logger, Exception exception)
        {
            var exceptionLog = exception.GenerateExceptionLogMessage();
            logger.Error(exceptionLog);
        }
    }
}