using System.Web.Mvc;
using System.Web.Routing;

namespace Mil.AirForce.Loki.WebUi
{
	/// <summary>
	/// Configuration class for MVC routing.
	/// </summary>
	public class RouteConfig
	{
		/// <summary>
		/// Registers MVC routes for the application.
		/// </summary>
		/// <param name="routes">A RouteCollection instance to be configured.</param>
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
