using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkManagement.GooglePlus
{
	public class GooglePlusUserInfo : UserInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("displayName")]
        public string FullName { get; set; }

        [JsonProperty("image")]
        public GooglePlusProfilePicture ProfilePicure { get; set; }

		[JsonProperty("emails")]
		public IEnumerable<GooglePlusProfileEmail> Emails { get; set; }

		public string Email { get; set; }

		[JsonProperty("gender")]
		public string Gender { get; set; }
    }
}
