using Business.Services.User.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
	public class HomeController : Controller
	{
		private readonly IHomeService _homeService;

		public HomeController(IHomeService homeService)
        {
			_homeService = homeService;
		}
        public async Task<IActionResult> Index()
		{
			var model = await _homeService.GelAllASync();
			return View(model);
		}
	}
}
