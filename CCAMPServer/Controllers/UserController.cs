using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CCAMPServer.Classes;
using CCAMPServer.Data;
using CCAMPServerModel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CCAMPServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private static ILogger log { get; } = ApplicationLogging.Logger.ForContext<DealsController>();
        private readonly TransactionDBContext _context;

        public UserController(TransactionDBContext context)
        {
            _context = context;
        }       

        // POST: api/Users
        [HttpPost, AllowAnonymous]
        public void PostUserAsync([FromBody]JObject userJson)
        {
            var authToken = AuthHelper.getTokenUserId(User);

            try
            {
                var user = userJson.ToObject<User>();

                var itExist = _context.User.Any(x => x.AuthUserId.Equals(authToken, StringComparison.InvariantCultureIgnoreCase));

                if (!itExist)
                {
                    //INSERT USER HERE, GET EMAIL FROM AUTH TOKEN BASED ON FRAN IMPLEMENTATION
                    user.AuthUserId = authToken;
                    user.Guid = Guid.NewGuid();
                    user.Status = EStatusMode.sm_Active;
                    user.CreationDate = DateTime.Now;

                    _context.User.Add(user);
                    _context.SaveChanges();
                    Debug.WriteLine("REGISTER");
                }
                else
                {
                    Debug.WriteLine("LOGIN - TEMPORARY ELSE, NO USE FOR THIS YET.....");
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                log.Warning(exception, exception.Message);
            }
        }
    }
}

