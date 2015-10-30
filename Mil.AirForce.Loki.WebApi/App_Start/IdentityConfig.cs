using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Mil.AirForce.Loki.WebApi.Models;

namespace Mil.AirForce.Loki.WebApi
{
	/// <summary>
	/// Manager class for application user accounts.
	/// </summary>
	public class ApplicationUserManager : UserManager<ApplicationUser>
	{
		/// <summary>
		/// Default constructor. Initializes an instance of this class.
		/// </summary>
		/// <param name="store">An IUserStore implementation for managing users.</param>
		public ApplicationUserManager(IUserStore<ApplicationUser> store)
			: base(store)
		{
		}

		/// <summary>
		/// Creates a new manager instance.
		/// </summary>
		/// <param name="options">An IdentityFactoryOptions instance containing the options for the manager.</param>
		/// <param name="context">An IOwinContext implementation to initialize the manager.</param>
		/// <returns>An ApplicationUserManager instance.</returns>
		public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
		{
			var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
			// Configure validation logic for usernames
			manager.UserValidator = new UserValidator<ApplicationUser>(manager)
			{
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = true
			};
			// Configure validation logic for passwords
			manager.PasswordValidator = new PasswordValidator
			{
				RequiredLength = 6,
				RequireNonLetterOrDigit = true,
				RequireDigit = true,
				RequireLowercase = true,
				RequireUppercase = true,
			};
			var dataProtectionProvider = options.DataProtectionProvider;
			if (dataProtectionProvider != null)
			{
				manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
			}
			return manager;
		}
	}
}
