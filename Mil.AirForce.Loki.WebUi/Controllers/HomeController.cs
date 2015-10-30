using System.Web.Mvc;

namespace Mil.AirForce.Loki.WebUi.Controllers
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
			return View();
		}

		/// <summary>
		/// GET method for displaying the "About" page.
		/// </summary>
		/// <returns>An ActionResult instance containing the view data.</returns>
		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		/// <summary>
		/// GET method for displaying the "Contact Us" page.
		/// </summary>
		/// <returns>An ActionResult instance containing the view data.</returns>
		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}