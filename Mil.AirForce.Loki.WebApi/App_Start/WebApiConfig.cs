using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Owin.Security.OAuth;

namespace Mil.AirForce.Loki.WebApi
{
	/// <summary>
	/// Configuration class for ASP.NET WebApi.
	/// </summary>
	public static class WebApiConfig
	{
		/// <summary>
		/// Registers ASP.NET WebApi components and routes for the application.
		/// </summary>
		/// <param name="config">An HttpConfiguration instance to be configured.</param>
		public static void Register(HttpConfiguration config)
		{
			var corsAllowedOrigins = ConfigurationManager.AppSettings["corsAllowedOrigins"];

			// Web API configuration and services
			config.EnableCors(new EnableCorsAttribute(corsAllowedOrigins, "*", "*"));

			// Configure Web API to use only bearer token authentication.
			config.SuppressDefaultHostAuthentication();

			config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
