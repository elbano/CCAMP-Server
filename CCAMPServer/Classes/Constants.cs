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

            public const string YOUTUBE_SEARCH = "search?part=snippet{0}";
        }

        public class SearchParameters
        {

            //THESE PARAMETERS ARE OPTIONAL BUT LIMITED TO ONE (1)
            //https://developers.google.com/youtube/v3/docs/search/list#usage
            public const string FOR_CONTENT_OWNER_PARAMETER = "forContentOwner"; //Bool 
            public const string FOR_DEVELOPER_PARAMETER = "forDeveloper"; //Bool 
            public const string FOR_MINE_PARAMETER = "forMine"; //Bool 
            public const string RELATED_TO_VIDEO_ID_PARAMETER = "relatedToVideoId";


            //OPTIONAL PARAMETERS TO DEFINE THE SEARCH
            public const string CHANNEL_ID_PARAMETER = "channelId";
            public const string CHANNEL_TYPE_PARAMETER = "channelType";
            public const string EVEN_TYPE_PARAMETER = "eventType";
            public const string ORDER_PARAMETER = "order";
            public const string LOCATION_PARAMETER = "location";
            public const string LOCATION_RADIUS_PARAMETER = "locationRadius";
            public const string MAX_RESULTS_PARAMETER = "maxResults";
            public const string PAGE_TOKEN_PARAMETER = "pageToken";
            public const string PUBLISHED_AFTER_PARAMETER = "publishedAfter";
            public const string PUBLISHED_BEFORE_PARAMETER = "publishedBefore";
            public const string QUERY_PARAMETER = "q";
            public const string REGION_CODE_PARAMETER = "regionCode";
            public const string RELEVANCE_LANGUAGE_PARAMETER = "relevanceLanguage";
            public const string SAFE_SEARCH_PARAMETER = "safeSearch";
            public const string TOPIC_ID_PARAMETER = "topicId";
            public const string TYPE_PARAMETER = "type";
            public const string VIDEO_CAPTION_PARAMETER = "videoCaption";
            public const string VIDEO_CATEGORY_ID_PARAMETER = "videoCategoryId";
            public const string VIDEO_DEFINITION_PARAMETER = "videoDefinition";
            public const string VIDEO_DIMENSION_PARAMETER = "videoDimension";
            public const string VIDEO_DURATION_PARAMETER = "videoDuration";
            public const string VIDEO_EMBEDDABLE_PARAMETER = "videoEmbeddable";
            public const string VIDEO_LICENCE_PARAMETER = "videoLicense";
            public const string VIDEO_SYNDICATED_PARAMETER = "videoSyndicated";
            public const string VIDEOTYPE_PARAMETER = "videoType";

            public class Order
            {
                public const string DATE = "date";
                public const string RATING = "rating";
                public const string RELEVANCE = "relevance";
                public const string TITLE = "title";
                public const string VIDEOCOUNT = "videoCount";
                public const string VIEWCOUNT = "viewCount";
            }

            public class EventType
            {
                public const string COMPLETED = "completed";
                public const string LIVE = "live";
                public const string UPCOMING = "upcoming";
            }

            public class ChannelType
            {
                public const string ANY = "any";
                public const string SHOW = "show";
            }

            public class SafeSearch
            {
                public const string MODERATE = "moderate";
                public const string NONE = "none";
                public const string STRICT = "strict";
            }

            public class Type
            {
                public const string CHANNEL = "channel";
                public const string PLAYLIST = "playlist";
                public const string VIDEO = "video";
            }
            public class VideoCaption
            {
                public const string ANY = "any";
                public const string CLOSED_CAPTION = "closedCaption";
                public const string NONE = "none";
            }

            public class VideoDefinition
            {
                public const string ANY = "any";
                public const string HIGH = "high";
                public const string STANDART = "standard";
            }
            public class VideoDimension
            {
                public const string ANY = "any";
                public const string TWOD_D = "2d";
                public const string THREE_D = "3d";
            }

            public class VideoDuration
            {
                public const string ANY = "any";
                public const string LONG = "long";
                public const string MEDIMUM = "medium";
                public const string SHORT = "short";
            }

            public class VideoEmbeddable
            {
                public const string ANY = "any";
                public const string TRUE = "true";
            }

            public class videoLicense
            {
                public const string ANY = "any";
                public const string CREATIVE_COMMON = "creativeCommon ";
                public const string YOUTUBE = "youtube";
            }

            public class VideoSyndicated
            {
                public const string ANY = "any";
                public const string TRUE = "true";
            }

            public class VideoType
            {
                public const string ANY = "any";
                public const string EPISODE = "episode";
                public const string MOVIE = "movie";
            }

        }
    }
}
