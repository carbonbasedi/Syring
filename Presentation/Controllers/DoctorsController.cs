﻿using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
	public class DoctorsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Details()
		{
			return View();
		}
	}
}
