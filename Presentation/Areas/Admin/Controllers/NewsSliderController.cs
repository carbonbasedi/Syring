using Business.Services.Admin.Abstract;
using Business.ViewModels.Admin.NewsSlider;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsSliderController : Controller
    {
		private readonly INewsSliderService _sliderService;

		public NewsSliderController(INewsSliderService sliderService)
        {
			_sliderService = sliderService;
		}

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var slider = await _sliderService.GetAsync();
            return View(slider);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int id) 
        {
            var model = await _sliderService.UpdateAsync(id);
			if (model is null) return NotFound();

            return View(model);
		}

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(NewsSliderUpdateVM model, int id)
        {
            var isSucceeded = await _sliderService.UpdateAsync(model,id);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            return View(model);
        }
    }
}
