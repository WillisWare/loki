using System.Web.Http;
using System.Web.Mvc;

namespace Mil.AirForce.Loki.WebApi.Areas.HelpPage
{
	/// <summary>
	/// Configuration class for MVC areas.
	/// </summary>
	public class HelpPageAreaRegistration : AreaRegistration
	{
		/// <summary>
		/// Gets the area name.
		/// </summary>
		public override string AreaName
		{
			get { return "HelpPage"; }
		}

		/// <summary>
		/// Registers the help page MVC area.
		/// </summary>
		/// <param name="context">An AreaRegistrationContext instance to be configured.</param>
		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"HelpPage_Default",
				"Help/{action}/{apiId}",
				new { controller = "Help", action = "Index", apiId = UrlParameter.Optional });

			HelpPageConfig.Register(GlobalConfiguration.Configuration);
		}
	}
}