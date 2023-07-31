using Business.Services.User.Abstract;
using Business.ViewModels.User.Shop;
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
		public async Task<IActionResult> Index(ShopIndexVM model)
		{
			model = await _shopService.Index(model);
			return View(model);
		}
	}
}
