using Business.Services.Admin.Abstract;
using Business.ViewModels.Admin.Department;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class DepartmentController : Controller
	{
		private readonly IDepartmentService _departmentService;

		public DepartmentController(IDepartmentService departmentService)
        {
			_departmentService = departmentService;
		}

		[HttpGet]
        public async Task<IActionResult> Index()
		{
			var model = await _departmentService.GetAllAsync();
			return View(model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(DepartmentCreateVM model)
		{
			var isSucceeded = await _departmentService.CreateAsync(model);
			if (!isSucceeded) return RedirectToAction(nameof(Index));

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> UpdateAsync(int id) 
		{
			var model = await _departmentService.UpdateAsync(id);
			if (model is null) return NotFound();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateAsync(DepartmentUpdateVM model, int id)
		{
			var isSucceeded = await _departmentService.UpdateAsync(model, id);
			if (!isSucceeded) return RedirectToAction(nameof(Index));

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var isSucceeded = await _departmentService.DeleteAsync(id);
			if (!isSucceeded) return RedirectToAction(nameof(Index));

			return NotFound("Department not found");
		}
	}
}
