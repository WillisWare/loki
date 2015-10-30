using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Mil.AirForce.Loki.WebApi.Models;
using Mil.AirForce.Loki.WebApi.Providers;
using Mil.AirForce.Loki.WebApi.Results;

namespace Mil.AirForce.Loki.WebApi.Controllers
{
	/// <summary>
	/// User account controller.
	/// </summary>
	[Authorize]
	[RoutePrefix("api/Account")]
	public class AccountController : ApiController
	{
		private const string LOCAL_LOGIN_PROVIDER = "Local";

		private ApplicationUserManager _userManager;

		/// <summary>
		/// Default constructor. Initializes an instance of this class.
		/// </summary>
		public AccountController()
		{
		}

		/// <summary>
		/// Overloaded. Initializes an instance of this class.
		/// </summary>
		/// <param name="userManager">An ApplicationUserManager instance for managing user accounts.</param>
		/// <param name="accessTokenFormat">An ISecureDataFormat implementation containing the access token format.</param>
		public AccountController(ApplicationUserManager userManager,
			ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
		{
			UserManager = userManager;
			AccessTokenFormat = accessTokenFormat;
		}

		/// <summary>
		/// Gets the user manager instance.
		/// </summary>
		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}

		/// <summary>
		/// Gets the access token format.
		/// </summary>
		public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

		/// <summary>
		/// GET method for user account info.
		/// </summary>
		/// <returns>A UserInfoViewModel instance containing the view data.</returns>
		[HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
		[Route("UserInfo")]
		public UserInfoViewModel GetUserInfo()
		{
			ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

			return new UserInfoViewModel
			{
				Email = User.Identity.GetUserName(),
				HasRegistered = externalLogin == null,
				LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
			};
		}

		/// <summary>
		/// POST method for logging out.
		/// </summary>
		/// <returns>An IHttpActionResult implementation containing the view data.</returns>
		[Route("Logout")]
		public IHttpActionResult Logout()
		{
			Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
			return Ok();
		}

		/// <summary>
		/// GET method for managing user account info.
		/// </summary>
		/// <param name="returnUrl">A String containing the return URL.</param>
		/// <param name="generateState">A Boolean value indicating whether or not to generate statefulness.</param>
		/// <returns>A ManageInfoViewModel instance containing the view data.</returns>
		[Route("ManageInfo")]
		public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
		{
			IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

			if (user == null)
			{
				return null;
			}

			List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

			foreach (IdentityUserLogin linkedAccount in user.Logins)
			{
				logins.Add(new UserLoginInfoViewModel
				{
					LoginProvider = linkedAccount.LoginProvider,
					ProviderKey = linkedAccount.ProviderKey
				});
			}

			if (user.PasswordHash != null)
			{
				logins.Add(new UserLoginInfoViewModel
				{
					LoginProvider = LOCAL_LOGIN_PROVIDER,
					ProviderKey = user.UserName,
				});
			}

			return new ManageInfoViewModel
			{
				LocalLoginProvider = LOCAL_LOGIN_PROVIDER,
				Email = user.UserName,
				Logins = logins,
				ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
			};
		}

		/// <summary>
		/// POST method for changing an account password.
		/// </summary>
		/// <param name="model">A ChangePasswordBindingModel instance containing the password change.</param>
		/// <returns>An IHttpActionResult implementation containing the view data.</returns>
		[Route("ChangePassword")]
		public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
				model.NewPassword);

			if (!result.Succeeded)
			{
				return GetErrorResult(result);
			}

			return Ok();
		}

		/// <summary>
		/// POST method for setting an account password.
		/// </summary>
		/// <param name="model">A SetPasswordBindingModel instance containing the password.</param>
		/// <returns>An IHttpActionResult implementation containing the view data.</returns>
		[Route("SetPassword")]
		public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

			if (!result.Succeeded)
			{
				return GetErrorResult(result);
			}

			return Ok();
		}

		/// <summary>
		/// POST method for adding an external (OAuth) login.
		/// </summary>
		/// <param name="model">An AddExternalLoginBindingModel instance containing the login.</param>
		/// <returns>An IHttpActionResult implementation containing the view data.</returns>
		[Route("AddExternalLogin")]
		public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

			AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

			if (ticket == null || ticket.Identity == null || (ticket.Properties != null
				&& ticket.Properties.ExpiresUtc.HasValue
				&& ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
			{
				return BadRequest("External login failure.");
			}

			ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

			if (externalData == null)
			{
				return BadRequest("The external login is already associated with an account.");
			}

			IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
				new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

			if (!result.Succeeded)
			{
				return GetErrorResult(result);
			}

			return Ok();
		}

		/// <summary>
		/// POST method for removing an external login.
		/// </summary>
		/// <param name="model">A RemoveLoginBindingModel instance containing the login to remove.</param>
		/// <returns>An IHttpActionResult implementation containing the view data.</returns>
		[Route("RemoveLogin")]
		public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			IdentityResult result;

			if (model.LoginProvider == LOCAL_LOGIN_PROVIDER)
			{
				result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
			}
			else
			{
				result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
					new UserLoginInfo(model.LoginProvider, model.ProviderKey));
			}

			if (!result.Succeeded)
			{
				return GetErrorResult(result);
			}

			return Ok();
		}

		/// <summary>
		/// GET method for retrieving an external login.
		/// </summary>
		/// <param name="provider">A String value containing the login provider name.</param>
		/// <param name="error">A String value containing an error message, or null.</param>
		/// <returns>An IHttpActionResult implementation containing the view data.</returns>
		[OverrideAuthentication]
		[HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
		[AllowAnonymous]
		[Route("ExternalLogin", Name = "ExternalLogin")]
		public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
		{
			if (error != null)
			{
				return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
			}

			if (!User.Identity.IsAuthenticated)
			{
				return new ChallengeResult(provider, this);
			}

			ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

			if (externalLogin == null)
			{
				return InternalServerError();
			}

			if (externalLogin.LoginProvider != provider)
			{
				Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
				return new ChallengeResult(provider, this);
			}

			ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
				externalLogin.ProviderKey));

			bool hasRegistered = user != null;

			if (hasRegistered)
			{
				Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

				ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
				   OAuthDefaults.AuthenticationType);
				ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
					CookieAuthenticationDefaults.AuthenticationType);

				AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
				Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
			}
			else
			{
				IEnumerable<Claim> claims = externalLogin.GetClaims();
				ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
				Authentication.SignIn(identity);
			}

			return Ok();
		}

		/// <summary>
		/// GET method for listing external logins.
		/// </summary>
		/// <param name="returnUrl">A String value containing the return URL.</param>
		/// <param name="generateState">A Boolean value indicating whether or not to generate statefulness.</param>
		/// <returns>An IEnumerable of ExternalLoginViewModel containing the view data.</returns>
		[AllowAnonymous]
		[Route("ExternalLogins")]
		public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
		{
			IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
			List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

			string state;

			if (generateState)
			{
				const int strengthInBits = 256;
				state = RandomOAuthStateGenerator.Generate(strengthInBits);
			}
			else
			{
				state = null;
			}

			foreach (AuthenticationDescription description in descriptions)
			{
				ExternalLoginViewModel login = new ExternalLoginViewModel
				{
					Name = description.Caption,
					Url = Url.Route("ExternalLogin", new
					{
						provider = description.AuthenticationType,
						response_type = "token",
						client_id = Startup.PublicClientId,
						redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
						state = state
					}),
					State = state
				};
				logins.Add(login);
			}

			return logins;
		}

		/// <summary>
		/// POST method for registering a user account.
		/// </summary>
		/// <param name="model">A RegisterBindingModel instance containing the user account.</param>
		/// <returns>An IHttpActionResult implementation containing the view data.</returns>
		[AllowAnonymous]
		[Route("Register")]
		public async Task<IHttpActionResult> Register(RegisterBindingModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

			IdentityResult result = await UserManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
			{
				return GetErrorResult(result);
			}

			return Ok();
		}

		/// <summary>
		/// POST method for registering an external login.
		/// </summary>
		/// <param name="model">A RegisterExternalBindingModel instance containing the external login.</param>
		/// <returns>An IHttpActionResult implementation containing the view data.</returns>
		[OverrideAuthentication]
		[HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
		[Route("RegisterExternal")]
		public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var info = await Authentication.GetExternalLoginInfoAsync();
			if (info == null)
			{
				return InternalServerError();
			}

			var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

			IdentityResult result = await UserManager.CreateAsync(user);
			if (!result.Succeeded)
			{
				return GetErrorResult(result);
			}

			result = await UserManager.AddLoginAsync(user.Id, info.Login);
			if (!result.Succeeded)
			{
				return GetErrorResult(result);
			}
			return Ok();
		}

		/// <summary>
		/// Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && _userManager != null)
			{
				_userManager.Dispose();
				_userManager = null;
			}

			base.Dispose(disposing);
		}

		#region Helpers

		private IAuthenticationManager Authentication
		{
			get { return Request.GetOwinContext().Authentication; }
		}

		private IHttpActionResult GetErrorResult(IdentityResult result)
		{
			if (result == null)
			{
				return InternalServerError();
			}

			if (!result.Succeeded)
			{
				if (result.Errors != null)
				{
					foreach (string error in result.Errors)
					{
						ModelState.AddModelError("", error);
					}
				}

				if (ModelState.IsValid)
				{
					// No ModelState errors are available to send, so just return an empty BadRequest.
					return BadRequest();
				}

				return BadRequest(ModelState);
			}

			return null;
		}

		private class ExternalLoginData
		{
			public string LoginProvider { get; set; }
			public string ProviderKey { get; set; }
			public string UserName { get; set; }

			public IEnumerable<Claim> GetClaims()
			{
				IList<Claim> claims = new List<Claim>();
				claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

				if (UserName != null)
				{
					claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
				}

				return claims;
			}

			public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
			{
				if (identity == null)
				{
					return null;
				}

				Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

				if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
					|| String.IsNullOrEmpty(providerKeyClaim.Value))
				{
					return null;
				}

				if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
				{
					return null;
				}

				return new ExternalLoginData
				{
					LoginProvider = providerKeyClaim.Issuer,
					ProviderKey = providerKeyClaim.Value,
					UserName = identity.FindFirstValue(ClaimTypes.Name)
				};
			}
		}

		private static class RandomOAuthStateGenerator
		{
			private static readonly RandomNumberGenerator _random = new RNGCryptoServiceProvider();

			public static string Generate(int strengthInBits)
			{
				const int bitsPerByte = 8;

				if (strengthInBits % bitsPerByte != 0)
				{
					throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
				}

				int strengthInBytes = strengthInBits / bitsPerByte;

				byte[] data = new byte[strengthInBytes];
				_random.GetBytes(data);
				return HttpServerUtility.UrlTokenEncode(data);
			}
		}

		#endregion
	}
}
