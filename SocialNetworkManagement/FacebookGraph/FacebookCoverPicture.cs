using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkManagement.FacebookGraph
{
    public class FacebookCoverPicture
    {
        [JsonProperty("source")]
        public string ImageUrl { get; set; }
    }
}