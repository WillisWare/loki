using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mil.AirForce.Loki.WebUi.Startup))]

namespace Mil.AirForce.Loki.WebUi
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
