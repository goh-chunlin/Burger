using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkManagement.MicrosoftLive
{
	public class MicrosoftLiveUserInfo : UserInfo
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string FullName { get; set; }

		public string ProfilePicure { get; set; }

		[JsonProperty("emails")]
		public MicrosoftLiveEmails Emails { get; set; }

		public string Email { get; set; }

		[JsonProperty("gender")]
		public string Gender { get; set; }
	}
}
