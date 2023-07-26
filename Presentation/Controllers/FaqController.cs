using Business.Services.User.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
	public class FaqController : Controller
	{
		private readonly IFaqPageService _faqPageService;

		public FaqController(IFaqPageService faqPageService)
        {
			_faqPageService = faqPageService;
		}
        public async Task<IActionResult> Index()
		{
			var model = await _faqPageService.GetAllAsync();
			return View(model);
		}
	}
}
