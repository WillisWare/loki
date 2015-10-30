using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Mil.AirForce.Loki.Common.Resources;

namespace Mil.AirForce.Loki.WebUi.Models
{
	/// <summary>
	/// Data class for default view.
	/// </summary>
	public class IndexViewModel
	{
		/// <summary>
		/// Gets or sets a value indicating whether or not the user has a password.
		/// </summary>
		public bool HasPassword { get; set; }

		/// <summary>
		/// Gets or sets the collection of external logins.
		/// </summary>
		public IList<UserLoginInfo> Logins { get; set; }

		/// <summary>
		/// Gets or sets the phone number.
		/// </summary>
		[Phone]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_PhoneNumber")]
		public string PhoneNumber { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether or not two-factor authentication is enabled.
		/// </summary>
		public bool TwoFactor { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether or not a cookie is set.
		/// </summary>
		public bool BrowserRemembered { get; set; }
	}

	/// <summary>
	/// Data class for managing external logins.
	/// </summary>
	public class ManageLoginsViewModel
	{
		/// <summary>
		/// Gets or sets the collection of linked external logins.
		/// </summary>
		public IList<UserLoginInfo> CurrentLogins { get; set; }

		/// <summary>
		/// Gets or sets the collection of available external logins.
		/// </summary>
		public IList<AuthenticationDescription> OtherLogins { get; set; }
	}

	/// <summary>
	/// Data class for two-factor authentication.
	/// </summary>
	public class FactorViewModel
	{
		/// <summary>
		/// Gets or sets the purpose.
		/// </summary>
		public string Purpose { get; set; }
	}

	/// <summary>
	/// Data class for setting a password.
	/// </summary>
	public class SetPasswordViewModel
	{
		/// <summary>
		/// Gets or sets the new password.
		/// </summary>
		[Required]
		[StringLength(100, ErrorMessageResourceType = typeof(ExceptionStrings), ErrorMessageResourceName = "Validation_FieldLength", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_NewPassword")]
		public string NewPassword { get; set; }

		/// <summary>
		/// Gets or sets the password confirmation.
		/// </summary>
		[DataType(DataType.Password)]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_ConfirmNewPassword")]
		[Compare("Password", ErrorMessageResourceType = typeof(ExceptionStrings), ErrorMessageResourceName = "Validation_PasswordMismatch")]
		public string ConfirmPassword { get; set; }
	}

	/// <summary>
	/// Data class for changing a password.
	/// </summary>
	public class ChangePasswordViewModel
	{
		/// <summary>
		/// Gets or sets the old password.
		/// </summary>
		[Required]
		[DataType(DataType.Password)]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_CurrentPassword")]
		public string OldPassword { get; set; }

		/// <summary>
		/// Gets or sets the new password.
		/// </summary>
		[Required]
		[StringLength(100, ErrorMessageResourceType = typeof(ExceptionStrings), ErrorMessageResourceName = "Validation_FieldLength", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_NewPassword")]
		public string NewPassword { get; set; }

		/// <summary>
		/// Gets or sets the password confirmation.
		/// </summary>
		[DataType(DataType.Password)]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_ConfirmNewPassword")]
		[Compare("Password", ErrorMessageResourceType = typeof(ExceptionStrings), ErrorMessageResourceName = "Validation_PasswordMismatch")]
		public string ConfirmPassword { get; set; }
	}

	/// <summary>
	/// Data class for adding a phone number.
	/// </summary>
	public class AddPhoneNumberViewModel
	{
		/// <summary>
		/// Gets or sets the phone number.
		/// </summary>
		[Required]
		[Phone]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_PhoneNumber")]
		public string Number { get; set; }
	}

	/// <summary>
	/// Data class for verifying a phone number.
	/// </summary>
	public class VerifyPhoneNumberViewModel
	{
		/// <summary>
		/// Gets or sets the verification code.
		/// </summary>
		[Required]
		public string Code { get; set; }

		/// <summary>
		/// Gets or sets the phone number.
		/// </summary>
		[Required]
		[Phone]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_PhoneNumber")]
		public string PhoneNumber { get; set; }
	}

	/// <summary>
	/// Data class for configuring two-factor authentication.
	/// </summary>
	public class ConfigureTwoFactorViewModel
	{
		/// <summary>
		/// Gets or sets the external login provider name.
		/// </summary>
		public string SelectedProvider { get; set; }

		/// <summary>
		/// Gets or sets the collection of external login providers.
		/// </summary>
		public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
	}
}