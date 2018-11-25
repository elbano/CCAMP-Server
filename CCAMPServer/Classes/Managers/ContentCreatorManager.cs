using CCAMPServer.Data;
using CCAMPServerModel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CCAMPServer.Classes
{
    public class ContentCreatorManager : IContentCreatorManager
    {
        #region Variables

        private readonly TransactionDBContext _context;
        private readonly HttpContext _httpContext;
        private readonly ClaimsPrincipal _user;

        #endregion

        #region Constructor

        public ContentCreatorManager(TransactionDBContext context, HttpContext httpContext,ClaimsPrincipal user)
        {
            _context = context;
            _httpContext = httpContext;
            _user = user;
        }

        #endregion

        #region Public Methods

        public async Task CreateUpdateContentCreator( ContentCreator contentCreator= null, string authUserId = null)
        {
            if (contentCreator == null && authUserId == null)
                return;

            if (authUserId != null && contentCreator == null)
                contentCreator = _context.ContentCreator.Where(c => c.AuthUserId == authUserId).SingleOrDefault();

            var accessToken = AuthHelper.GetAuth0AccessToken(_user, _httpContext);
            UserInfo userInfo = null;

            if (accessToken != null)
            {
                userInfo = CommonFunctions.GetAuth0UserInfobyToken(accessToken.Result);

                if (userInfo == null)
                {
                    //Error
                    return ;
                }

                if (contentCreator != null)
                {
                    if (contentCreator.LastName == null || contentCreator.Name == null)
                    {
                        //Call function to retrieve data from Auth0 Endpoint

                        contentCreator.Name = userInfo.GivenName;
                        contentCreator.LastName = userInfo.FamilyName;
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    contentCreator = new ContentCreator
                    {
                        Guid = Guid.NewGuid(),
                        Name = userInfo.GivenName,
                        LastName = userInfo.FamilyName,
                        AuthUserId = userInfo.Sub,
                        UserName = userInfo.Name,
                        Email = userInfo.EmailAddress
                    };
                    _context.ContentCreator.Add(contentCreator);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async  Task<ContentCreator> GetContentCreatorById(int id)
        {
            return await _context.ContentCreator.FindAsync(id);
        }

        public async Task DeleteContentCreator(ContentCreator contentCreator)
        {
            _context.ContentCreator.Remove(contentCreator);
            await _context.SaveChangesAsync();
        }

        public bool ContentCreatorExists(int id)
        {
            return _context.ContentCreator.Any(e => e.Id == id);
        }

        public async Task SetContentCreatorState(int id, ContentCreator contentCreator)
        {
            _context.Entry(contentCreator).State = EntityState.Modified;
            await CreateUpdateContentCreator(contentCreator);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentCreatorExists(id))
                {
                    return;
                }
                else
                {
                    throw;
                }
            }
        }

        public IEnumerable<ContentCreator> GetContentCreator()
        {
            return _context.ContentCreator;
        }

        #endregion
    }
}
