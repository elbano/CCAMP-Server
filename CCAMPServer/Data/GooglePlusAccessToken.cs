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
        private string _redirectUri;

        public GooglePlusAccessToken(Initializer initializer, string redirectUri)
            : base(initializer) {

            _redirectUri = redirectUri;
        }

        public override AuthorizationCodeRequestUrl
                       CreateAuthorizationCodeRequest(string redirectUri)
        {
            return base.CreateAuthorizationCodeRequest(_redirectUri);
        }
    }
}
