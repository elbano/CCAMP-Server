using CCAMPServer.Data;
using CCAMPServerModel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCAMPServer.Classes;

namespace CCAMPServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentsController : ControllerBase
    {
        private readonly ContentsManager _manager;

        public ContentsController(TransactionDBContext context)
        {
            _manager = new ContentsManager(context, HttpContext,User);
        }

        //// GET: api/Contents
        //[HttpGet]
        //public IEnumerable<Content> GetContent()
        //{
        //    return _context.Content;
        //}

        //// GET: api/Contents/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetContent([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var content = await _context.Content.FindAsync(id);

        //    if (content == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(content);
        //}

        //// PUT: api/Contents/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutContent([FromRoute] int id, [FromBody] Content content)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != content.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(content).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ContentExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Contents
        //[HttpPost]
        //public async Task<IActionResult> PostContent([FromBody] Content content)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Content.Add(content);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetContent", new { id = content.Id }, content);
        //}

        //// DELETE: api/Contents/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteContent([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var content = await _context.Content.FindAsync(id);
        //    if (content == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Content.Remove(content);
        //    await _context.SaveChangesAsync();

        //    return Ok(content);
        //}

        //private bool ContentExists(int id)
        //{
        //    return _context.Content.Any(e => e.Id == id);
        //}
    }
}