using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using System.Threading.Tasks;
using burgerappService.DataObjects;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System;
using System.Text;
using System.Security.Claims;
using System.Linq;
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json;
using SocialNetworkManagement;
using SocialNetworkManagement.FacebookGraph;
using Microsoft.ApplicationInsights;
using System.Collections.Generic;
using SocialNetworkManagement.GooglePlus;
using SocialNetworkManagement.Twitter;
using SocialNetworkManagement.MicrosoftLive;
using System.Configuration;

namespace burgerappService.Controllers
{
    [MobileAppController]
    public class UserInfoController : ApiController
    {
        private TelemetryClient telemetry = new TelemetryClient();


        /// <summary>
        /// Returns the caller's info from the correct provider. The user who invokes it must be authenticated.
        /// </summary>
        /// <returns>The users info</returns>
        public async Task<UserInfo> GetUserInfo()
        {

            try
            {
                string provider = "";
                string secret;
                string accessToken = GetAccessToken(out provider, out secret);

                switch (provider)
                {
                    case "facebook":
                        using (HttpClient client = new HttpClient())
                        {
                            using (
                                HttpResponseMessage response =
                                    await
                                        client.GetAsync("https://graph.facebook.com/me?access_token=" + accessToken +
                                        "&fields=name,email,picture.type(large),cover,gender,taggable_friends.limit(4){picture.type(large),name}"))
                            {
                                return JsonConvert.DeserializeObject<FacebookUserInfo>(await response.Content.ReadAsStringAsync());
                            }
                        }
                    case "google":
                        using (HttpClient client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Authorization =
                                AuthenticationHeaderValue.Parse("Bearer " + accessToken);

                            using (
                                HttpResponseMessage response =
                                    await
                                        client.GetAsync("https://www.googleapis.com/plus/v1/people/me"))
                            {
                                var googlePlusUserInfo = JsonConvert.DeserializeObject<GooglePlusUserInfo>(await response.Content.ReadAsStringAsync());

                                googlePlusUserInfo.Email = googlePlusUserInfo.Emails.Count() > 0 ? 
                                    googlePlusUserInfo.Emails.First().EmailAddress : "";
                                googlePlusUserInfo.ProfilePicure.ImageUrl = googlePlusUserInfo.ProfilePicure.ImageUrl.Split(new char[] { '?' })[0];

                                return googlePlusUserInfo;
                            }
                        }
                    case "twitter":
                        string twitterConsumerKey = ConfigurationManager.AppSettings["TwitterConsumerKey"];
                        string twitterConsumerSecret = ConfigurationManager.AppSettings["TwitterConsumerSecret"];
                        
                        //generating signature as of https://dev.twitter.com/oauth/overview/creating-signatures
                        string nonce = GenerateNonce();
                        string s = "oauth_consumer_key=" + twitterConsumerKey + 
                            "&oauth_nonce=" + nonce + "&oauth_signature_method=HMAC-SHA1&" + 
                            "oauth_timestamp=" + DateTimeToUnixTimestamp(DateTime.Now) + 
                            "&oauth_token=" + accessToken + "&oauth_version=1.0";
                        string sign = "GET&" + Uri.EscapeDataString("https://api.twitter.com/1.1/account/verify_credentials.json") +
                                      "&" + Uri.EscapeDataString(s);
                        string sec = Uri.EscapeDataString(twitterConsumerSecret) + "&" + Uri.EscapeDataString(secret);
                        byte[] key = Encoding.ASCII.GetBytes(sec);
                        string signature = Uri.EscapeDataString(Encode(sign, key));

                        using (HttpClient client = new HttpClient())
                        {

                            client.DefaultRequestHeaders.Authorization =
                                AuthenticationHeaderValue.Parse("OAuth oauth_consumer_key =\"" + twitterConsumerKey + "\"," +
                                                                "oauth_signature_method=\"HMAC-SHA1\"," + 
                                                                "oauth_timestamp=\"" + DateTimeToUnixTimestamp(DateTime.Now) + "\"," +
                                                                "oauth_nonce=\"" + nonce + "\",oauth_version=\"1.0\"," + 
                                                                "oauth_token=\"" + accessToken + "\"," + 
                                                                "oauth_signature =\"" + signature + "\"");
                            using (
                                HttpResponseMessage response =
                                    await
                                        client.GetAsync("https://api.twitter.com/1.1/account/verify_credentials.json"))
                            {
                                var twitterUserInfo = JsonConvert.DeserializeObject<TwitterUserInfo>(await response.Content.ReadAsStringAsync());

                                twitterUserInfo.ProfilePicure = twitterUserInfo.ProfilePicure.Replace("_normal", "");

                                return twitterUserInfo;
                            }
                        }
                    case "microsoftaccount":
                        MicrosoftLiveUserInfo microsoftLiveUserInfo;
                        using (HttpClient client = new HttpClient())
                        {
                            using (
                                HttpResponseMessage response =
                                    await
                                        client.GetAsync("https://apis.live.net/v5.0/me?access_token=" +  accessToken))
                            {
                                microsoftLiveUserInfo = JsonConvert.DeserializeObject<MicrosoftLiveUserInfo>(await response.Content.ReadAsStringAsync());

                                microsoftLiveUserInfo.Email = microsoftLiveUserInfo.Emails.PreferredEmailAddress;
                            }
                        }
                        using (HttpClient client = new HttpClient())
                        {
                            using (
                                HttpResponseMessage response =
                                    await
                                        client.GetAsync("https://apis.live.net/v5.0/me/picture?suppress_redirects=true&type=large&access_token=" + accessToken))
                            {
                                var microsoftLiveUserProfilePicture = JObject.Parse(await response.Content.ReadAsStringAsync());

                                microsoftLiveUserInfo.ProfilePicure = microsoftLiveUserProfilePicture["location"].ToString();
                            }
                        }
                        return microsoftLiveUserInfo;
                }
            }
            catch (Exception ex)
            {
                telemetry.TrackException(ex);
            }

            return null;
        }

        /// <summary>
        /// Returns the access token and the provider the current user is using.
        /// </summary>
        /// <param name="provider">The provider e.g. facebook</param>
        /// <param name="secret">The user's secret when using Twitter</param>
        /// <returns>The Access Token</returns>
        private string GetAccessToken(out string provider, out string secret)
        {
            var serviceUser = User as ClaimsPrincipal;

            var identity = (ClaimsIdentity)serviceUser.Identity;
            identity.AddClaim(new Claim("scp", "email")); // This is to add scope to OAuth to retrieve email address from Google Plus

            var ident = serviceUser.FindFirst("http://schemas.microsoft.com/identity/claims/identityprovider").Value;
            string token = "";
            secret = "";
            provider = ident;
            switch (ident)
            {
                case "facebook":
                    token = Request.Headers.GetValues("X-MS-TOKEN-FACEBOOK-ACCESS-TOKEN").FirstOrDefault();
                    break;
                case "google":
                    token = Request.Headers.GetValues("X-MS-TOKEN-GOOGLE-ACCESS-TOKEN").FirstOrDefault();
                    break;
                case "microsoftaccount":
                    token = Request.Headers.GetValues("X-MS-TOKEN-MICROSOFTACCOUNT-ACCESS-TOKEN").FirstOrDefault();
                    break;
                case "twitter":
                    token = Request.Headers.GetValues("X-MS-TOKEN-TWITTER-ACCESS-TOKEN").FirstOrDefault();
                    secret = Request.Headers.GetValues("X-MS-TOKEN-TWITTER-ACCESS-TOKEN-SECRET").FirstOrDefault();
                    break;
            }
            return token;
        }

        /// <summary>
        /// Encodes to HMAC-SHA1 used by Twitter OAuth 1.1 Authentication
        /// </summary>
        /// <param name="input">The input string</param>
        /// <param name="key">The input key</param>
        /// <returns>The Base64 HMAC-SHA1 encoded string</returns>
        public static string Encode(string input, byte[] key)
        {
            HMACSHA1 myhmacsha1 = new HMACSHA1(key);
            byte[] byteArray = Encoding.ASCII.GetBytes(input);
            MemoryStream stream = new MemoryStream(byteArray);
            return Convert.ToBase64String(myhmacsha1.ComputeHash(stream));
        }

        /// <summary>
        /// Returns the Unix Timestamp of the given DateTime
        /// </summary>
        /// <param name="dateTime">The DateTime to convert</param>
        /// <returns>The Unix Timestamp</returns>
        public static long DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (long)(TimeZoneInfo.ConvertTimeToUtc(dateTime) -
                           new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

        /// <summary>
        /// Generates a random number from 123400 to int.MaxValue
        /// </summary>
        /// <returns>A random number as string</returns>
        public static string GenerateNonce()
        {
            return new Random()
                .Next(123400, int.MaxValue)
                .ToString("X");
        }

    }
}
