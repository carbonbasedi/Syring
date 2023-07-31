using Business.Services.User.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
	public class PricingController : Controller
	{
		private readonly IPricingService _pricingService;

		public PricingController(IPricingService pricingService)
        {
			_pricingService = pricingService;
		}
        public async Task<IActionResult> Index()
		{
			var model = await _pricingService.GetAllAsync();
			return View(model);
		}
	}
}
