using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCAMPServer.Data
{
    public class DBSeeder
    {
        private static ILogger log { get; } = ApplicationLogging.Logger;

        #region Methods        
        /// <summary>
        /// Seeding void that check for current records and populates DB with initial data
        /// </summary>
        /// <param name="dBContext"></param>
        public static void Seed(ApplicationDBContext dBContext, IConfiguration Configuration)
        {
            
        }
        #endregion Methods
    }
}
