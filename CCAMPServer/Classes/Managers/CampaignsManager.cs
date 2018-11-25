using CCAMPServer.Data;
using CCAMPServerModel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace CCAMPServer.Classes
{
    public class CampaignsManager : BaseManager, ICampaignsManager
    {
        #region Constructor

        public CampaignsManager(TransactionDBContext context, HttpContext httpContext, ClaimsPrincipal user) :
            base(context, httpContext, user)
        {
        }

        #endregion

        #region Public Methods

        public IEnumerable<Campaign> GetCampaigns()
        {
            return _context.Campaign;
        }

        public async Task<Campaign> GetCampaignById(int id)
        {
            return await _context.Campaign.FindAsync(id);
        }

        public async Task SetCampaignState(int id, Campaign campaign)
        {
            try
            {
                _context.Entry(campaign).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampaignExists(id))
                {
                    return;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task AddCampaign(Campaign campaign)
        {
            _context.Campaign.Add(campaign);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCampaign(int id)
        {
            var campaign = await GetCampaignById(id);
            if (campaign != null)
            {
                await DeleteCampaign(campaign);
            }
        }

        public async Task DeleteCampaign(Campaign campaign)
        {
            _context.Campaign.Remove(campaign);
            await _context.SaveChangesAsync();
        }

        public bool CampaignExists(int id)
        {
            return _context.Campaign.Any(e => e.Id == id);
        }

        #endregion
        
        #region Private Methods

        #endregion
    }
}
