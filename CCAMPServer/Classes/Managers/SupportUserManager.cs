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
    public class SupportUserManager : BaseManager, ISupportUserManager
    {
        #region Constructor

        public SupportUserManager(TransactionDBContext context, HttpContext httpContext, ClaimsPrincipal user) :
            base(context, httpContext, user)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>Gets the SupportUsers.</summary>
        /// <returns></returns>
        public IEnumerable<SupportUser> GetSupportUsers()
        {
            return _context.SupportUser;
        }

        /// <summary>Gets the SupportUser by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<SupportUser> GetSupportUserById(int id)
        {
            return await _context.SupportUser.FindAsync(id);
        }


        /// <summary>Deletes the SupportUser.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task DeleteSupportUser(int id)
        {
            var SupportUser = await GetSupportUserById(id);
            if (SupportUser != null)
            {
                await DeleteSupportUser(SupportUser);
            }
        }

        /// <summary>Sets the state of the SupportUser.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="supportUser">The SupportUser.</param>
        /// <returns></returns>
        public async Task SetSupportUserState(int id, SupportUser supportUser)
        {
            try
            {
                _context.Entry(supportUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupportUserExists(id))
                {
                    return;
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>Adds the SupportUser.</summary>
        /// <param name="supportUser">The SupportUser.</param>
        /// <returns></returns>
        public async Task AddSupportUser(SupportUser supportUser)
        {
            _context.SupportUser.Add(supportUser);
            await _context.SaveChangesAsync();
        }

        /// <summary>Deletes the SupportUser.</summary>
        /// <param name="supportUser">The SupportUser.</param>
        /// <returns></returns>
        public async Task DeleteSupportUser(SupportUser supportUser)
        {
            _context.SupportUser.Remove(supportUser);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// SupportUsers  exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public bool SupportUserExists(int id)
        {
            return _context.SupportUser.Any(e => e.Id == id);
        }


        #endregion
    }
}
