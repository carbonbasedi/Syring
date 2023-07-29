using Business.Services.User.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
	public class ShopController : Controller
	{
		private readonly IShopService _shopService;

		public ShopController(IShopService shopService)
		{
			_shopService = shopService;
		}
		public async Task<IActionResult> Index()
		{
			var model = await _shopService.GetAllAsync();
			return View(model);
		}

	}
}
