using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CCAMPServer.Data;
using CCAMPServerModel.Models;

namespace CCAMPServer.Classes
{
    public abstract class BaseManager
    {
        #region Variables

        protected readonly TransactionDBContext _context;
        protected readonly HttpContext _httpContext;
        protected readonly ClaimsPrincipal _user;

        #endregion

        #region Constructors

        public BaseManager(TransactionDBContext context, HttpContext httpContext, ClaimsPrincipal user)
        {
            _context = context;
            _httpContext = httpContext;
            _user = user;
        }

        #endregion
    }
}
