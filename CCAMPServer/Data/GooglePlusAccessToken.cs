using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCAMPServer.Data
{
    public class GooglePlusAccessToken: GoogleAuthorizationCodeFlow
    {
        public GooglePlusAccessToken(Initializer initializer)
            : base(initializer) { }

        public override AuthorizationCodeRequestUrl
                       CreateAuthorizationCodeRequest(string redirectUri)
        {
            return base.CreateAuthorizationCodeRequest(Authorization.RedirectUri);
        }
    }
}
