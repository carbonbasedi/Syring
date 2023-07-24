using Business.Services.Admin.Abstract;
using Business.Services.Admin.Concrete;
using Business.ViewModels.Admin.VisionGoal;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class VisionGoalController : Controller
	{
		private readonly IVisionGoalService _visionGoalService;

		public VisionGoalController(IVisionGoalService visionGoalService)
        {
			_visionGoalService = visionGoalService;
		}

		[HttpGet]
        public async Task<IActionResult> Index()
		{
			var model = await _visionGoalService.GetAllAsync();
			return View(model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			var model = _visionGoalService.Create();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(VisionGoalCreateVM model)
		{
			var isSucceded = await _visionGoalService.CreateAsync(model);
			if (isSucceded) return RedirectToAction(nameof(Index));

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> UpdateAsync(int id)
		{
			var model = await _visionGoalService.UpdateAsync(id);
			if(model is null) return NotFound();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateAsync(VisionGoalUpdateVM model,int id)
		{
			var isSucceeded = await _visionGoalService.UpdateAsync(model,id);
			if (isSucceeded) return RedirectToAction(nameof(Index));

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var isSucceeded = await _visionGoalService.DeleteAsync(id);
			if (isSucceeded) return RedirectToAction(nameof(Index));

			return NotFound("Vision not found");
		}
	}
}
