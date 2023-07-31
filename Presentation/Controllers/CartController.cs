using Business.Services.User.Abstract;
using Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
	public class CartController : Controller
	{
		private readonly ICartService _cartService;
		private readonly UserManager<IdentityUser> _userManager;

		public CartController(ICartService cartService,
								UserManager<IdentityUser> userManager)
        {
			_cartService = cartService;
			_userManager = userManager;
		}

		[HttpGet]
        public async Task<IActionResult> Index()
		{
			var user = await _userManager.GetUserAsync(User);
			if(user is null) return Unauthorized();

			var model = await _cartService.Index(user);

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> AddAsync(int id)
		{
			var user = await _userManager.GetUserAsync(User);
			if(user is null) return Unauthorized();

			var isSucceeded =  await _cartService.AddAsync(user, id);
			if (isSucceeded) return Ok("Product added to basket");

			return BadRequest("Something went wrong");
		}

		[HttpGet]
		public async Task<IActionResult> IncreaseCount(int id)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user is null) return Unauthorized();

			var isSucceeded = await _cartService.IncreaseAsync(user, id);
			if (isSucceeded) return Ok();

			return BadRequest("Something went wrong");
		}

		[HttpGet]
		public async Task<IActionResult> DecreaseCount(int id)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user is null) return Unauthorized();

			var isSucceeded = await _cartService.DecreaseAsync(user, id);
			if (isSucceeded) return Ok();

			return BadRequest("Something went wrong");
		}

		[HttpGet]
		public async Task<IActionResult> RemoveProduct(int id)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user is null) return Unauthorized();

			var isSucceeded = await _cartService.DeleteAsync(user, id);
			if (isSucceeded) return Ok();

			return BadRequest("Something went wrong");
		}

	}
}
