using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCAMPServer.Classes
{
    public class UserInfo
    {
        [JsonProperty("sub")]
        public string Sub { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string EmailAddress { get; set; }

        [JsonProperty("given_name")]
        public string GivenName { get; set;  }

        [JsonProperty("family_name")]
        public string FamilyName { get; set; }

        [JsonProperty("nickname")]
        public string NickName { get; set; }


    }
}
