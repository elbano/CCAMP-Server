using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CCAMPServer.Classes;
using System.Net.Http;

namespace CCAMPServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : Controller
    {
        [HttpGet]
        public string Search()
        {
            using (var client = new HttpClient())
            {
                var search = string.Format(Constants.API.YOUTUBE_SEARCH, $"&q=YouTube+Data+API&type=video&videoCaption=closedCaption&key={Constants.API.YOUTUBE_API_KEY}");
                var request = string.Format(Constants.API.YOUTUBE_BASE_REQUEST,search);


                var response = client.GetAsync(request);
                var responseContent = response.Result.Content.ReadAsStringAsync();

                return responseContent.Result.ToString();

            }
        }

    }
}