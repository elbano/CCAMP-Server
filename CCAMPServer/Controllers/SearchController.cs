using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CCAMPServer.Classes;
using System.Net.Http;
using Newtonsoft.Json;

namespace CCAMPServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : Controller
    {
        [HttpPost]
        public string Search([FromBody]SearchParameters queryParams )
        {
            if (string.IsNullOrWhiteSpace(queryParams.ToString()))
                return null;

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(CommonFunctions.FormatSearchQuery(queryParams.ToString()));
                var responseContent = response.Result.Content.ReadAsStringAsync();

                return responseContent.Result.ToString();
            }

        }

        [HttpGet("GetSearchParameters")]
        public string GetSearchParameters()
        {
            return JsonConvert.SerializeObject(new SearchQueryParameters(), Formatting.Indented, new ConverterClassDefToJSON(typeof(SearchQueryParameters)));
        }

    }
}