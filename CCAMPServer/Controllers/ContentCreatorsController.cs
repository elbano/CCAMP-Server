using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CCAMPServer.Data;
using CCAMPServerModel.Models;
using Microsoft.AspNetCore.Authorization;
using CCAMPServer.Classes;

namespace CCAMPServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentCreatorsController : ControllerBase
    {
        private readonly ContentCreatorManager _manager;

        public ContentCreatorsController(TransactionDBContext context)
        {
            _manager = new ContentCreatorManager(context, HttpContext, User);
        }

        // GET: api/ContentCreators
        [HttpGet]
        public IEnumerable<ContentCreator> GetContentCreator()
        {
            return _manager.GetContentCreator();
        }

        // GET: api/ContentCreators/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContentCreator([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contentCreator = await _manager.GetContentCreatorById(id);

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

            await _manager.SetContentCreatorState(id, contentCreator);

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

            await _manager.CreateUpdateContentCreator(contentCreator);

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

            var contentCreator = await _manager.GetContentCreatorById(id);

            if (contentCreator == null)
            {
                return NotFound();
            }

            await _manager.DeleteContentCreator(contentCreator);
            return Ok(contentCreator);
        }
    }
}