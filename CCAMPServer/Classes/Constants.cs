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

            public const string YOUTUBE_SEARCH = "search?{0}";
        }
    }
}
