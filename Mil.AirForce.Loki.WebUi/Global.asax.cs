using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Mil.AirForce.Loki.WebUi
{
	/// <summary>
	/// Startup logic for the application.
	/// </summary>
	public class MvcApplication : HttpApplication
	{
		/// <summary>
		/// Main entry point for the application.
		/// </summary>
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}
}
