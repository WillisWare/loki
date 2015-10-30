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
	/// User account management controller.
	/// </summary>
	[Authorize]
	public class ManageController : Controller
	{
		private ApplicationSignInManager _signInManager;

		private ApplicationUserManager _userManager;

		/// <summary>
		/// Default constructor. Initializes an instance of this class.
		/// </summary>
		public ManageController()
		{
		}

		/// <summary>
		/// Overloaded. Initializes an instance of this class.
		/// </summary>
		/// <param name="userManager">An ApplicationUserManager instance for managing user accounts.</param>
		/// <param name="signInManager">An ApplicationSignInManager instance for managing logins.</param>
		public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
		/// Default action. Displays a view.
		/// </summary>
		/// <param name="message">A ManageMessageId enum value containing the message to be displayed.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		public async Task<ActionResult> Index(ManageMessageId? message)
		{
			ViewBag.StatusMessage =
				message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
				: message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
				: message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
				: message == ManageMessageId.Error ? "An error has occurred."
				: message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
				: message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
				: "";

			var userId = User.Identity.GetUserId();
			var model = new IndexViewModel
			{
				HasPassword = HasPassword(),
				PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
				TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
				Logins = await UserManager.GetLoginsAsync(userId),
				BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
			};
			return View(model);
		}

		/// <summary>
		/// POST method for removing an external login.
		/// </summary>
		/// <param name="loginProvider">A String value containing the external login provider name.</param>
		/// <param name="providerKey">A String value containing the external login provider access key.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
		{
			ManageMessageId? message;
			var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
			if (result.Succeeded)
			{
				var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
				if (user != null)
				{
					await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
				}
				message = ManageMessageId.RemoveLoginSuccess;
			}
			else
			{
				message = ManageMessageId.Error;
			}
			return RedirectToAction("ManageLogins", new { Message = message });
		}

		/// <summary>
		/// GET method for adding a phone number.
		/// </summary>
		/// <returns>An ActionResult instance containing the view data.</returns>
		public ActionResult AddPhoneNumber()
		{
			return View();
		}

		/// <summary>
		/// POST method for adding a phone number.
		/// </summary>
		/// <param name="model">An AddPhoneNumberViewModel instance containing the phone number info.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			// Generate the token and send it
			var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
			if (UserManager.SmsService != null)
			{
				var message = new IdentityMessage
				{
					Destination = model.Number,
					Body = "Your security code is: " + code
				};
				await UserManager.SmsService.SendAsync(message);
			}
			return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
		}

		/// <summary>
		/// POST method for enabling two-factor authentication.
		/// </summary>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EnableTwoFactorAuthentication()
		{
			await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
			var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
			if (user != null)
			{
				await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
			}
			return RedirectToAction("Index", "Manage");
		}

		/// <summary>
		/// POST method for disabling two-factor authentication.
		/// </summary>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DisableTwoFactorAuthentication()
		{
			await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
			var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
			if (user != null)
			{
				await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
			}
			return RedirectToAction("Index", "Manage");
		}

		/// <summary>
		/// GET method for verifying a phone number.
		/// </summary>
		/// <param name="phoneNumber">A String value containing the phone number.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
		{
			var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
			// Send an SMS through the SMS provider to verify the phone number
			return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
		}

		/// <summary>
		/// POST method for verifying a phone number.
		/// </summary>
		/// <param name="model">A VerifyPhoneNumberViewModel instance containing the phone number info.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
			if (result.Succeeded)
			{
				var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
				if (user != null)
				{
					await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
				}
				return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
			}
			// If we got this far, something failed, redisplay form
			ModelState.AddModelError("", "Failed to verify phone");
			return View(model);
		}

		/// <summary>
		/// GET method for removing a phone number.
		/// </summary>
		/// <returns>An ActionResult instance containing the view data.</returns>
		public async Task<ActionResult> RemovePhoneNumber()
		{
			var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
			if (!result.Succeeded)
			{
				return RedirectToAction("Index", new { Message = ManageMessageId.Error });
			}
			var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
			if (user != null)
			{
				await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
			}
			return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
		}

		/// <summary>
		/// GET method for changing a password.
		/// </summary>
		/// <returns>An ActionResult instance containing the view data.</returns>
		public ActionResult ChangePassword()
		{
			return View();
		}

		/// <summary>
		/// POST method for changing a password.
		/// </summary>
		/// <param name="model">A ChangePasswordViewModel instance containing the password info.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
			if (result.Succeeded)
			{
				var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
				if (user != null)
				{
					await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
				}
				return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
			}
			AddErrors(result);
			return View(model);
		}

		/// <summary>
		/// GET method for setting a password.
		/// </summary>
		/// <returns>An ActionResult instance containing the view data.</returns>
		public ActionResult SetPassword()
		{
			return View();
		}

		/// <summary>
		/// POST method for setting a password.
		/// </summary>
		/// <param name="model">A SetPasswordViewModel instance containing the password info.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
				if (result.Succeeded)
				{
					var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
					if (user != null)
					{
						await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
					}
					return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
				}
				AddErrors(result);
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		/// <summary>
		/// GET method for managing external logins.
		/// </summary>
		/// <param name="message">A ManageMessageId enum value containing the message to be displayed.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		public async Task<ActionResult> ManageLogins(ManageMessageId? message)
		{
			ViewBag.StatusMessage =
				message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
				: message == ManageMessageId.Error ? "An error has occurred."
				: "";
			var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
			if (user == null)
			{
				return View("Error");
			}
			var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
			var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
			ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
			return View(new ManageLoginsViewModel
			{
				CurrentLogins = userLogins,
				OtherLogins = otherLogins
			});
		}

		/// <summary>
		/// POST method for linking an external login.
		/// </summary>
		/// <param name="provider">A String value containing the external login provider name.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LinkLogin(string provider)
		{
			// Request a redirect to the external login provider to link a login for the current user
			return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
		}

		/// <summary>
		/// GET method for linking an external login.
		/// </summary>
		/// <returns>An ActionResult instance containing the view data.</returns>
		public async Task<ActionResult> LinkLoginCallback()
		{
			var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
			if (loginInfo == null)
			{
				return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
			}
			var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
			return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
		}

		/// <summary>
		/// Releases unmanaged resources and optionally releases managed resources.
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
		// Used for XSRF protection when adding external logins
		private const string XsrfKey = "XsrfId";

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

		private bool HasPassword()
		{
			var user = UserManager.FindById(User.Identity.GetUserId());
			if (user != null)
			{
				return user.PasswordHash != null;
			}
			return false;
		}

		private bool HasPhoneNumber()
		{
			var user = UserManager.FindById(User.Identity.GetUserId());
			if (user != null)
			{
				return user.PhoneNumber != null;
			}
			return false;
		}

		/// <summary>
		/// Enumeration for indicating display messages.
		/// </summary>
		public enum ManageMessageId
		{
			/// <summary>
			/// Indicates successful addition of a phone number.
			/// </summary>
			AddPhoneSuccess,
			/// <summary>
			/// Indicates successful change of password.
			/// </summary>
			ChangePasswordSuccess,
			/// <summary>
			/// Indicates successful two-factor enable/disable.
			/// </summary>
			SetTwoFactorSuccess,
			/// <summary>
			/// Indicates successful setting of a password.
			/// </summary>
			SetPasswordSuccess,
			/// <summary>
			/// Indicates successful removal of an external login.
			/// </summary>
			RemoveLoginSuccess,
			/// <summary>
			/// Indicates successful removal of a phone number.
			/// </summary>
			RemovePhoneSuccess,
			/// <summary>
			/// Indicates an error.
			/// </summary>
			Error
		}

		#endregion
	}
}