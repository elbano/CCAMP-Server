using CCAMPServer.Data;
using CCAMPServerModel.Extensions;
using CCAMPServerModel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CCAMPServer.Classes
{
    public class ChannelManager : BaseManager, IChannelManager
    {
        #region Constructor

        public ChannelManager(TransactionDBContext context, HttpContext httpContext, ClaimsPrincipal user) :
            base(context, httpContext, user)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>Gets the channels.</summary>
        /// <returns></returns>
        public IEnumerable<Channel> GetChannels()
        {
            return _context.Channel;
        }

        /// <summary>Gets the channel by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Channel> GetChannelById(int id)
        {
            return await _context.Channel.FindAsync(id);
        }

        /// <summary>Channels the exists.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public bool ChannelExists(int id)
        {
            return _context.Channel.Any(e => e.Id == id);
        }

        /// <summary>Gets the channels by key word.</summary>
        /// <param name="searchString">The search string.</param>
        /// <returns></returns>
        public JsonResult GetChannelsByKeyWord(string searchString)
        {
            JsonResult jsonResult = null;
            string[] searchTerms = searchString.Split(' ');

            try
            {
                var channelList = _context.Channel.Where(x => x.KeyWords.ContainsAny(searchTerms)).ToList();

                if (channelList != null)
                { 
                  return  jsonResult = new JsonResult(channelList, ApplicationJsonSerializerSettings.Settings);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return jsonResult;
        }

        /// <summary>Sets the state of the channel.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="channel">The channel.</param>
        /// <returns></returns>
        public async Task SetChannelState(int id, Channel channel)
        {
            try
            {
                _context.Entry(channel).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChannelExists(id))
                {
                    return;
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>Adds the channel.</summary>
        /// <param name="channel">The channel.</param>
        /// <returns></returns>
        public async Task AddChannel(Channel channel)
        {
            _context.Channel.Add(channel);
            await _context.SaveChangesAsync();
        }

        /// <summary>Deletes the channel.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task DeleteChannel(int id)
        {
            var channel = await GetChannelById(id);
            if (channel != null)
            {
                await DeleteChannel(channel);
            }
        }

        /// <summary>Deletes the channel.</summary>
        /// <param name="channel">The channel.</param>
        /// <returns></returns>
        public async Task DeleteChannel(Channel channel)
        {
            _context.Channel.Remove(channel);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
