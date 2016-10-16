# Burger
Xamarin app demonstrating the social network authentication and logged-in user info retrieval with Microsoft Mobile App Service.

## Project Objectives
- Inspired by Ben Ishiyama-Levy's talk at Microsoft Singapore about authenticating and authorizing users with Microsoft Azure Mobile Apps;
- Understand how to login user to an Xamarin app via social network such as Facebook, Google+, Twitter, and Microsoft Live;
- Learn the deployment of Microsoft Azue Mobile Apps;
- Get updated with the new Mobile Apps after [Microsoft plans to kill Azure Mobile Services in December 2016](http://venturebeat.com/2016/05/10/microsoft-killing-azure-mobile-services-in-december-will-migrate-sites-to-app-service-starting-september-1/).

## Technologies Used
- Xamarin.Android
- Azure App Service: Mobile App
- Facebook Graph
- Google+ APIs
- OAuth Scope
- Twitter
- Microsoft Live
- Azure Application Insights

![Facebook Login](github-images/facebook-login.png?raw=true) ![Google+ Login](github-images/google-plus-login.png?raw=true) ![Twitter Login](github-images/twitter-login.png?raw=true) ![Microsoft Live Login](github-images/microsoft-live-login.png?raw=true)

## Fields to Customize
- BACKEND_APP_SERVICE_URL: Please setup one Azure App Service: Mobile App to get its URL. Put the URL in Burger/Resources/values.ApiKeys.xml;
- TwitterConsumerKey: Only applicable if the app is setup to allow logging in via Twitter. Put the value from Twitter Developer in burgerappService/web.config.
- TwitterConsumerSecret: Only applicable if the app is setup to allow logging in via Twitter. Put the value from Twitter Developer in burgerappService/web.config.
- InstrumentationKey: Only applicable if the app is using Azure Application Insights. Put the value in burgerappService/ApplicationInsights.config.

## References
- [Azure Mobile App Service - Get personal info of authenticated users](http://social.technet.microsoft.com/wiki/contents/articles/34290.azure-mobile-app-service-get-personal-info-of-authenticated-users.aspx)
- [Google+ Platform: Authorizing API requests](https://developers.google.com/+/web/api/rest/oauth)
- [OAuth2.0 Playground](https://developers.google.com/oauthplayground/)
- [Authentication and authorization in Azure App Service](https://azure.microsoft.com/en-us/documentation/articles/app-service-authentication-overview/)