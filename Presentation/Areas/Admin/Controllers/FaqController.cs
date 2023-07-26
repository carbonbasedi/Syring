using Business.Services.Admin.Abstract;
using Business.ViewModels.Admin.Faq;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class FaqController : Controller
	{
		private readonly IFaqService _faqService;

		public FaqController(IFaqService faqService)
        {
			_faqService = faqService;
		}

		[HttpGet]
        public async Task<IActionResult> List()
		{
			var model = await _faqService.GetAllAsync();
			return View(model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			var model = _faqService.Create();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(FaqCreateVM model)
		{
			var isSucceeded = await _faqService.CreateAsync(model);
			if(isSucceeded) return RedirectToAction(nameof(List));

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> UpdateAsync(int id)
		{
			var model = await _faqService.UpdateAsync(id);
			if (model is null) return NotFound();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateAsync(FaqUpdateVM model,int id)
		{
			var isSucceeded = await _faqService.UpdateAsync(model, id);
			if (isSucceeded) return RedirectToAction(nameof(List));

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var isSucceeded = await _faqService.DeleteAsync(id);
			if (isSucceeded) return RedirectToAction(nameof(List));

			return NotFound("Faq not found");
		}
	}
}
