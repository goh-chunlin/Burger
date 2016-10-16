using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetworkManagement.GooglePlus;

namespace SocialNetworkManagement.FacebookGraph
{
    public class FacebookUserInfo : FacebookPerson
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("cover")]
        public FacebookCoverPicture CoverPicture { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("taggable_friends")]
        public FacebookFriends Friends { get; set; }
    }
}