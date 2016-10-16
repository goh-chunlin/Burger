using System;
using System.Threading.Tasks;
using Android.Content;
using Microsoft.WindowsAzure.MobileServices;
using SocialNetworkManagement;

namespace Burger
{
	public abstract class SocialNetworkConnection
	{
		public static SocialNetworkConnection GetInstance(string socialNetwork) 
		{
			return Activator.CreateInstance(Type.GetType($"Burger.{socialNetwork}Connection")) as SocialNetworkConnection;
		}

		protected UserInfo UserInfo;

		protected abstract MobileServiceAuthenticationProvider GetAuthenticationProvider();

		protected abstract Task<UserInfo> GetUserInfoAsync(MobileServiceClient client);

		public abstract string GetUserName();

		public abstract string GetPrimaryInfo();

		public abstract string GetSecondaryInfo();

		public abstract string GetProfileImageUrl();

		public async Task<MobileServiceUser> Authenticate(Context context, MobileServiceClient client) 
		{
			var user = await client.LoginAsync(context, GetAuthenticationProvider());

			if (user != null) 
			{
				UserInfo = await GetUserInfoAsync(client);
			}

			return user;
		}
	}
}
