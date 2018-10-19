using CCAMPServerModel.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            CreateOrUpdateContentCreator(dBContext, Configuration.GetSection($"DBSeeder:ContentCreators").Get<String>());
        }

        /// <summary>
        /// Return initial records in the given type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonFile"></param>
        /// <returns></returns>
        private static List<T> GetSetupRecord<T>(String jsonFile)
        {
            try
            {
                var jsonList = System.IO.File.ReadAllText(jsonFile);

                var list = JsonConvert.DeserializeObject<List<T>>(jsonList, ApplicationJsonSerializerSettings.SettingsDBSeader);

                return list;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                log.Error(exception, exception.Message);
            }

            return new List<T>();
        }

        /// <summary>
        /// Creates or updates the initial recrods for content creators
        /// </summary>
        /// <param name="dBContext"></param>
        private static void CreateOrUpdateContentCreator(ApplicationDBContext dBContext, String jsonFile)
        {
            try
            {
                var contentCreatorList = GetSetupRecord<ContentCreator>(jsonFile);

                foreach (var newContentCreator in contentCreatorList)
                {
                    var contentCreator = dBContext.ContentCreator.FirstOrDefault(x => x.Guid.Equals(newContentCreator.Guid));

                    if (contentCreator == null)
                    {                       
                        dBContext.ContentCreator.Add(newContentCreator);
                    }
                    else
                    {
                        newContentCreator.Id = contentCreator.Id;
                        dBContext.Entry(contentCreator).CurrentValues.SetValues(newContentCreator);
                    }
                }
                dBContext.SaveChanges();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                log.Error(exception, exception.Message);
            }
        }
        #endregion Methods
    }
}
