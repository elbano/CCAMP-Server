using CCAMPServer.Data;
using CCAMPServerModel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CCAMPServer.Classes
{
    public class TransactionManager : BaseManager, ITransactionManager
    {
        #region Constructor

        public TransactionManager(TransactionDBContext context, HttpContext httpContext, ClaimsPrincipal user) :
            base(context, httpContext, user)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>Gets the Transactions.</summary>
        /// <returns></returns>
        public IEnumerable<Transaction> GetTransactions()
        {
            return _context.Transaction;
        }

        /// <summary>Gets the Transaction by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Transaction> GetTransactionById(int id)
        {
            return await _context.Transaction.FindAsync(id);
        }


        /// <summary>Deletes the Transaction.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task DeleteTransaction(int id)
        {
            var Transaction = await GetTransactionById(id);
            if (Transaction != null)
            {
                await DeleteTransaction(Transaction);
            }
        }

        /// <summary>Sets the state of the Transaction.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="transaction">The Transaction.</param>
        /// <returns></returns>
        public async Task SetTransactionState(int id, Transaction transaction)
        {
            try
            {
                _context.Entry(transaction).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return;
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>Adds the Transaction.</summary>
        /// <param name="transaction">The Transaction.</param>
        /// <returns></returns>
        public async Task AddTransaction(Transaction transaction)
        {
            _context.Transaction.Add(transaction);
            await _context.SaveChangesAsync();
        }

        /// <summary>Deletes the Transaction.</summary>
        /// <param name="transaction">The Transaction.</param>
        /// <returns></returns>
        public async Task DeleteTransaction(Transaction transaction)
        {
            _context.Transaction.Remove(transaction);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Transactions exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public bool TransactionExists(int id)
        {
            return _context.Transaction.Any(e => e.Id == id);
        }


        #endregion
    }
}
