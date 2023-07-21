using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
	public class AccountController : Controller
	{
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
	}
}
