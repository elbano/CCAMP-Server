using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace CCAMPServer
{
    public class AuthHelper
    {
        public static String GetTokenUserId(ClaimsPrincipal userClaim)
        {
            if (userClaim.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                return "NO-TOKEN";
            }
            else
            {
                return userClaim.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }

        public static async Task<string> GetAuth0AccessToken(ClaimsPrincipal User, HttpContext context)
        {
            string accessToken = null ;

            if (User.Identity.IsAuthenticated)
            {
                accessToken = await context.GetTokenAsync("access_token");
                // Now you can use them. For more info on when and how to use the 
                // access_token and id_token, see https://auth0.com/docs/tokens
            }

            return accessToken;
        }

    }
}
