using CCAMPServer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCAMPServer.Data
{
    public class TokenData
    {
        #region Properties

        public string tokenUri { get; set; }

        public string client_id { get; set; }

        public string client_secret { get; set; }

        public string code { get; set; }

        #endregion

        #region Constructors

        public TokenData()
        {
            client_id = Constants.APP.CLIENT_ID;
            client_secret = Constants.APP.CLIENT_SECRET;
            tokenUri = Constants.API.TOKEN_URI;
        }

        #endregion
    }
}
