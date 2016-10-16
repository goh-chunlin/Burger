using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using SocialNetworkManagement;
using SocialNetworkManagement.GooglePlus;

namespace Burger
{
	public class GooglePlusConnection : SocialNetworkConnection
	{
		protected override MobileServiceAuthenticationProvider GetAuthenticationProvider()
		{
			return MobileServiceAuthenticationProvider.Google;
		}

		public override string GetPrimaryInfo()
		{
			return ((GooglePlusUserInfo)UserInfo).Email;
		}

		public override string GetSecondaryInfo()
		{
			return "Gender: " + ((GooglePlusUserInfo)UserInfo).Gender;
		}

		protected override async Task<UserInfo> GetUserInfoAsync(MobileServiceClient client)
		{
			return await client.InvokeApiAsync<GooglePlusUserInfo>("UserInfo", HttpMethod.Get, null);
		}

		public override string GetUserName()
		{
			return ((GooglePlusUserInfo)UserInfo).FullName;
		}

		public override string GetProfileImageUrl()
		{
			return ((GooglePlusUserInfo)UserInfo).ProfilePicure.ImageUrl;
		}
	}
}
