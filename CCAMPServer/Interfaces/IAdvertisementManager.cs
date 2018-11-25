using System.Collections.Generic;
using System.Threading.Tasks;
using CCAMPServerModel.Models;

namespace CCAMPServer.Classes
{
    public interface IAdvertisementManager
    {
        Task AddAdvertisement(Advertisement advertisement);
        bool AdvertisementExists(int id);
        Task DeleteAdvertisement(Advertisement advertisement);
        Task DeleteAdvertisement(int id);
        Task<Advertisement> GetAdvertisementById(int id);
        IEnumerable<Advertisement> GetAdvertisements();
        Task SetAdvertisementState(int id, Advertisement advertisement);
    }
}