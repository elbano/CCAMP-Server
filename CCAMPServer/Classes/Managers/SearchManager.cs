using CCAMPServer.Data;
using CCAMPServerModel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CCAMPServer.Classes
{
    public class SearchManager : BaseManager, ISearchManager
    {
        #region Constructor

        public SearchManager(TransactionDBContext context, HttpContext httpContext, ClaimsPrincipal user) :
            base(context, httpContext, user)
        {
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Searches the specified query parameters.
        /// </summary>
        /// <param name="queryParams">The query parameters.</param>
        /// <returns></returns>
        public string SearchByQueryParams(string queryParams)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(CommonFunctions.FormatSearchQuery(queryParams.ToString()));
                var responseContent = response.Result.Content.ReadAsStringAsync();

                return responseContent.Result.ToString();
            }
        }

        /// <summary>
        /// Gets the search parameters.
        /// </summary>
        /// <returns></returns>
        public string GetSearchParameters()
        {
            return JsonConvert.SerializeObject(new SearchQueryParameters(), Formatting.Indented, new ConverterClassDefToJSON(typeof(SearchQueryParameters)));
        }

        #endregion
    }
}
