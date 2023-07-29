using Business.Services.Admin.Abstract;
using Business.ViewModels.Admin.Plan;
using Business.ViewModels.Admin.PlanFeature;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class PlanFeatureController : Controller
	{
		private readonly IPlanFeatureService _featureService;

		public PlanFeatureController(IPlanFeatureService featureService)
        {
			_featureService = featureService;
		}

		[HttpGet]
        public async Task<IActionResult> List()
		{
			var model = await _featureService.GetAllAsync();
			return View(model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			var model = _featureService.Create();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(PlanFeatureCreateVM model)
		{
			var isSucceeded = await _featureService.CreateAsync(model);
			if (isSucceeded) return RedirectToAction(nameof(List));

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> UpdateAsync(int id)
		{
			var model = await _featureService.UpdateAsync(id);
			if (model is null) return NotFound();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateAsync(PlanFeatureUpdateVM model,int id)
		{
			var isSucceeded = await _featureService.UpdateAsync(model,id);
			if (isSucceeded) return RedirectToAction(nameof(List));

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var isSucceeded = await _featureService.DeleteAsync(id);
			if (isSucceeded) return RedirectToAction(nameof(List));

			return NotFound();
		}
	}
}
