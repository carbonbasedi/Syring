using Business.Services.Admin.Abstract;
using Business.ViewModels.Admin.PricingPage;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PricingPageController : Controller
    {
        private readonly IPricingPageService _pageService;

        public PricingPageController(IPricingPageService pageService)
        {
            _pageService = pageService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _pageService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(PricingCreateVM model)
        {
            var isSucceeded = await _pageService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var model = await _pageService.UpdateAsync(id);
            if(model is null) return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(PricingUpdateVM model, int id)
        {
            var isSucceeded = await _pageService.UpdateAsync(model, id);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var isSucceeded = await _pageService.DeleteAsync(id);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            return NotFound();
        }
    }
}
