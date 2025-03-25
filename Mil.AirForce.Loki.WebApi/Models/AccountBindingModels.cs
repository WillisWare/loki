using System.ComponentModel.DataAnnotations;
using Mil.AirForce.Loki.Common.Resources;

// Models used as parameters to AccountController actions.
namespace Mil.AirForce.Loki.WebApi.Models
{
	/// <summary>
	/// Data class for adding an external login.
	/// </summary>
	public class AddExternalLoginBindingModel
	{
		/// <summary>
		/// Gets or sets the access token.
		/// </summary>
		[Required]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_ProviderAccessToken")]
		public string ExternalAccessToken { get; set; }
	}

	/// <summary>
	/// Data class for changing a password.
	/// </summary>
	public class ChangePasswordBindingModel
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
		[Compare("NewPassword", ErrorMessageResourceType = typeof(ExceptionStrings), ErrorMessageResourceName = "Validation_PasswordMismatch")]
		public string ConfirmPassword { get; set; }
	}

	/// <summary>
	/// Data class for registering a user account.
	/// </summary>
	public class RegisterBindingModel
	{
		/// <summary>
		/// Gets or sets the e-mail address.
		/// </summary>
		[Required]
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
	/// Data class for registering external logins.
	/// </summary>
	public class RegisterExternalBindingModel
	{
		/// <summary>
		/// Gets or sets the e-mail address.
		/// </summary>
		[Required]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_Email")]
		public string Email { get; set; }
	}

	/// <summary>
	/// Data class for removing an external login.
	/// </summary>
	public class RemoveLoginBindingModel
	{
		/// <summary>
		/// Gets or sets the external login provider name.
		/// </summary>
		[Required]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_ProviderName")]
		public string LoginProvider { get; set; }

		/// <summary>
		/// Gets or sets the external login provider access key.
		/// </summary>
		[Required]
		[Display(ResourceType = typeof(UiStrings), Name = "Label_ProviderKey")]
		public string ProviderKey { get; set; }
	}

	/// <summary>
	/// Data class for setting a password.
	/// </summary>
	public class SetPasswordBindingModel
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
		[Compare("NewPassword", ErrorMessageResourceType = typeof(ExceptionStrings), ErrorMessageResourceName = "Validation_PasswordMismatch")]
		public string ConfirmPassword { get; set; }
	}
}
