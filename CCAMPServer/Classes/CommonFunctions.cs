using CCAMPServer.Data;
using CCAMPServerModel.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace CCAMPServer.Classes
{
    public static class CommonFunctions
    {
        public static string GenerateQueryString<T>(T sourceObject)
        {
            Type type = sourceObject.GetType();
            var strToReturn = new StringBuilder();
            var arrProperties = type.GetProperties();

            foreach (PropertyInfo prop in arrProperties)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    CCAMAttribute customAttribute = attr as CCAMAttribute;
                    if (customAttribute != null)
                    {
                        string customName = customAttribute.GetName();
                        bool isOptional = customAttribute.IsOPtional();

                        var currValue = prop.GetValue(sourceObject);

                        if (currValue != null)
                        {
                            strToReturn.Append($"&{customName}={currValue}");
                        }
                    }
                }

            }

            return strToReturn.ToString();
        }

        public static string FormatSearchQuery(string query)
        {
            var search = string.Format(Constants.API.YOUTUBE_SEARCH, query);
            var searchPlusKey = string.Format(Constants.API.YOUTUBE_KEY_PARAM, search, Constants.API.YOUTUBE_API_KEY);
            var request = string.Format(Constants.API.YOUTUBE_BASE_REQUEST, searchPlusKey);

            return request;
        }

        public static string GetToken(TokenRequest request, HttpContext context)
        {
            //Check DB if there is a Refresh Token otherwise request from API

            return GetTokenFromAPI(request, context);
        }

        private static string GetTokenFromDB(TokenRequest request)
        {
            try
            {
                return "ACK";
            }
            catch (Exception ex)
            {
                //Log exception and return Error
                return "ERR";
            }
        }

        public static string GetTokenRedirectURI(HttpContext context)
        {
            return $"{context.Request.Scheme}://{context.Request.Host.Host}:{context.Request.Host.Port}/api/token/result";
        }

        private static string GetTokenFromAPI(TokenRequest request, HttpContext context)
        {
            try
            {
                string redirectURI = GetTokenRedirectURI(context);
                var authorization = new Data.Authorization(redirectURI);

                ClientSecrets clientSecrets = new ClientSecrets()
                {
                    ClientId = Constants.APP.CLIENT_ID,
                    ClientSecret = Constants.APP.CLIENT_SECRET
                };

                var resul = authorization.AuthorizeAsync(clientSecrets, new[]
                        {
                            YouTubeService.Scope.Youtube,
                            YouTubeService.Scope.Youtubepartner,
                            YouTubeService.Scope.YoutubeUpload,
                            YouTubeService.Scope.YoutubepartnerChannelAudit,
                            YouTubeService.Scope.YoutubeReadonly,
                            Constants.API.Scopes.USER_INFO_EMAIL,
                            Constants.API.Scopes.USER_INFO_PROFILE
                        },
                        request.EmailAddress,
                        CancellationToken.None, null);

                //Insert into DB

            }
            catch (Exception ex)
            {
                //Log exception and return Error
                return "ERR";
            }

            return "ACK";
        }

        public static UserInfo GetAuth0UserInfobyToken(string accessToken)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var request = client.GetAsync(string.Format(Constants.API.AUTH0_ENDPOINT_USER_INFO, accessToken));
                    var response = request.Result;

                    if (response != null && response.IsSuccessStatusCode)
                    {
                        var json = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<UserInfo>(json);
                    }

                    return null;
                }
            }
            catch (Exception)
            {
                //Log exception and return Error
                return null;
            }
        }

        public static UserInfo GetGoogleUserInfobyToken(string accessToken)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var request = client.GetAsync(string.Format(Constants.API.USER_INFO_BY_ACCESS_TOKEN, accessToken));
                    var response = request.Result;

                    if (response != null && response.IsSuccessStatusCode)
                    {
                        var json = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<UserInfo>(json);
                    }

                    return null;
                }
            }
            catch (Exception)
            {
                //Log exception and return Error
                return null;
            }
        }

        public static async Task GetTokenResponseAsync(HttpRequest request,  HttpContext context, ApplicationDBContext dbContext)
        {
            try
            {
                TokenData token = new TokenData()
                {
                    code = request.Query["code"].ToString()
                };

                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"grant_type", "authorization_code"},
                    {"code", request.Query["code"].ToString()},
                    {"redirect_uri", GetTokenRedirectURI(context) }, //"https://localhost:44371/api/test/result"},
                    {"client_id", token.client_id},
                    {"client_secret", token.client_secret}
                });

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.PostAsync(token.tokenUri, content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
                    string print = "AccessToken = " + tokenResponse.AccessToken + " RefreshToken = " + tokenResponse.RefreshToken;

                    var userInfo = CommonFunctions.GetGoogleUserInfobyToken(tokenResponse.AccessToken);

                    //Call DB to store the data

                    var currToken = new Token() {
                        EmailAddress = userInfo.EmailAddress,
                        AccessToken = tokenResponse.AccessToken,
                        RefreshToken = tokenResponse.RefreshToken
                    };

                    var lstparams = new List<SqlParameter>();
                    var sqlParam = new SqlParameter("@EmailAddress", currToken.EmailAddress);
                    lstparams.Add(sqlParam);
                    sqlParam = new SqlParameter("@AccessToken", currToken.AccessToken);
                    lstparams.Add(sqlParam);
                    sqlParam = new SqlParameter("@RefreshToken", currToken.RefreshToken);
                    lstparams.Add(sqlParam);

                    string strParams = "@EmailAddress, @AccessToken, @RefreshToken";
                    string sqlQuery = string.Format(Constants.APP.StoredProcedures.FORMAT, Constants.APP.StoredProcedures.INSERT_TOKEN, strParams);

                    dbContext.Database.ExecuteSqlCommand(sqlQuery, lstparams);

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static void GetUserByUserId(string userId = "105968547501401706371")
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var request = client.GetAsync(string.Format(Constants.API.USER_INFO_BY_USER_ID, userId));
                    var response = request.Result;

                    if (response != null && response.IsSuccessStatusCode)
                    {
                        var json = response.Content.ReadAsStringAsync().Result;
                        var gt = "";
                        //return JsonConvert.DeserializeObject<UserInfo>(json);
                    }

                    //return null;
                }
            }
            catch (Exception)
            {
                //Log exception and return Error
                //return null;
            }
        }
    }
}
