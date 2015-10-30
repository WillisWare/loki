using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Mil.AirForce.Loki.WebApi.Models
{
	/// <summary>
	/// Data class for user accounts.
	/// </summary>
	/// <remarks>You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.</remarks>
	public class ApplicationUser : IdentityUser
	{
		/// <summary>
		/// Creates an application user account.
		/// </summary>
		/// <param name="manager">A UserManager instance for creating users.</param>
		/// <param name="authenticationType">A String value containing the authentication type.</param>
		/// <returns>A ClaimsIdentity instance containing the results of the operation.</returns>
		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

			// Add custom user claims here
			return userIdentity;
		}
	}

	/// <summary>
	/// Database context for user accounts.
	/// </summary>
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		/// <summary>
		/// Default constructor. Initializes an instance of this class.
		/// </summary>
		public ApplicationDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		/// <summary>
		/// Creates a new application database context.
		/// </summary>
		/// <returns>An ApplicationDbContext instance.</returns>
		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}
	}
}