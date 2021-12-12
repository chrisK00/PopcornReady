using Microsoft.Extensions.Logging;
using System;

namespace PopcornReadyV2.Business.Extensions
{
    public static class ILoggerExtensions
    {
        public static void LogInfoWithTime(this ILogger logger, string message)
        {
            logger.LogInformation($"[{DateTime.Now}]: {message}");
        }
    }
}
