using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using SocialNetworkManagement;
using SocialNetworkManagement.Twitter;

namespace Burger
{
	public class TwitterConnection : SocialNetworkConnection
	{
		protected override MobileServiceAuthenticationProvider GetAuthenticationProvider()
		{
			return MobileServiceAuthenticationProvider.Twitter;
		}

		public override string GetPrimaryInfo()
		{
			int numberOfFollowers = ((TwitterUserInfo)UserInfo).NumberOfFollowers;

			return numberOfFollowers + " Follower" + (numberOfFollowers != 1 ? "s" : "");
		}

		public override string GetSecondaryInfo()
		{
			return ((TwitterUserInfo)UserInfo).Description;
		}

		protected override async Task<UserInfo> GetUserInfoAsync(MobileServiceClient client)
		{
			return await client.InvokeApiAsync<TwitterUserInfo>("UserInfo", HttpMethod.Get, null);
		}

		public override string GetUserName()
		{
			return ((TwitterUserInfo)UserInfo).FullName;
		}

		public override string GetProfileImageUrl()
		{
			return ((TwitterUserInfo)UserInfo).ProfilePicure;
		}
	}
}
