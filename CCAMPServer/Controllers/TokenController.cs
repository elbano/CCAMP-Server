using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using CCAMPServer.Classes;
using CCAMPServer.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace CCAMPServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        
        [HttpPost("token"), AllowAnonymous]
        public string GetToken(TokenRequest request)
        {

            return CommonFunctions.GetToken(request, HttpContext);
        }

        [HttpGet("result"), AllowAnonymous]
        public async Task<string> GetResultAsync()
        {
            //await CommonFunctions.GetTokenResponseAsync(Request, HttpContext, _context);

            return "hi";
        }
    }
}