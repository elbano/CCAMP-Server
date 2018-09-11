using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CCAMPServer.Data
{
    public class DBContextFactory
    {
        private static ILogger log { get; } = ApplicationLogging.Logger;
        private static Dictionary<int, String> connectionStrings;
        public const int TRANSACTION = 1;
        public const int ROOT = 2;

        /// <summary>
        /// Adds a connection string to the dictionary of connections
        /// </summary>
        /// <param name="id"></param>
        /// <param name="connectionStr"></param>
        public static void AddConnectionString(int id, String connectionStr)
        {
            if (connectionStrings == null) connectionStrings = new Dictionary<int, string>();
            connectionStrings.Add(id, connectionStr);
        }

        /// <summary>
        /// Creates the ApplicationDbContext based on the connection type, default return is transaction based connection
        /// </summary>
        /// <param name="connectionType"></param>
        /// <returns></returns>
        public static ApplicationDBContext Create(int connectionType)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();

            try
            {
                switch (connectionType)
                {
                    case TRANSACTION:
                        optionsBuilder.UseSqlServer(connectionStrings[TRANSACTION]);
                        return new ApplicationDBContext(optionsBuilder.Options);
                    case ROOT:
                        optionsBuilder.UseSqlServer(connectionStrings[ROOT]);
                        return new ApplicationDBContext(optionsBuilder.Options);
                    default:
                        optionsBuilder.UseSqlServer(connectionStrings[TRANSACTION]);
                        return new ApplicationDBContext(optionsBuilder.Options);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                log.Error(exception, exception.Message);
                throw new ArgumentNullException("ConnectionId");
            }
        }
    }
}
