using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkManagement.FacebookGraph
{
    public class FacebookProfilePicture
    {
        [JsonProperty("is_silhouette")]
        public bool isSilhouette { get; set; }

        [JsonProperty("url")]
        public string ImageUrl { get; set; }
    }
}