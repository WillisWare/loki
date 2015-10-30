using System.Web.Http;
using System.Web.Mvc;
using Mil.AirForce.Loki.WebApi.Areas.HelpPage.ModelDescriptions;

namespace Mil.AirForce.Loki.WebApi.Areas.HelpPage.Controllers
{
	/// <summary>
	/// The controller that will handle requests for the help page.
	/// </summary>
	public class HelpController : Controller
	{
		private const string ERROR_VIEW_NAME = "Error";

		/// <summary>
		/// Default constructor. Initializes an instance of this class.
		/// </summary>
		public HelpController()
			: this(GlobalConfiguration.Configuration)
		{
		}

		/// <summary>
		/// Overloaded. Initializes an instance of this class.
		/// </summary>
		/// <param name="config">An HttpConfiguration instance to be configured.</param>
		public HelpController(HttpConfiguration config)
		{
			Configuration = config;
		}

		/// <summary>
		/// Gets the configuration for this controller.
		/// </summary>
		public HttpConfiguration Configuration { get; private set; }

		/// <summary>
		/// Default action. Displays a view.
		/// </summary>
		/// <returns>An ActionResult instance containing the view data.</returns>
		public ActionResult Index()
		{
			ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
			return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
		}

		/// <summary>
		/// Displays a view.
		/// </summary>
		/// <param name="apiId">A String value containing the API method ID.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		public ActionResult Api(string apiId)
		{
			if (!string.IsNullOrEmpty(apiId))
			{
				var apiModel = Configuration.GetHelpPageApiModel(apiId);
				if (apiModel != null)
				{
					return View(apiModel);
				}
			}

			return View(ERROR_VIEW_NAME);
		}

		/// <summary>
		/// Displays a view.
		/// </summary>
		/// <param name="modelName">A String value containing the model name.</param>
		/// <returns>An ActionResult instance containing the view data.</returns>
		public ActionResult ResourceModel(string modelName)
		{
			if (!string.IsNullOrEmpty(modelName))
			{
				var modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
				ModelDescription modelDescription;
				if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
				{
					return View(modelDescription);
				}
			}

			return View(ERROR_VIEW_NAME);
		}
	}
}