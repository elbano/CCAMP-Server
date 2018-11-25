using System.Threading.Tasks;
using CCAMPServerModel.Models;
using Microsoft.AspNetCore.Mvc;

namespace CCAMPServer.Classes
{
    public interface IDealsManager
    {
        Task CreateUpdateDeal(Deal deal);
        bool DealExists(int id);
        Task DeleteDeal(Deal deal);
        Task DeleteDealById(int id);
        Task<Deal> GetDealById(int id);
        JsonResult GetUserDeals(int status);
        Task SetDealState(int id, Deal deal);
    }
}