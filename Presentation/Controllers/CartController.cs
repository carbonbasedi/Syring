﻿using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
	public class CartController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}