using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CCAMPServer.Data;
using CCAMPServerModel.Models;
using CCAMPServer.Classes;

namespace CCAMPServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {
        private readonly AdvertisementManager _manager;

        public AdvertisementsController(TransactionDBContext context)
        {
            _manager = new AdvertisementManager(context,HttpContext, User);
        }

        // GET: api/Advertisements
        [HttpGet]
        public IEnumerable<Advertisement> GetAdvertisement()
        {
            return _manager.GetAdvertisements();
        }

        // GET: api/Advertisements/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdvertisement([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var advertisement = await _manager.GetAdvertisementById(id);

            if (advertisement == null)
            {
                return NotFound();
            }

            return Ok(advertisement);
        }

        // PUT: api/Advertisements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdvertisement([FromRoute] int id, [FromBody] Advertisement advertisement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != advertisement.Id)
            {
                return BadRequest();
            }


            await _manager.SetAdvertisementState(id, advertisement);
            return NoContent();
        }

        // POST: api/Advertisements
        [HttpPost]
        public async Task<IActionResult> PostAdvertisement([FromBody] Advertisement advertisement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _manager.AddAdvertisement(advertisement);

            return CreatedAtAction("GetAdvertisement", new { id = advertisement.Id }, advertisement);
        }

        // DELETE: api/Advertisements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdvertisement([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var advertisement = await _manager.GetAdvertisementById(id);
            if (advertisement == null)
            {
                return NotFound();
            }

            await _manager.DeleteAdvertisement(advertisement);
            return Ok(advertisement);
        }
    }
}