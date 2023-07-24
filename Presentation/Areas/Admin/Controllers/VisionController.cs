using Business.Services.Admin.Abstract;
using Business.Services.Admin.Concrete;
using Business.ViewModels.Admin.Vision;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Presentation.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class VisionController : Controller
	{
		private readonly IVisionService _visionService;

		public VisionController(IVisionService visionService)
		{
			_visionService = visionService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var model = await _visionService.GetAllAsync();
			return View(model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(VisionCreateVM model)
		{
			var isSucceeded = await _visionService.CreateAsync(model);
			if (isSucceeded) return RedirectToAction(nameof(Index));

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> UpdateAsync(int id)
		{
			var model = await _visionService.UpdateAsync(id);
			if (model is null) return NotFound();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateAsync(VisionUpdateVM model, int id)
		{
			var isSucceeded = await _visionService.UpdateAsync(model, id);
			if (isSucceeded) return RedirectToAction(nameof(Index));

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var isSucceeded = await _visionService.DeleteAsync(id);
			if (isSucceeded) return RedirectToAction(nameof(Index));

			return NotFound("Vision not found");
		}

		[HttpGet]
		public async Task<IActionResult> DetailsAsync(int id)
		{
			var model = await _visionService.DetailsAsync(id);
			if (model is null) return NotFound();

			return View(model);
		}
	}
}
