using System.Collections.Generic;
using System.Threading.Tasks;
using CCAMPServerModel.Models;

namespace CCAMPServer.Classes
{
    public interface IContentCreatorManager
    {
        bool ContentCreatorExists(int id);
        Task CreateUpdateContentCreator(ContentCreator contentCreator = null, string authUserId = null);
        Task DeleteContentCreator(ContentCreator contentCreator);
        IEnumerable<ContentCreator> GetContentCreator();
        Task<ContentCreator> GetContentCreatorById(int id);
        Task SetContentCreatorState(int id, ContentCreator contentCreator);
    }
}