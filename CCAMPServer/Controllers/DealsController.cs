using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CCAMPServer.Data;
using CCAMPServerModel.Models;
using Serilog;
using System.Net;
using Newtonsoft.Json;

namespace CCAMPServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealsController : Controller
    {
        private static ILogger log { get; } = ApplicationLogging.Logger.ForContext<DealsController>();
        private readonly TransactionDBContext _context;

        public DealsController(TransactionDBContext context)
        {
            _context = context;
        }
        
        // GET: api/Deals/5
        [HttpGet]
        public IActionResult GetDealsOfUser(int status)
        {            
            var jsonResult = new JsonResult("");            
            var authToken = AuthHelper.getTokenUserId(User);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var dealList = _context.Deal.Include(d => d.Campaign).ThenInclude(x => x.SponsorUser).
                    Where(x => x.Channel.ContentCreatorUser.AuthUserId.
                        Equals(authToken, StringComparison.InvariantCultureIgnoreCase)).ToList();

                if (dealList == null)
                {
                    return NotFound();
                }
                else
                {
                    jsonResult = new JsonResult(dealList, ApplicationJsonSerializerSettings.Settings);
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

        // GET: api/Deals/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deal = await _context.Deal.FindAsync(id);

            if (deal == null)
            {
                return NotFound();
            }

            return Ok(deal);
        }

        // PUT: api/Deals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeal([FromRoute] int id, [FromBody] Deal deal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deal.Id)
            {
                return BadRequest();
            }

            _context.Entry(deal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DealExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Deals
        [HttpPost]
        public async Task<IActionResult> PostDeal([FromBody] Deal deal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Deal.Add(deal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeal", new { id = deal.Id }, deal);
        }

        // DELETE: api/Deals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deal = await _context.Deal.FindAsync(id);
            if (deal == null)
            {
                return NotFound();
            }

            _context.Deal.Remove(deal);
            await _context.SaveChangesAsync();

            return Ok(deal);
        }

        private bool DealExists(int id)
        {
            return _context.Deal.Any(e => e.Id == id);
        }
    }
}