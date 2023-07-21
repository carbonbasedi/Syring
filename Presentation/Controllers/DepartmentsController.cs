using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
	public class DepartmentsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
