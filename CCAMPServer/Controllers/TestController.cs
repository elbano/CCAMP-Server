using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Newtonsoft.Json;
using System.Net.Http;
using CCAMPServer.Data;
using System.Threading;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CCAMPServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        private static ILogger log { get; } = ApplicationLogging.Logger.ForContext<TestController>();
        private string client_id = "383687934789-5fc26b4cpldna5bj9c5ld6ccrg0ma88f.apps.googleusercontent.com";
        private string client_secret = "2zIZAU8jjqdsDmqyrQHzQmAq";
        private string googleplus_redirect_url = "https://developers.google.com/oauthplayground";
        private string api_key = "AIzaSyAlBNZZkNZc8P3AjdiJl1LutEVxZesy4_Q";
        private string aplication_name = "CCAMP client";
        public static string RedirectUri;



        // GET: api/<controller>
        [HttpGet, AllowAnonymous]
        public IEnumerable<string> Get()
        {
            log.Information("TEST");
            return new string[] { "value1", "value2" };
        }

        [HttpGet("token"), AllowAnonymous]
        public IEnumerable<string> GetToken()
        {
            Task<UserCredential> resul = null;
            try
            {
                Data.Authorization bo = new Data.Authorization();
                ClientSecrets cli = new ClientSecrets() { ClientId = client_id, ClientSecret = client_secret };
                resul = bo.AuthorizeAsync(cli, new[]
                        {
                YouTubeService.Scope.Youtube,
                YouTubeService.Scope.Youtubepartner,
                YouTubeService.Scope.YoutubeUpload,
                YouTubeService.Scope.YoutubepartnerChannelAudit,
                YouTubeService.Scope.YoutubeReadonly
                        },
                        "victorinox88@gmail.com",
                        CancellationToken.None,
                        new FileDataStore(this.GetType().ToString()));



            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message, ex);
                Response.Redirect("index.aspx");
            }

            return new string[] { "value1", "value2" };
        }



        [HttpGet("result"), AllowAnonymous]
        public async Task<string> GetResultAsync()
        {
            try
            {

                TokenData token = new TokenData()
                {
                    client_id = "383687934789-5fc26b4cpldna5bj9c5ld6ccrg0ma88f.apps.googleusercontent.com",
                    client_secret = "2zIZAU8jjqdsDmqyrQHzQmAq",
                    code = Request.Query["code"].ToString(),
                    tokenUri = "https://www.googleapis.com/oauth2/v4/token"
                };

                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"grant_type", "authorization_code"},
                    {"code", Request.Query["code"].ToString()},
                    {"redirect_uri", "https://localhost:44371/api/test/result"},
                    {"client_id", token.client_id},
                    {"client_secret", token.client_secret}
                });

                HttpClient httpClient = new HttpClient();


                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.PostAsync(token.tokenUri, content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

                    return tokenResponse.AccessToken;
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        [HttpGet("video/{id}"), AllowAnonymous]
        public async Task<string> GetVideoAsync(string id)
        {
            string apiKey = "AIzaSyAlBNZZkNZc8P3AjdiJl1LutEVxZesy4_Q";
            string path = string.Format("https://www.googleapis.com/youtube/v3/videos?id={0}&key={1}&part=snippet,contentDetails,statistics,status", id, apiKey);
            HttpClient httpClient = new HttpClient();

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(path);
                var responseContent = await response.Content.ReadAsStringAsync();
               
                return responseContent;
            }
                
        }

        [HttpGet("videoToken/{id}/{token}")]
        public async Task<string> GetVideToken(string id,string token)
        {
            string apiKey = "AIzaSyAlBNZZkNZc8P3AjdiJl1LutEVxZesy4_Q";
            string path = string.Format("https://www.googleapis.com/youtube/v3/videos?id={0}&part=snippet,contentDetails,statistics,status&mine=true&access_token={1}", id, token);
            HttpClient httpClient = new HttpClient();

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(path);
                var responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }

        }

        // GET api/<controller>/5
        [HttpGet("{id}"), AllowAnonymous]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
