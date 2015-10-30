using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Mil.AirForce.Loki.WebUi.Models;

namespace Mil.AirForce.Loki.WebUi.Controllers
{
	/// <summary>
	/// User account controller.
	/// </summary>
	[Authorize]
	public class AccountController : Controller
	{
		private ApplicationSignInManager _signInManager;

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
		/// <param name="signInManager">An ApplicationSignInManager instance for managing logins.</param>
		public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
		{
			UserManager = userManager;
			SignInManager = signInManager;
		}

		/// <summary>
		/// Gets the login manager.
		/// </summary>
		public ApplicationSignInManager SignInManager
		{
			get
			{
				return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
			}
			private set
			{
				_signInManager = value;
			}
		}

		/// <summary>
		/// Gets the user account manager.
		/// </summary>
		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}

		/// <summary>
		/// GET method for logging in.
		/// </summary>
		/// <param name="returnUrl">A String value containing the return URL.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		/// <summary>
		/// POST method for logging in.
		/// </summary>
		/// <param name="model">A LoginViewModel instance containing the login info.</param>
		/// <param name="returnUrl">A String value containing the return URL.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			// This doesn't count login failures towards account lockout
			// To enable password failures to trigger account lockout, change to shouldLockout: true
			var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
			switch (result)
			{
				case SignInStatus.Success:
					return RedirectToLocal(returnUrl);
				case SignInStatus.LockedOut:
					return View("Lockout");
				case SignInStatus.RequiresVerification:
					return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
				case SignInStatus.Failure:
				default:
					ModelState.AddModelError("", "Invalid login attempt.");
					return View(model);
			}
		}

		/// <summary>
		/// GET method for verifying an authentication code.
		/// </summary>
		/// <param name="provider">A String value containing the external login provider name.</param>
		/// <param name="returnUrl">A String value containing the return URL.</param>
		/// <param name="rememberMe">A Boolean value indicating whether or not to persist the login.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[AllowAnonymous]
		public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
		{
			// Require that the user has already logged in via username/password or external login
			if (!await SignInManager.HasBeenVerifiedAsync())
			{
				return View("Error");
			}
			return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
		}

		/// <summary>
		/// POST method for verifying an authentication code.
		/// </summary>
		/// <param name="model">A VerifyCodeViewModel instance containing the login info.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			// The following code protects for brute force attacks against the two factor codes. 
			// If a user enters incorrect codes for a specified amount of time then the user account 
			// will be locked out for a specified amount of time. 
			// You can configure the account lockout settings in IdentityConfig
			var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
			switch (result)
			{
				case SignInStatus.Success:
					return RedirectToLocal(model.ReturnUrl);
				case SignInStatus.LockedOut:
					return View("Lockout");
				case SignInStatus.Failure:
				default:
					ModelState.AddModelError("", "Invalid code.");
					return View(model);
			}
		}

		/// <summary>
		/// GET method for registering a user account.
		/// </summary>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[AllowAnonymous]
		public ActionResult Register()
		{
			return View();
		}

		/// <summary>
		/// POST method for registering a user account.
		/// </summary>
		/// <param name="model">A RegisterViewModel instance containing the login info.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
				var result = await UserManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

					// For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
					// Send an email with this link
					// string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
					// var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
					// await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

					return RedirectToAction("Index", "Home");
				}
				AddErrors(result);
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		/// <summary>
		/// GET method for confirming an e-mail address.
		/// </summary>
		/// <param name="userId">A String value containing the user ID.</param>
		/// <param name="code">A String value containing the authentication code.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[AllowAnonymous]
		public async Task<ActionResult> ConfirmEmail(string userId, string code)
		{
			if (userId == null || code == null)
			{
				return View("Error");
			}
			var result = await UserManager.ConfirmEmailAsync(userId, code);
			return View(result.Succeeded ? "ConfirmEmail" : "Error");
		}

		/// <summary>
		/// GET method for retrieving a password.
		/// </summary>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[AllowAnonymous]
		public ActionResult ForgotPassword()
		{
			return View();
		}

		/// <summary>
		/// POST method for retrieving a password.
		/// </summary>
		/// <param name="model">A ForgotPasswordViewModel instance containing the password info.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await UserManager.FindByNameAsync(model.Email);
				if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
				{
					// Don't reveal that the user does not exist or is not confirmed
					return View("ForgotPasswordConfirmation");
				}

				// For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
				// Send an email with this link
				// string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
				// var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
				// await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
				// return RedirectToAction("ForgotPasswordConfirmation", "Account");
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		/// <summary>
		/// GET method for confirming a password.
		/// </summary>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[AllowAnonymous]
		public ActionResult ForgotPasswordConfirmation()
		{
			return View();
		}

		/// <summary>
		/// GET method for resetting a password.
		/// </summary>
		/// <param name="code">A String value containing the authentication code.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[AllowAnonymous]
		public ActionResult ResetPassword(string code)
		{
			return code == null ? View("Error") : View();
		}

		/// <summary>
		/// POST method for resetting a password.
		/// </summary>
		/// <param name="model">A ResetPasswordViewModel instance containing the password info.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var user = await UserManager.FindByNameAsync(model.Email);
			if (user == null)
			{
				// Don't reveal that the user does not exist
				return RedirectToAction("ResetPasswordConfirmation", "Account");
			}
			var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
			if (result.Succeeded)
			{
				return RedirectToAction("ResetPasswordConfirmation", "Account");
			}
			AddErrors(result);
			return View();
		}

		/// <summary>
		/// GET method for confirming a password.
		/// </summary>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[AllowAnonymous]
		public ActionResult ResetPasswordConfirmation()
		{
			return View();
		}

		/// <summary>
		/// POST method for linking an external login.
		/// </summary>
		/// <param name="provider">A String value containing the external login provider name.</param>
		/// <param name="returnUrl">A String value containing the return URL.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult ExternalLogin(string provider, string returnUrl)
		{
			// Request a redirect to the external login provider
			return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
		}

		/// <summary>
		/// GET method for sending an authentication code.
		/// </summary>
		/// <param name="returnUrl">A String value containing the return URL.</param>
		/// <param name="rememberMe">A Boolean value indicating whether or not to presist the login.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[AllowAnonymous]
		public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
		{
			var userId = await SignInManager.GetVerifiedUserIdAsync();
			if (userId == null)
			{
				return View("Error");
			}
			var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
			var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
			return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
		}

		/// <summary>
		/// POST method for sending an authentication code.
		/// </summary>
		/// <param name="model">A SendCodeViewModel instance containing the authentication code info.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> SendCode(SendCodeViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			// Generate the token and send it
			if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
			{
				return View("Error");
			}
			return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
		}

		/// <summary>
		/// GET method for linking an external login.
		/// </summary>
		/// <param name="returnUrl">A String value containing the return URL.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[AllowAnonymous]
		public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
		{
			var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
			if (loginInfo == null)
			{
				return RedirectToAction("Login");
			}

			// Sign in the user with this external login provider if the user already has a login
			var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
			switch (result)
			{
				case SignInStatus.Success:
					return RedirectToLocal(returnUrl);
				case SignInStatus.LockedOut:
					return View("Lockout");
				case SignInStatus.RequiresVerification:
					return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
				case SignInStatus.Failure:
				default:
					// If the user does not have an account, then prompt the user to create an account
					ViewBag.ReturnUrl = returnUrl;
					ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
					return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
			}
		}

		/// <summary>
		/// POST method for confirming an external login.
		/// </summary>
		/// <param name="model">An ExternalLoginConfirmationViewModel instance containing the external login info.</param>
		/// <param name="returnUrl">A String value containing the return URL.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index", "Manage");
			}

			if (ModelState.IsValid)
			{
				// Get the information about the user from the external login provider
				var info = await AuthenticationManager.GetExternalLoginInfoAsync();
				if (info == null)
				{
					return View("ExternalLoginFailure");
				}
				var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
				var result = await UserManager.CreateAsync(user);
				if (result.Succeeded)
				{
					result = await UserManager.AddLoginAsync(user.Id, info.Login);
					if (result.Succeeded)
					{
						await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
						return RedirectToLocal(returnUrl);
					}
				}
				AddErrors(result);
			}

			ViewBag.ReturnUrl = returnUrl;
			return View(model);
		}

		/// <summary>
		/// POST method for logging out.
		/// </summary>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOff()
		{
			AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
			return RedirectToAction("Index", "Home");
		}

		/// <summary>
		/// GET method for displaying a login failure.
		/// </summary>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[AllowAnonymous]
		public ActionResult ExternalLoginFailure()
		{
			return View();
		}

		/// <summary>
		/// Releases unmanaged resources and optionally releases managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_userManager != null)
				{
					_userManager.Dispose();
					_userManager = null;
				}

				if (_signInManager != null)
				{
					_signInManager.Dispose();
					_signInManager = null;
				}
			}

			base.Dispose(disposing);
		}

		#region Helpers

		// Used for XSRF protection when adding external logins
		private const string XSRF_KEY = "XsrfId";

		private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return HttpContext.GetOwinContext().Authentication;
			}
		}

		private void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error);
			}
		}

		private ActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			return RedirectToAction("Index", "Home");
		}

		internal class ChallengeResult : HttpUnauthorizedResult
		{
			public ChallengeResult(string provider, string redirectUri)
				: this(provider, redirectUri, null)
			{
			}

			public ChallengeResult(string provider, string redirectUri, string userId)
			{
				LoginProvider = provider;
				RedirectUri = redirectUri;
				UserId = userId;
			}

			public string LoginProvider { get; set; }
			public string RedirectUri { get; set; }
			public string UserId { get; set; }

			public override void ExecuteResult(ControllerContext context)
			{
				var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
				if (UserId != null)
				{
					properties.Dictionary[XSRF_KEY] = UserId;
				}
				context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
			}
		}

		#endregion
	}
}