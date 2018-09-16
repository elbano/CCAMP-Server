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
    public class ContentCreatorsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ContentCreatorsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/ContentCreators
        [HttpGet]
        public IEnumerable<ContentCreator> GetContentCreator()
        {
            return _context.ContentCreator;
        }

        // GET: api/ContentCreators/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContentCreator([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contentCreator = await _context.ContentCreator.FindAsync(id);

            if (contentCreator == null)
            {
                return NotFound();
            }

            return Ok(contentCreator);
        }

        // PUT: api/ContentCreators/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContentCreator([FromRoute] int id, [FromBody] ContentCreator contentCreator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contentCreator.Id)
            {
                return BadRequest();
            }

            _context.Entry(contentCreator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentCreatorExists(id))
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

        // POST: api/ContentCreators
        [HttpPost]
        public async Task<IActionResult> PostContentCreator([FromBody] ContentCreator contentCreator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ContentCreator.Add(contentCreator);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContentCreator", new { id = contentCreator.Id }, contentCreator);
        }

        // DELETE: api/ContentCreators/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContentCreator([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contentCreator = await _context.ContentCreator.FindAsync(id);
            if (contentCreator == null)
            {
                return NotFound();
            }

            _context.ContentCreator.Remove(contentCreator);
            await _context.SaveChangesAsync();

            return Ok(contentCreator);
        }

        private bool ContentCreatorExists(int id)
        {
            return _context.ContentCreator.Any(e => e.Id == id);
        }
    }
}