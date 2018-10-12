using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CCAMPServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : Controller
    {
        private static ILogger log { get; } = ApplicationLogging.Logger.ForContext<LogController>();

        /// <summary>
        /// Recieves logger json from component and writes into log
        /// </summary>
        /// <param name="loggerJson"></param>
        /// <returns></returns>
        // POST api/<controller>
        [HttpPost, AllowAnonymous]
        public IActionResult Post([FromBody]JObject loggerJson)
        {
            try
            {
                Debug.WriteLine(loggerJson);
                log.Information(loggerJson.ToString());
                return Ok();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                log.Error(exception, exception.Message);
                return new JsonResult("");
            }
        }
    }
}
