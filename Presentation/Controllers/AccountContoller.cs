using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
	public class AccountContoller : Controller
	{
		public IActionResult Login()
		{
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}
	}
}
