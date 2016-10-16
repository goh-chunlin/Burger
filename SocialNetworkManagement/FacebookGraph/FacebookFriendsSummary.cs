using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkManagement.FacebookGraph
{
    public class FacebookFriendsSummary
    {
        [JsonProperty("total_count")]
        public string TotalNumberOfFriends { get; set; }
    }
}