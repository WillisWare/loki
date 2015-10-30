using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Mil.AirForce.Loki.WebApi.Startup))]

namespace Mil.AirForce.Loki.WebApi
{
	/// <summary>
	/// Startup class for the application.
	/// </summary>
	public partial class Startup
	{
		/// <summary>
		/// Configures the application for startup.
		/// </summary>
		/// <param name="app">An IAppBuilder implementation to be configured.</param>
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
	}
}
