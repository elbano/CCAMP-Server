using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCAMPServer.Classes
{
    public class SearchParameters
    {
        #region OneOrNonParamters
        [CCAMAttribute("forContentOwner")]
        public bool? forContentOwner { get; set; } = null;

        [CCAMAttribute("forDeveloper")]
        public bool? forDeveloper { get; set; } = null;

        [CCAMAttribute("forMine")]
        public bool? forMine { get; set; } = null;

        [CCAMAttribute("relatedToVideoId")]
        public string relatedToVideoId { get; set; } = null;

        #endregion

        #region OptionalParameters
        [CCAMAttribute("channelId")]
        public string channelId { get; set; } = null;

        [CCAMAttribute("channelType")]
        public string channelType { get; set; } = null;

        [CCAMAttribute("eventType")]
        public string eventType { get; set; } = null;

        [CCAMAttribute("location")]
        public string location { get; set; } = null;

        [CCAMAttribute("locationRadius")]
        public string locationRadius { get; set; } = null;

        [CCAMAttribute("maxResults")]
        public int maxResults { get; set; } = 5;

        [CCAMAttribute("order")]
        public string order { get; set; } = null;

        [CCAMAttribute("pageToken")]
        public string pageToken { get; set; } = null;

        [CCAMAttribute("publishedAfter")]
        public DateTime? publishedAfter { get; set; } = null;

        [CCAMAttribute("publishedBefore")]
        public DateTime? publishedBefore { get; set; } = null;

        [CCAMAttribute("q")]
        public string q { get; set; } = null;

        [CCAMAttribute("regionCode")]
        public string regionCode { get; set; } = null;

        [CCAMAttribute("relevanceLanguage")]
        public string relevanceLanguage { get; set; } = null;

        [CCAMAttribute("safeSearch")]
        public string safeSearch { get; set; } = null;

        [CCAMAttribute("topicId")]
        public string topicId { get; set; } = null;

        [CCAMAttribute("type")]
        public string type { get; set; } = null;

        [CCAMAttribute("videoCaption")]
        public string videoCaption { get; set; } = null;

        [CCAMAttribute("videoCategoryId")]
        public string videoCategoryId { get; set; } = null;

        [CCAMAttribute("videoDefinition")]
        public string videoDefinition { get; set; } = null;

        [CCAMAttribute("videoDimension")]
        public string videoDimension { get; set; } = null;

        [CCAMAttribute("videoDuration")]
        public string videoDuration { get; set; } = null;

        [CCAMAttribute("videoEmbeddable")]
        public string videoEmbeddable { get; set; } = null;

        [CCAMAttribute("videoLicense")]
        public string videoLicense { get; set; } = null;

        [CCAMAttribute("videoSyndicated")]
        public string videoSyndicated { get; set; } = null;

        [CCAMAttribute("videoType")]
        public string videoType { get; set; } = null;

        #endregion

        #region Properties

        public override string ToString()
        {
            return CommonFunctions.GenerateQueryString<SearchParameters>(this);
        }

        #endregion
    }
}
