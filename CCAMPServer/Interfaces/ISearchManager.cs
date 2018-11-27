namespace CCAMPServer.Classes
{
    public interface ISearchManager
    {
        string GetSearchParameters();
        string SearchByQueryParams(string queryParams);
    }
}