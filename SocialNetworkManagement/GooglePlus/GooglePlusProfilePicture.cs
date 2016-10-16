using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkManagement.GooglePlus
{
    public class GooglePlusProfilePicture
    {
        [JsonProperty("isDefault")]
        public bool isSilhouette { get; set; }

        [JsonProperty("url")]
        public string ImageUrl { get; set; }
    }
}
