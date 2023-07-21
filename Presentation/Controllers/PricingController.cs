using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
	public class PricingController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
