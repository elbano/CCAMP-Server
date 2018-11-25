using System.Collections.Generic;
using System.Threading.Tasks;
using CCAMPServerModel.Models;

namespace CCAMPServer.Classes
{
    public interface ICampaignsManager
    {
        Task AddCampaign(Campaign campaign);
        bool CampaignExists(int id);
        Task DeleteCampaign(Campaign campaign);
        Task DeleteCampaign(int id);
        Task<Campaign> GetCampaignById(int id);
        IEnumerable<Campaign> GetCampaigns();
        Task SetCampaignState(int id, Campaign campaign);
    }
}