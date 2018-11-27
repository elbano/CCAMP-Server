using CCAMPServer.Classes;
using CCAMPServer.Data;
using CCAMPServerModel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCAMPServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SponsorsController : ControllerBase
    {
        private readonly SponsorsManager _manager;

        public SponsorsController(TransactionDBContext context)
        {
            _manager = new SponsorsManager(context, HttpContext, User);
        }

        // GET: api/Sponsors
        [HttpGet]
        public IEnumerable<Sponsor> GetSponsor()
        {
            return _manager.GetSponsors();
        }

        // GET: api/Sponsors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSponsor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sponsor = await _manager.GetSponsorById(id);

            if (sponsor == null)
            {
                return NotFound();
            }

            return Ok(sponsor);
        }

        // PUT: api/Sponsors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSponsor([FromRoute] int id, [FromBody] Sponsor sponsor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sponsor.Id)
            {
                return BadRequest();
            }

            await _manager.SetSponsorState(id, sponsor);

            return NoContent();
        }

        // POST: api/Sponsors
        [HttpPost]
        public async Task<IActionResult> PostSponsor([FromBody] Sponsor sponsor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _manager.AddSponsor(sponsor);

            return CreatedAtAction("GetSponsor", new { id = sponsor.Id }, sponsor);
        }

        // DELETE: api/Sponsors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSponsor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sponsor = await _manager.GetSponsorById(id);
            if (sponsor == null)
            {
                return NotFound();
            }

            await _manager.DeleteSponsor(sponsor);

            return Ok(sponsor);
        }
    }
}