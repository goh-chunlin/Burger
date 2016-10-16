using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkManagement.FacebookGraph
{
    public class FacebookProfilePictureData
    {
        [JsonProperty("data")]
        public FacebookProfilePicture PictureData { get; set; }
    }
}