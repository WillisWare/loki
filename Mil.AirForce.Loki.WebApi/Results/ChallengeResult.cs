using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Mil.AirForce.Loki.WebApi.Results
{
	/// <summary>
	/// Data class for external logins.
	/// </summary>
	public class ChallengeResult : IHttpActionResult
	{
		/// <summary>
		/// Default constructor. Initializes an instance of this class.
		/// </summary>
		/// <param name="loginProvider">A String value containing the external login provider name.</param>
		/// <param name="controller">An ApiController instance containing the request context.</param>
		public ChallengeResult(string loginProvider, ApiController controller)
		{
			LoginProvider = loginProvider;
			Request = controller.Request;
		}

		/// <summary>
		/// Gets or sets the external login provider name.
		/// </summary>
		public string LoginProvider { get; set; }

		/// <summary>
		/// Gets or sets the request context.
		/// </summary>
		public HttpRequestMessage Request { get; set; }

		Task<HttpResponseMessage> IHttpActionResult.ExecuteAsync(CancellationToken cancellationToken)
		{
			Request.GetOwinContext().Authentication.Challenge(LoginProvider);

			var response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { RequestMessage = Request };

			return Task.FromResult(response);
		}
	}
}
