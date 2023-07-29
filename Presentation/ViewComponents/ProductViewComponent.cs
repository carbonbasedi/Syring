using Business.Services.Admin.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents
{
	public class ProductViewComponent : ViewComponent
	{
		private readonly IProductService _productService;

		public ProductViewComponent(IProductService productService)
        {
			_productService = productService;
		}

		public async Task<IViewComponentResult> InvokeAsync(int? id = null)
		{
			var model = await _productService.GetAllWithCategory(id);
			return View(model);
		}
    }
}
