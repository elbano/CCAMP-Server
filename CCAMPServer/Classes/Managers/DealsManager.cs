using CCAMPServer.Data;
using CCAMPServerModel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CCAMPServer.Classes
{
    public class DealsManager : BaseManager, IDealsManager
    {
        #region Constructors

        public DealsManager(TransactionDBContext context, HttpContext httpContext, ClaimsPrincipal user) :
            base(context, httpContext, user)
        {

        }

        #endregion

        #region Public Methods

        public JsonResult GetUserDeals(int status)
        {
            var jsonResult = new JsonResult("");
            var authToken = AuthHelper.GetTokenUserId(_user);

            try
            {
                var dealList = _context.Deal.Include(d => d.Campaign).ThenInclude(x => x.Sponsor).
                    Where(x => x.Channel.ContentCreator.AuthUserId.
                        Equals(authToken, StringComparison.InvariantCultureIgnoreCase)).ToList();

                if (dealList == null)
                {
                    return null;
                }
                else
                {
                    jsonResult = new JsonResult(dealList, ApplicationJsonSerializerSettings.Settings);
                    jsonResult.StatusCode = (int)HttpStatusCode.OK;
                }

                return jsonResult;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SetDealState(int id, Deal deal)
        {
            try
            {
                _context.Entry(deal).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DealExists(id))
                {
                    return;
                }
                else
                {
                    throw;
                }
            }
        }

        public bool DealExists(int id)
        {
            return _context.Deal.Any(e => e.Id == id);
        }

        public async Task CreateUpdateDeal(Deal deal)
        {
            _context.Deal.Add(deal);
            await _context.SaveChangesAsync();
        }

        public async Task<Deal> GetDealById(int id)
        {
            return await _context.Deal.FindAsync(id);
        }

        public async Task DeleteDealById(int id)
        {
            var deal = await GetDealById(id);

            if (deal == null)
            {
                return ;
            }

            await DeleteDeal(deal);
        }

        public async Task DeleteDeal(Deal deal)
        {
            _context.Deal.Remove(deal);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
