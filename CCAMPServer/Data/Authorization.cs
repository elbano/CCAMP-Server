using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CCAMPServer.Data
{
    public class Authorization: GoogleWebAuthorizationBroker
    {
        public readonly string RedirectUri;

        public Authorization(string redirectUrl)
        {
            RedirectUri = redirectUrl;//"https://localhost:44371/api/test/result";
        }


        public new async Task<UserCredential> AuthorizeAsync(
            ClientSecrets clientSecrets,
            IEnumerable<string> scopes,
            string user,
            CancellationToken taskCancellationToken,
            IDataStore dataStore = null)
        {
            
            var initializer = new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = clientSecrets,
            };
            return await AuthorizeAsyncCore(initializer, scopes, user,
                taskCancellationToken, dataStore).ConfigureAwait(false);
        }

        private async Task<UserCredential> AuthorizeAsyncCore(
            GoogleAuthorizationCodeFlow.Initializer initializer,
            IEnumerable<string> scopes,
            string user,
            CancellationToken taskCancellationToken,
            IDataStore dataStore)
        {
            initializer.Scopes = scopes;
            initializer.DataStore = dataStore ?? new FileDataStore(Folder);
            var flow = new GooglePlusAccessToken(initializer, RedirectUri);
            return await new AuthorizationCodeInstalledApp(flow,
                new LocalServerCodeReceiver())
                .AuthorizeAsync(user, taskCancellationToken).ConfigureAwait(false);
        }

    }
}
