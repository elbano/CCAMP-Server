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


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CCAMPServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private static ILogger log { get; } = ApplicationLogging.Logger.ForContext<ChannelController>();
        private readonly ChannelManager _manager;

        public ChannelController(TransactionDBContext context)
        {
            _manager = new ChannelManager(context,HttpContext, User);
        }

        // GET: api/Channels
        [HttpGet, AllowAnonymous]
        public IEnumerable<Channel> GetChannel()
        {
            return _manager.GetChannels();
        }

        // GET: api/Channels/5
        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> GetChannel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var channel = await _manager.GetChannelById(id);

            if (channel == null)
            {
                return NotFound();
            }

            return Ok(channel);
        }

        // GET: api/Channels/5
        [HttpGet("keys={searchString}"), AllowAnonymous]
        public IActionResult GetChannelKeyWordSearch(string searchString)
        {
            JsonResult jsonResult = null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                jsonResult = _manager.GetChannelsByKeyWord(searchString);

                if (jsonResult == null)
                {
                    return NotFound();
                }
                else
                {
                    jsonResult.StatusCode = (int)HttpStatusCode.OK;
                }

                return jsonResult;
            }
            catch (Exception exception)
            {
                log.Error(exception, exception.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // PUT: api/Channels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChannel([FromRoute] int id, [FromBody] Channel channel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != channel.Id)
            {
                return BadRequest();
            }

            await _manager.SetChannelState(id, channel);

            return NoContent();
        }

        // POST: api/Channels
        [HttpPost]
        public async Task<IActionResult> PostChannel([FromBody] Channel channel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _manager.AddChannel(channel);
            return CreatedAtAction("GetChannel", new { id = channel.Id }, channel);
        }

        // DELETE: api/Channels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChannel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var channel = await _manager.GetChannelById(id);
            if (channel == null)
            {
                return NotFound();
            }

            await _manager.DeleteChannel(channel);

            return Ok(channel);
        }
    }
}
