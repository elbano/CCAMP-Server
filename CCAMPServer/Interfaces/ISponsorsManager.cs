using System.Collections.Generic;
using System.Threading.Tasks;
using CCAMPServerModel.Models;

namespace CCAMPServer.Classes
{
    public interface ISponsorsManager
    {
        Task AddSponsor(Sponsor sponsor);
        Task DeleteSponsor(int id);
        Task DeleteSponsor(Sponsor sponsor);
        Task<Sponsor> GetSponsorById(int id);
        IEnumerable<Sponsor> GetSponsors();
        Task SetSponsorState(int id, Sponsor sponsor);
        bool SponsorExists(int id);
    }
}