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

namespace CCAMPServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private static ILogger log { get; } = ApplicationLogging.Logger.ForContext<TransactionsController>();

        // GET: api/Transactions
        [HttpGet]
        public IEnumerable<Transaction> GetTransaction()
        {
            using (var dbContext = DBContextFactory.Create(DBContextFactory.TRANSACTION))
            {
                return dbContext.Transaction;
            }
        }

        // GET: api/Transactions/5
        [HttpGet("{guid}")]
        public async Task<IActionResult> GetTransaction([FromRoute] Guid guid)
        {
            if (!ModelState.IsValid)
            {
                log.Warning("BadRequest");
                return BadRequest(ModelState);
            }

            using (var dbContext = DBContextFactory.Create(DBContextFactory.TRANSACTION))
            {
                var transaction = await dbContext.Transaction.FindAsync(guid);

                if (transaction == null)
                {
                    return NotFound();
                }

                return Ok(transaction);
            }
        }

        // PUT: api/Transactions/5
        [HttpPut("{guid}")]
        public async Task<IActionResult> PutTransaction([FromRoute] Guid guid, [FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!guid.Equals(transaction.Guid))
            {
                return BadRequest();
            }

            using (var dbContext = DBContextFactory.Create(DBContextFactory.TRANSACTION))
            {
                dbContext.Entry(transaction).State = EntityState.Modified;

                try
                {
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(guid, dbContext))
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

        // POST: api/Transactions
        [HttpPost]
        public async Task<IActionResult> PostTransaction([FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var dbContext = DBContextFactory.Create(DBContextFactory.TRANSACTION))
            {
                dbContext.Transaction.Add(transaction);
                await dbContext.SaveChangesAsync();
            }

            return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{guid}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] Guid guid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var dbContext = DBContextFactory.Create(DBContextFactory.TRANSACTION))
            {
                var transaction = await dbContext.Transaction.FindAsync(guid);
                if (transaction == null)
                {
                    return NotFound();
                }

                dbContext.Transaction.Remove(transaction);
                await dbContext.SaveChangesAsync();
            }

            return NoContent();
        }

        private bool TransactionExists(Guid guid, ApplicationDBContext dbContext)
        {
            return dbContext.Transaction.Any(e => e.Guid.Equals(guid));
        }
    }
}