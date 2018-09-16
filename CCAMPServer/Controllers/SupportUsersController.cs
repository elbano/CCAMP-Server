using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CCAMPServer.Data;
using CCAMPServerModel.Models;

namespace CCAMPServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportUsersController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public SupportUsersController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/SupportUsers
        [HttpGet]
        public IEnumerable<SupportUser> GetSupportUser()
        {
            return _context.SupportUser;
        }

        // GET: api/SupportUsers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupportUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var supportUser = await _context.SupportUser.FindAsync(id);

            if (supportUser == null)
            {
                return NotFound();
            }

            return Ok(supportUser);
        }

        // PUT: api/SupportUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupportUser([FromRoute] int id, [FromBody] SupportUser supportUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != supportUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(supportUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupportUserExists(id))
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

        // POST: api/SupportUsers
        [HttpPost]
        public async Task<IActionResult> PostSupportUser([FromBody] SupportUser supportUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SupportUser.Add(supportUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupportUser", new { id = supportUser.Id }, supportUser);
        }

        // DELETE: api/SupportUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupportUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var supportUser = await _context.SupportUser.FindAsync(id);
            if (supportUser == null)
            {
                return NotFound();
            }

            _context.SupportUser.Remove(supportUser);
            await _context.SaveChangesAsync();

            return Ok(supportUser);
        }

        private bool SupportUserExists(int id)
        {
            return _context.SupportUser.Any(e => e.Id == id);
        }
    }
}