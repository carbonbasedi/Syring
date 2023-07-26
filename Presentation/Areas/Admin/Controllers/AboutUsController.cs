using Business.Services.Admin.Abstract;
using Business.ViewModels.Admin.AboutUs;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AboutUsController : Controller
	{
		private readonly IAboutUsService _aboutUsService;

		public AboutUsController(IAboutUsService aboutUsService)
        {
			_aboutUsService = aboutUsService;
		}

		[HttpGet]
        public async Task<IActionResult> Index()
		{
			var model = await _aboutUsService.GelAllASync();
			return View(model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(AboutUsCreateVM model)
		{
			var isSucceeded = await _aboutUsService.CreateAsync(model);
			if(isSucceeded) return RedirectToAction(nameof(Index));

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var isSucceeded = await _aboutUsService.DeleteAsync(id);
			if (isSucceeded) return RedirectToAction(nameof(Index));

			return NotFound("About us not found");
		}

		[HttpGet]
		public async Task<IActionResult> UpdateAsync(int id)
		{
			var model = await _aboutUsService.UpdateAsync(id);
			if (model is null) return NotFound();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateAsync(AboutUsUpdateVM model,int id)
		{
			var isSucceeded = await _aboutUsService.UpdateAsync(model, id);
			if (isSucceeded) return RedirectToAction(nameof(Index));

			return View(model);
		}
	}
}
