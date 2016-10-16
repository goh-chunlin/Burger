using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkManagement.FacebookGraph
{
    public abstract class FacebookPerson : UserInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string FullName { get; set; }

        [JsonProperty("picture")]
        public FacebookProfilePictureData ProfilePicure { get; set; }
    }
}