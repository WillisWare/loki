using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Mil.AirForce.Loki.WebApi.Models;
using Mil.AirForce.Loki.WebApi.Providers;
using Owin;

namespace Mil.AirForce.Loki.WebApi
{
	/// <summary>
	/// Startup class for initializing OAuth providers.
	/// </summary>
	public partial class Startup
	{
		/// <summary>
		/// Gets the options for OAuth.
		/// </summary>
		public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

		/// <summary>
		/// Gets the public client ID.
		/// </summary>
		public static string PublicClientId { get; private set; }

		/// <summary>
		/// Configures OAuth for the application.
		/// </summary>
		/// <param name="app">An IAppBuilder implementation to be configured.</param>
		public void ConfigureAuth(IAppBuilder app)
		{
			// Configure the db context and user manager to use a single instance per request
			app.CreatePerOwinContext(ApplicationDbContext.Create);
			app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

			// Enable the application to use a cookie to store information for the signed in user
			// and to use a cookie to temporarily store information about a user logging in with a third party login provider
			app.UseCookieAuthentication(new CookieAuthenticationOptions());
			app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

			// Configure the application for OAuth based flow
			PublicClientId = "self";
			OAuthOptions = new OAuthAuthorizationServerOptions
			{
				TokenEndpointPath = new PathString("/Token"),
				Provider = new ApplicationOAuthProvider(PublicClientId),
				AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
				AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
				// In production mode set AllowInsecureHttp = false
				AllowInsecureHttp = true
			};

			// Enable the application to use bearer tokens to authenticate users
			app.UseOAuthBearerTokens(OAuthOptions);

			// Uncomment the following lines to enable logging in with third party login providers
			//app.UseMicrosoftAccountAuthentication(
			//    clientId: "",
			//    clientSecret: "");

			//app.UseTwitterAuthentication(
			//    consumerKey: "",
			//    consumerSecret: "");

			//app.UseFacebookAuthentication(
			//    appId: "",
			//    appSecret: "");

			//app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
			//{
			//    ClientId = "",
			//    ClientSecret = ""
			//});
		}
	}
}
