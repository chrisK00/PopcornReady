using Microsoft.Extensions.Logging;
using System;

namespace PopcornReady.Core.Extensions
{
    public static class ILoggerExtensions
    {
        public static void LogInfoWithTime(this ILogger logger, string message)
        {
            logger.LogInformation($"[{DateTime.Now}]: {message}");
        }
    }
}
