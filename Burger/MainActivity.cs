using Android.App;
using Android.Widget;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace Burger
{
	[Activity(Label = "Burger", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		private MobileServiceClient client;
		private MobileServiceUser user;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);

			client = new MobileServiceClient(this.GetString(Resource.String.BACKEND_APP_SERVICE_URL));

			Button btnLoginWithFacebook = FindViewById<Button>(Resource.Id.btnLoginWithFacebook);
			btnLoginWithFacebook.Click += async delegate { await Authenticate("Facebook"); };

			Button btnLoginWithGoogle = FindViewById<Button>(Resource.Id.btnLoginWithGoogle);
			btnLoginWithGoogle.Click += async delegate { await Authenticate("GooglePlus"); };

			Button btnLoginWithTwitter = FindViewById<Button>(Resource.Id.btnLoginWithTwitter);
			btnLoginWithTwitter.Click += async delegate { await Authenticate("Twitter"); };

			Button btnLoginWithMicrosoft = FindViewById<Button>(Resource.Id.btnLoginWithMicrosoft);
			btnLoginWithMicrosoft.Click += async delegate { await Authenticate("MicrosoftLive"); };

			LinearLayout userInfoLayout = FindViewById<LinearLayout>(Resource.Id.userInfoLayout);
			userInfoLayout.Visibility = Android.Views.ViewStates.Gone;
		}

		private async Task Authenticate(string socialNetwork)
		{
			var progressDialog = ProgressDialog.Show(this, "Please Wait", "Connecting to Social Network...", true);

			try
			{
				var socialNetworkConnection = SocialNetworkConnection.GetInstance(socialNetwork);

				user = await socialNetworkConnection.Authenticate(this, client);

				if (user != null)
				{
					TextView txtFullName = FindViewById<TextView>(Resource.Id.txtFullName);
					txtFullName.Text = socialNetworkConnection.GetUserName();

					TextView txtEmail = FindViewById<TextView>(Resource.Id.txtEmail);
					txtEmail.Text = socialNetworkConnection.GetPrimaryInfo();

					TextView txtGender = FindViewById<TextView>(Resource.Id.txtGender);
					txtGender.Text = socialNetworkConnection.GetSecondaryInfo();

					ImageView imgProfilePicture = FindViewById<ImageView>(Resource.Id.imgProfilePicture);

					imgProfilePicture.SetImageBitmap(Image.GetImageBitmapFromUrl(socialNetworkConnection.GetProfileImageUrl()));

					LinearLayout userInfoLayout = FindViewById<LinearLayout>(Resource.Id.userInfoLayout);
					userInfoLayout.Visibility = Android.Views.ViewStates.Visible;
				}

			}
			catch (Exception ex)
			{
				new AlertDialog.Builder(this)
							  .SetPositiveButton("Okai", (sender, args) => { })
							  .SetMessage(ex.Message)
							  .SetTitle("Exception!")
							  .Show();
			}

			progressDialog.Hide();
		}
	}
}

