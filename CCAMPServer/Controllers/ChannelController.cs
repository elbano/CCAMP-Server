using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCAMPServer.Data;
using CCAMPServerModel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CCAMPServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private static ILogger log { get; } = ApplicationLogging.Logger.ForContext<ChannelController>();
        // GET: api/Channels
        [HttpGet]
        public IEnumerable<Channel> GetChannel()
        {
            using (var dbContext = DBContextFactory.Create(DBContextFactory.TRANSACTION))
            {
                return dbContext.Channel;
            }
        }

        // GET: api/Channels/5
        [HttpGet("{guid}")]
        public async Task<IActionResult> GetChannel([FromRoute] Guid guid)
        {
            if (!ModelState.IsValid)
            {
                log.Warning("BadRequest");
                return BadRequest(ModelState);
            }

            using (var dbContext = DBContextFactory.Create(DBContextFactory.TRANSACTION))
            {
                var channel = await dbContext.Channel.FindAsync(guid);

                if (channel == null)
                {
                    return NotFound();
                }

                return Ok(channel);
            }
        }

        // PUT: api/Channels/5
        [HttpPut("{guid}")]
        public async Task<IActionResult> PutChannel([FromRoute] Guid guid, [FromBody] Channel channel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!guid.Equals(channel.Guid))
            {
                return BadRequest();
            }

            using (var dbContext = DBContextFactory.Create(DBContextFactory.TRANSACTION))
            {
                dbContext.Entry(channel).State = EntityState.Modified;

                try
                {
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChannelExists(guid, dbContext))
                    {
                        log.Warning("NotFound");
                        return NotFound();
                    }
                    else
                    {
                        log.Warning("throw");
                        throw;
                    }
                }
            }

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

            using (var dbContext = DBContextFactory.Create(DBContextFactory.TRANSACTION))
            {
                dbContext.Channel.Add(channel);
                await dbContext.SaveChangesAsync();
            }

            return CreatedAtAction("GetChannel", new { id = channel.Id }, channel);
        }

        // DELETE: api/Channels/5
        [HttpDelete("{guid}")]
        public async Task<IActionResult> DeleteChannel([FromRoute] Guid guid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var dbContext = DBContextFactory.Create(DBContextFactory.TRANSACTION))
            {
                var channel = await dbContext.Channel.FindAsync(guid);
                if (channel == null)
                {
                    return NotFound();
                }

                dbContext.Channel.Remove(channel);
                await dbContext.SaveChangesAsync();
            }

            return NoContent();
        }

        private bool ChannelExists(Guid guid, ApplicationDBContext dbContext)
        {
            return dbContext.Channel.Any(e => e.Guid.Equals(guid));
        }
    }
}
