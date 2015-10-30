using System.Web.Mvc;

namespace Mil.AirForce.Loki.WebUi
{
	/// <summary>
	/// Configuration class for ASP.NET filters.
	/// </summary>
	public class FilterConfig
	{
		/// <summary>
		/// Registers ASP.NET filters for the application.
		/// </summary>
		/// <param name="filters">A GlobalFilterCollection instance to be configured.</param>
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
