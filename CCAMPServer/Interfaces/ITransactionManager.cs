using System.Collections.Generic;
using System.Threading.Tasks;
using CCAMPServerModel.Models;

namespace CCAMPServer.Classes
{
    public interface ITransactionManager
    {
        Task AddTransaction(Transaction transaction);
        Task DeleteTransaction(int id);
        Task DeleteTransaction(Transaction transaction);
        Task<Transaction> GetTransactionById(int id);
        IEnumerable<Transaction> GetTransactions();
        Task SetTransactionState(int id, Transaction transaction);
        bool TransactionExists(int id);
    }
}