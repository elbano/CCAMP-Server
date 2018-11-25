using CCAMPServer.Classes;
using CCAMPServer.Data;
using CCAMPServerModel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CCAMPServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class DealsController : ControllerBase
    {
        private static ILogger log { get; } = ApplicationLogging.Logger.ForContext<DealsController>();
        private readonly DealsManager _manager;

        public DealsController(TransactionDBContext context)
        {
            _manager = new DealsManager(context, HttpContext, User);
        }
        
        // GET: api/Deals/5
        [HttpGet]
        public IActionResult GetDealsOfUser(int status)
        {            
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var lstDeals =_manager.GetUserDeals(status);

                if (lstDeals == null)
                {
                    return NotFound();
                }
                else
                {
                    return lstDeals;
                };
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

            var deal = await _manager.GetDealById(id); 

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

            try
            {
                await _manager.SetContentCreatorState(id, deal);
            }
            catch (Exception exception)
            {
                log.Error(exception, exception.Message);
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

            try
            {
                await _manager.CreateUpdateDeal(deal);
            }
            catch (Exception exception)
            {
                log.Error(exception, exception.Message);
            }

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

            var deal = await _manager.GetDealById(id);

            if (deal == null)
            {
                return NoContent();
            }

            try
            {
                await _manager.DeleteDealById(id);
            }
            catch (Exception exception)
            {
                log.Error(exception, exception.Message);
            }

            return Ok(deal);
        }
    }
}