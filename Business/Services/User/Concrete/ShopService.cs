using Business.Services.User.Abstract;
using Business.ViewModels.User.Shop;
using Common.Entities;
using Common.Utilities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.User.Concrete
{
	public class ShopService : IShopService
	{
		private readonly IProductCategoryRepository _categoryRepository;
		private readonly IProductRepository _productRepository;

		public ShopService(IProductCategoryRepository categoryRepository,
							IProductRepository productRepository)
        {
			_categoryRepository = categoryRepository;
			_productRepository = productRepository;
		}

		public async Task<ShopIndexVM> Index(ShopIndexVM model)
		{
			model = new ShopIndexVM
			{
				Products = await FilterProductsAsync(model),
				Categories = await _categoryRepository.GetCategorySelectList()
			};
			return model;
		}

		private async Task<List<Product>> FilterProductsAsync(ShopIndexVM model)
		{
			var products = await _productRepository.FilterProductsByTitle(model.Title);
			products = await _productRepository.FilterProductsByCategory(products, model.CategoryIds);

			return await products.ToListAsync();
		}
	}
}
