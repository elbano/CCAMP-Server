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
    public class AdvertisementManager : BaseManager, IAdvertisementManager
    {
        #region Constructor

        public AdvertisementManager(TransactionDBContext context, HttpContext httpContext, ClaimsPrincipal user) :
            base(context, httpContext, user)
        {
        }

        #endregion

        #region Public Methods

        public IEnumerable<Advertisement> GetAdvertisements()
        {
            return _context.Advertisement;
        }

        public async Task<Advertisement> GetAdvertisementById(int id)
        {
            return  await _context.Advertisement.FindAsync(id);
        }

        public async Task AddAdvertisement(Advertisement advertisement)
        {
            _context.Advertisement.Add(advertisement);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAdvertisement(int id)
        {
            var advertisement = await GetAdvertisementById(id);
            if (advertisement != null)
            {
                await DeleteAdvertisement(advertisement);
            }
        }

        public async Task DeleteAdvertisement(Advertisement advertisement)
        {
            _context.Advertisement.Remove(advertisement);
            await _context.SaveChangesAsync();
        }

        public async Task SetAdvertisementState(int id, Advertisement advertisement)
        {
            try
            {
                _context.Entry(advertisement).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdvertisementExists(id))
                {
                    return;
                }
                else
                {
                    throw;
                }
            }
        }

        public bool AdvertisementExists(int id)
        {
            return _context.Advertisement.Any(e => e.Id == id);
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
