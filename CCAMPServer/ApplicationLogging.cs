using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ILogger = Serilog.ILogger;

namespace CCAMPServer
{
    public class ApplicationLogging
    {
        private static ILogger logger;

        public static void ConfigureLogger(IConfiguration configuration)
        {
            logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public static ILogger Logger
        {
            get
            {                
                return logger;
            }
        }
    }
}
