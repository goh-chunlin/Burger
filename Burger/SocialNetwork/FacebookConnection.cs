using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using SocialNetworkManagement;
using SocialNetworkManagement.FacebookGraph;

namespace Burger
{
	public class FacebookConnection : SocialNetworkConnection
	{
		protected override MobileServiceAuthenticationProvider GetAuthenticationProvider()
		{
			return MobileServiceAuthenticationProvider.Facebook;
		}

		public override string GetPrimaryInfo()
		{
			return ((FacebookUserInfo)UserInfo).Email;
		}

		public override string GetSecondaryInfo()
		{
			return "Gender: " + ((FacebookUserInfo)UserInfo).Gender;
		}

		protected override async Task<UserInfo> GetUserInfoAsync(MobileServiceClient client)
		{
			return await client.InvokeApiAsync<FacebookUserInfo>("UserInfo", HttpMethod.Get, null);
		}

		public override string GetUserName()
		{
			return ((FacebookUserInfo)UserInfo).FullName;
		}

		public override string GetProfileImageUrl()
		{
			return ((FacebookUserInfo)UserInfo).ProfilePicure.PictureData.ImageUrl;
		}
	}
}
