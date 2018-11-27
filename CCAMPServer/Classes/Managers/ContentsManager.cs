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
    public class ContentsManager:BaseManager
    {
        #region Constructor

        public ContentsManager(TransactionDBContext context, HttpContext httpContext, ClaimsPrincipal user) :
            base(context, httpContext, user)
        {
        }

        #endregion
    }
}
