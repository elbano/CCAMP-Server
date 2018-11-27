using System.Collections.Generic;
using System.Threading.Tasks;
using CCAMPServerModel.Models;

namespace CCAMPServer.Classes
{
    public interface ISupportUserManager
    {
        Task AddSupportUser(SupportUser supportUser);
        Task DeleteSupportUser(int id);
        Task DeleteSupportUser(SupportUser supportUser);
        Task<SupportUser> GetSupportUserById(int id);
        IEnumerable<SupportUser> GetSupportUsers();
        Task SetSupportUserState(int id, SupportUser supportUser);
        bool SupportUserExists(int id);
    }
}