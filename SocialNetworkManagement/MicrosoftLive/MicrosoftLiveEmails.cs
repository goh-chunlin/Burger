using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkManagement.MicrosoftLive
{
	public class MicrosoftLiveEmails
	{
		[JsonProperty("preferred")]
		public string PreferredEmailAddress { get; set; }

		[JsonProperty("account")]
		public string AccountEmailAddress { get; set; }

		[JsonProperty("personal")]
		public string PersonalEmailAddress { get; set; }

		[JsonProperty("business")]
		public string BusinessEmailAddress { get; set; }
	}
}
