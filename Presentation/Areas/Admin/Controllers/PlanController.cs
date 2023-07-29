using Business.Services.Admin.Abstract;
using Business.ViewModels.Admin.Plan;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class PlanController : Controller
	{
		private readonly IPlanService _planService;

		public PlanController(IPlanService planService)
        {
			_planService = planService;
		}

		[HttpGet]
        public async Task<IActionResult> List()
		{
			var model = await _planService.GetAllAsync();
			return View(model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(PlanCreateVM model)
		{
			var isSucceeded = await _planService.CreateAsync(model);
			if (isSucceeded) return RedirectToAction(nameof(List));

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> UpdateAsync(int id)
		{
			var model = await _planService.UpdateAsync(id);
			if (model == null) return NotFound();
			
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateAsync(PlanUpdateVM model, int id)
		{
			var isSucceeded = await _planService.UpdateAsync(model, id);
			if (isSucceeded) return RedirectToAction(nameof(List));

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var isSucceeded = await _planService.DeleteAsync(id);
			if (isSucceeded) return RedirectToAction(nameof(List));

			return NotFound();
		}

		[HttpGet]
		public async Task<IActionResult> DetailsAsync(int id)
		{
			var model = await _planService.DetailsAsync(id);
			if (model == null) return NotFound();

			return View(model);
		}
	}
}
