using Business.Services.Admin.Abstract;
using Business.ViewModels.Admin.FaqCategory;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class FaqCategoryController : Controller
	{
		private readonly IFaqCategoryService _faqCategoryService;

		public FaqCategoryController(IFaqCategoryService faqCategoryService)
        {
			_faqCategoryService = faqCategoryService;
		}

		[HttpGet]
        public async Task<IActionResult> List()
		{
			var model = await _faqCategoryService.GetAllAsync();
			return View(model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(FaqCategoryCreateVM model)
		{
			var isSucceded = await _faqCategoryService.CreateAsync(model);
			if (isSucceded) return RedirectToAction(nameof(List));

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> UpdateAsync(int id)
		{
			var model = await _faqCategoryService.UpdateAsync(id);
			if (model is null) return NotFound();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateAsync(FaqCategoryUpdateVM model,int id)
		{
			var isSucceeded = await _faqCategoryService.UpdateAsync(model, id);
			if (isSucceeded) return RedirectToAction(nameof(List));

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var isSucceeded = await _faqCategoryService.DeleteAsync(id);
			if (isSucceeded) return RedirectToAction(nameof(List));

			return NotFound();
		}

		[HttpGet]
		public async Task<IActionResult> DetailsAsync(int id)
		{
			var model = await _faqCategoryService.DetailsAsync(id);
			if (model is null) return NotFound();

			return View(model);
		}
	}
}
