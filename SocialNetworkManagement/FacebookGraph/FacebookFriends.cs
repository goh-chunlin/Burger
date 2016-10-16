using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkManagement.FacebookGraph
{
    public class FacebookFriends
    {
        //[JsonProperty("summary")]
        //public FacebookFriendsSummary Summary { get; set; }

        [JsonProperty("data")]
        public IEnumerable<FacebookFriend> FriendDetails { get; set; }
    }
}