using Business.Services.User.Abstract;
using Business.ViewModels.User.Shop;
using DataAccess.Repositories.Abstract;
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

		public ShopService(IProductCategoryRepository categoryRepository)
        {
			_categoryRepository = categoryRepository;
		}
        public async Task<ShopIndexVM> GetAllAsync()
		{
			var model = new ShopIndexVM
			{
				ProductCategories = await _categoryRepository.GetAllAsync()
			};
			return model;
		}
	}
}
