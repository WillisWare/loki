using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mil.AirForce.Loki.Common.Resources;

namespace Mil.AirForce.Loki.WebUi.Models
{
	/// <summary>
	/// Data class for external login confirmation.
	/// </summary>
	public class ExternalLoginConfirmationViewModel
	{
		/// <summary>
		/// Gets or sets the e-mail address.
		/// </summary>
		[Required]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_Email")]
		public string Email { get; set; }
	}

	/// <summary>
	/// Data class for linking/removing external logins.
	/// </summary>
	public class ExternalLoginListViewModel
	{
		/// <summary>
		/// Gets or sets the return URL.
		/// </summary>
		public string ReturnUrl { get; set; }
	}

	/// <summary>
	/// Data class for sending authentication codes.
	/// </summary>
	public class SendCodeViewModel
	{
		/// <summary>
		/// Gets or sets the external login provider name.
		/// </summary>
		public string SelectedProvider { get; set; }

		/// <summary>
		/// Gets or sets the collection of external login providers.
		/// </summary>
		public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }

		/// <summary>
		/// Gets or sets the return URL.
		/// </summary>
		public string ReturnUrl { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether or not to persist the login.
		/// </summary>
		public bool RememberMe { get; set; }
	}

	/// <summary>
	/// Data class for verifying authentication codes.
	/// </summary>
	public class VerifyCodeViewModel
	{
		/// <summary>
		/// Gets or sets the external login provider name.
		/// </summary>
		[Required]
		public string Provider { get; set; }

		/// <summary>
		/// Gets or sets the authentication code.
		/// </summary>
		[Required]
		public string Code { get; set; }

		/// <summary>
		/// Gets or sets the return URL.
		/// </summary>
		public string ReturnUrl { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether or not a cookie should be set.
		/// </summary>
		[Display(ResourceType = typeof(UiStrings), Name = "Label_RememberBrowser")]
		public bool RememberBrowser { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether or not to persist the login.
		/// </summary>
		[Display(ResourceType = typeof(UiStrings), Name = "Label_RememberMe")]
		public bool RememberMe { get; set; }
	}

	/// <summary>
	/// Data class for retrieving passwords.
	/// </summary>
	public class ForgotViewModel
	{
		/// <summary>
		/// Gets or sets the e-mail address.
		/// </summary>
		[Required]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_Email")]
		public string Email { get; set; }
	}

	/// <summary>
	/// Data class for logging in.
	/// </summary>
	public class LoginViewModel
	{
		/// <summary>
		/// Gets or sets the e-mail address.
		/// </summary>
		[Required]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_Email")]
		[EmailAddress]
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		[Required]
		[DataType(DataType.Password)]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_Password")]
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether or not to persist the login.
		/// </summary>
		[Display(ResourceType = typeof(UiStrings), Name = "Label_RememberMe")]
		public bool RememberMe { get; set; }
	}

	/// <summary>
	/// Data class for registering a user account.
	/// </summary>
	public class RegisterViewModel
	{
		/// <summary>
		/// Gets or sets the e-mail address.
		/// </summary>
		[Required]
		[EmailAddress]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_Email")]
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		[Required]
		[StringLength(100, ErrorMessageResourceType = typeof(ExceptionStrings), ErrorMessageResourceName = "Validation_FieldLength", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_Password")]
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets the password confirmation.
		/// </summary>
		[DataType(DataType.Password)]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_ConfirmPassword")]
		[Compare("Password", ErrorMessageResourceType = typeof(ExceptionStrings), ErrorMessageResourceName = "Validation_PasswordMismatch")]
		public string ConfirmPassword { get; set; }
	}

	/// <summary>
	/// Data class for resetting a password.
	/// </summary>
	public class ResetPasswordViewModel
	{
		/// <summary>
		/// Gets or sets the e-mail address.
		/// </summary>
		[Required]
		[EmailAddress]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_Email")]
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		[Required]
		[StringLength(100, ErrorMessageResourceType = typeof(ExceptionStrings), ErrorMessageResourceName = "Validation_FieldLength", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_Password")]
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets the password confirmation.
		/// </summary>
		[DataType(DataType.Password)]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_ConfirmPassword")]
		[Compare("Password", ErrorMessageResourceType = typeof(ExceptionStrings), ErrorMessageResourceName = "Validation_PasswordMismatch")]
		public string ConfirmPassword { get; set; }

		/// <summary>
		/// Gets or sets the authentication code.
		/// </summary>
		public string Code { get; set; }
	}

	/// <summary>
	/// Data class for retrieving a password.
	/// </summary>
	public class ForgotPasswordViewModel
	{
		/// <summary>
		/// Gets or sets the e-mail address.
		/// </summary>
		[Required]
		[EmailAddress]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_Email")]
		public string Email { get; set; }
	}
}
