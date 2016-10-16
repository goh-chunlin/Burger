using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkManagement.Twitter
{
	public class TwitterUserInfo : UserInfo
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string FullName { get; set; }

		[JsonProperty("profile_image_url_https")]
		public string ProfilePicure { get; set; }

		[JsonProperty("followers_count")]
		public int NumberOfFollowers { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }
	}
}
