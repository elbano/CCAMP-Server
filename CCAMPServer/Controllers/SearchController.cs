using CCAMPServer.Classes;
using CCAMPServer.Data;
using CCAMPServerModel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CCAMPServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : Controller
    {
        private readonly SearchManager _manager;

        public SearchController(TransactionDBContext context)
        {
            _manager = new SearchManager(context, HttpContext, User);
        }

        [HttpPost]
        public string Search([FromBody]SearchParameters queryParams )
        {
            if (string.IsNullOrWhiteSpace(queryParams.ToString()))
                return null;

            return _manager.SearchByQueryParams(queryParams.ToString());

        }

        [HttpGet("GetSearchParameters"), AllowAnonymous]
        public string GetSearchParameters()
        {
            return _manager.GetSearchParameters();
        }

    }
}