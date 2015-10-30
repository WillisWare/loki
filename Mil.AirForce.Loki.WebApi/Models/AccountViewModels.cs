using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mil.AirForce.Loki.Common.Resources;

// Models returned by AccountController actions.
namespace Mil.AirForce.Loki.WebApi.Models
{
	/// <summary>
	/// Data class for managing external logins.
	/// </summary>
	public class ExternalLoginViewModel
	{
		/// <summary>
		/// Gets or sets the external login provider name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the external login provider URL.
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// Gets or sets the state(?)
		/// </summary>
		public string State { get; set; }
	}

	/// <summary>
	/// Data class for managing user account info.
	/// </summary>
	public class ManageInfoViewModel
	{
		/// <summary>
		/// Gets or sets the login provider name.
		/// </summary>
		public string LocalLoginProvider { get; set; }

		/// <summary>
		/// Gets or sets the e-mail address.
		/// </summary>
		[Display(ResourceType = typeof(UiStrings), Name = "Label_Email")]
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the collection of external logins.
		/// </summary>
		public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

		/// <summary>
		/// Gets or sets the collection of external login providers.
		/// </summary>
		public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
	}

	/// <summary>
	/// Data class for managing user account info.
	/// </summary>
	public class UserInfoViewModel
	{
		/// <summary>
		/// Gets or sets the e-mail address.
		/// </summary>
		[Display(ResourceType = typeof(UiStrings), Name = "Label_Email")]
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets whether or not the user is registered.
		/// </summary>
		public bool HasRegistered { get; set; }

		/// <summary>
		/// Gets or sets the login provider name.
		/// </summary>
		public string LoginProvider { get; set; }
	}

	/// <summary>
	/// Data class for managing user login info.
	/// </summary>
	public class UserLoginInfoViewModel
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
}
