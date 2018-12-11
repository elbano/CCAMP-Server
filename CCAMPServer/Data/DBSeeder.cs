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

            CreateOrUpdateSponsor(dBContext, Configuration.GetSection($"DBSeeder:Sponsors").Get<String>());

            CreateOrUpdateCampaign(dBContext, Configuration.GetSection($"DBSeeder:Campaigns").Get<String>());

            CreateOrUpdateChannel(dBContext, Configuration.GetSection($"DBSeeder:Channels").Get<String>());

            CreateOrUpdateDeal(dBContext, Configuration.GetSection($"DBSeeder:Deals").Get<String>());
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
                var contentCreatorList = GetSetupRecord<User>(jsonFile);

                foreach (var newContentCreator in contentCreatorList)
                {
                    var contentCreator = dBContext.User.FirstOrDefault(x => x.Guid.Equals(newContentCreator.Guid));

                    if (contentCreator == null)
                    {                       
                        dBContext.User.Add(newContentCreator);
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

        /// <summary>
        /// Creates or updates the initial recrods for sponsors
        /// </summary>
        /// <param name="dBContext"></param>
        private static void CreateOrUpdateSponsor(ApplicationDBContext dBContext, String jsonFile)
        {
            try
            {
                var sponsorList = GetSetupRecord<User>(jsonFile);

                foreach (var newSponsor in sponsorList)
                {
                    var sponsor = dBContext.User.FirstOrDefault(x => x.Guid.Equals(newSponsor.Guid));

                    if (sponsor == null)
                    {
                        dBContext.User.Add(newSponsor);
                    }
                    else
                    {
                        newSponsor.Id = sponsor.Id;
                        dBContext.Entry(sponsor).CurrentValues.SetValues(newSponsor);
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

        /// <summary>
        /// Creates or updates the initial recrods for campaigns
        /// </summary>
        /// <param name="dBContext"></param>
        private static void CreateOrUpdateCampaign(ApplicationDBContext dBContext, String jsonFile)
        {
            try
            {
                var campaignList = GetSetupRecord<Campaign>(jsonFile);

                foreach (var newCampaign in campaignList)
                {
                    var campaign = dBContext.Campaign.FirstOrDefault(x => x.Guid.Equals(newCampaign.Guid));
                    var sponsor = dBContext.User.FirstOrDefault(x => x.Guid.Equals(newCampaign.SponsorUser.Guid));
                    if (sponsor != null) newCampaign.SponsorUser = sponsor;

                    if (campaign == null)
                    {
                        dBContext.Campaign.Add(newCampaign);
                    }
                    else
                    {
                        newCampaign.Id = campaign.Id;
                        dBContext.Entry(campaign).CurrentValues.SetValues(newCampaign);
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

        /// <summary>
        /// Creates or updates the initial recrods for channels
        /// </summary>
        /// <param name="dBContext"></param>
        private static void CreateOrUpdateChannel(ApplicationDBContext dBContext, String jsonFile)
        {
            try
            {
                var channelList = GetSetupRecord<Channel>(jsonFile);

                foreach (var newChannel in channelList)
                {
                    var channel = dBContext.Channel.FirstOrDefault(x => x.Guid.Equals(newChannel.Guid));
                    var contentCreator = dBContext.User.FirstOrDefault(x => x.Guid.Equals(newChannel.ContentCreatorUser.Guid));
                    if (contentCreator != null) newChannel.ContentCreatorUser = contentCreator;

                    if (channel == null)
                    {
                        dBContext.Channel.Add(newChannel);
                    }
                    else
                    {
                        newChannel.Id = channel.Id;
                        dBContext.Entry(channel).CurrentValues.SetValues(newChannel);
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

        /// <summary>
        /// Creates or updates the initial recrods for deals
        /// </summary>
        /// <param name="dBContext"></param>
        private static void CreateOrUpdateDeal(ApplicationDBContext dBContext, String jsonFile)
        {
            try
            {
                var dealList = GetSetupRecord<Deal>(jsonFile);

                foreach (var newDeal in dealList)
                {
                    var deal = dBContext.Deal.FirstOrDefault(x => x.Guid.Equals(newDeal.Guid));
                    var channel = dBContext.Channel.FirstOrDefault(x => x.Guid.Equals(newDeal.Channel.Guid));
                    if (channel != null) newDeal.Channel = channel;
                    var campaign = dBContext.Campaign.FirstOrDefault(x => x.Guid.Equals(newDeal.Campaign.Guid));
                    if (campaign != null) newDeal.Campaign = campaign;

                    if (deal == null)
                    {
                        dBContext.Deal.Add(newDeal);
                    }
                    else
                    {
                        newDeal.Id = deal.Id;
                        dBContext.Entry(deal).CurrentValues.SetValues(newDeal);
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
