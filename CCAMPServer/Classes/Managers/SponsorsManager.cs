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
    public class SponsorsManager : BaseManager, ISponsorsManager
    {
        #region Constructor

        public SponsorsManager(TransactionDBContext context, HttpContext httpContext, ClaimsPrincipal user) :
            base(context, httpContext, user)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>Gets the Sponsors.</summary>
        /// <returns></returns>
        public IEnumerable<Sponsor> GetSponsors()
        {
            return _context.Sponsor;
        }

        /// <summary>Gets the Sponsor by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Sponsor> GetSponsorById(int id)
        {
            return await _context.Sponsor.FindAsync(id);
        }


        /// <summary>Deletes the Sponsor.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task DeleteSponsor(int id)
        {
            var sponsor = await GetSponsorById(id);
            if (sponsor != null)
            {
                await DeleteSponsor(sponsor);
            }
        }

        /// <summary>Sets the state of the Sponsor.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="sponsor">The Sponsor.</param>
        /// <returns></returns>
        public async Task SetSponsorState(int id, Sponsor sponsor)
        {
            try
            {
                _context.Entry(sponsor).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SponsorExists(id))
                {
                    return;
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>Adds the Sponsor.</summary>
        /// <param name="sponsor">The Sponsor.</param>
        /// <returns></returns>
        public async Task AddSponsor(Sponsor sponsor)
        {
            _context.Sponsor.Add(sponsor);
            await _context.SaveChangesAsync();
        }

        /// <summary>Deletes the Sponsor.</summary>
        /// <param name="sponsor">The Sponsor.</param>
        /// <returns></returns>
        public async Task DeleteSponsor(Sponsor sponsor)
        {
            _context.Sponsor.Remove(sponsor);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Sponsors exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public bool SponsorExists(int id)
        {
            return _context.Sponsor.Any(e => e.Id == id);
        }


        #endregion
    }
}
