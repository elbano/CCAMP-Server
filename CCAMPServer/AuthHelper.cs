using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CCAMPServer
{
    public class AuthHelper
    {
        public static String getTokenUserId(ClaimsPrincipal userClaim)
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
    }
}
