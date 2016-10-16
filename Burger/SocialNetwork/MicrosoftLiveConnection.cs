using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using SocialNetworkManagement;
using SocialNetworkManagement.MicrosoftLive;

namespace Burger
{
	public class MicrosoftLiveConnection : SocialNetworkConnection
	{
		protected override MobileServiceAuthenticationProvider GetAuthenticationProvider()
		{
			return MobileServiceAuthenticationProvider.MicrosoftAccount;
		}

		public override string GetPrimaryInfo()
		{
			return ((MicrosoftLiveUserInfo)UserInfo).Email;
		}

		public override string GetSecondaryInfo()
		{
			return "Gender: " + (((MicrosoftLiveUserInfo)UserInfo).Gender ?? "undefined");
		}

		protected override async Task<UserInfo> GetUserInfoAsync(MobileServiceClient client)
		{
			return await client.InvokeApiAsync<MicrosoftLiveUserInfo>("UserInfo", HttpMethod.Get, null);
		}

		public override string GetUserName()
		{
			return ((MicrosoftLiveUserInfo)UserInfo).FullName;
		}

		public override string GetProfileImageUrl()
		{
			return ((MicrosoftLiveUserInfo)UserInfo).ProfilePicure;
		}
	}
}
