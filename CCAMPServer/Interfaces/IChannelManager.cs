using System.Collections.Generic;
using System.Threading.Tasks;
using CCAMPServerModel.Models;
using Microsoft.AspNetCore.Mvc;

namespace CCAMPServer.Classes
{
    public interface IChannelManager
    {
        Task AddChannel(Channel channel);
        bool ChannelExists(int id);
        Task DeleteChannel(Channel channel);
        Task DeleteChannel(int id);
        Task<Channel> GetChannelById(int id);
        IEnumerable<Channel> GetChannels();
        JsonResult GetChannelsByKeyWord(string searchString);
        Task SetChannelState(int id, Channel channel);
    }
}