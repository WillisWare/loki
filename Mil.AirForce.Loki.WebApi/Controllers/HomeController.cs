using System.Web.Mvc;

namespace Mil.AirForce.Loki.WebApi.Controllers
{
	/// <summary>
	/// Home page controller.
	/// </summary>
	public class HomeController : Controller
	{
		/// <summary>
		/// Default action. Displays a view.
		/// </summary>
		/// <returns>An ActionResult instance containing the view data.</returns>
		public ActionResult Index()
		{
			ViewBag.Title = "Home Page";

			return View();
		}
	}
}
