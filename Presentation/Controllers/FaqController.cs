﻿using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
	public class FaqController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
