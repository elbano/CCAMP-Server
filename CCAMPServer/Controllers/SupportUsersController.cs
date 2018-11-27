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
    public class SupportUsersController : ControllerBase
    {
        private readonly SupportUserManager _manager;

        public SupportUsersController(TransactionDBContext context)
        {
            _manager = new SupportUserManager(context, HttpContext, User);
        }

        // GET: api/SupportUsers
        [HttpGet]
        public IEnumerable<SupportUser> GetSupportUser()
        {
            return _manager.GetSupportUsers();
        }

        // GET: api/SupportUsers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupportUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var supportUser = await _manager.GetSupportUserById(id);

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

            await _manager.SetSupportUserState(id, supportUser);

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

            await _manager.AddSupportUser(supportUser);

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

            var supportUser = await _manager.GetSupportUserById(id);
            if (supportUser == null)
            {
                return NotFound();
            }

            await _manager.DeleteSupportUser(supportUser);

            return Ok(supportUser);
        }
    }
}