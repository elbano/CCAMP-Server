using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCAMPServer.Classes
{
    public static class Constants
    {
        public class API
        {
            public const string YOUTUBE_BASE_REQUEST = "https://www.googleapis.com/youtube/v3/{0}";
            public const string YOUTUBE_API_KEY = "AIzaSyAlBNZZkNZc8P3AjdiJl1LutEVxZesy4_Q";
            public const string TOKEN_URI = "https://www.googleapis.com/oauth2/v4/token";
            public const string USER_INFO = "https://www.googleapis.com/oauth2/v2/userinfo?alt=json&access_token={0}";
            
            public const string YOUTUBE_SEARCH = "search?part=snippet{0}";
            public const string YOUTUBE_KEY_PARAM = "{0}&key={1}";

            public class Scopes
            {
                public const string USER_INFO_PROFILE = "https://www.googleapis.com/auth/userinfo.profile";
                public const string USER_INFO_EMAIL = "https://www.googleapis.com/auth/userinfo.email";
            }
        }

        public class APP
        {
            public const string APPLICATION_NAME = "CCAMP client";
            public const string CLIENT_ID = "383687934789-5fc26b4cpldna5bj9c5ld6ccrg0ma88f.apps.googleusercontent.com";
            public const string CLIENT_SECRET = "2zIZAU8jjqdsDmqyrQHzQmAq";

            public class StoredProcedures
            {
                public const string FORMAT = "{0} {1}";
                public const string INSERT_TOKEN = "InsertToken";
                public const string UPDATE_TOKEN = "UpdateToken";
                public const string GET_TOKEN_DATA_BY_EMAIL = "GetTokenDataByEmailAddress";
            }
        }
    }
}
