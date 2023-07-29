using Business.ViewModels.Admin.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Abstract
{
	public interface IProductService
	{
		Task<ProductListVM> GetAllAsync();
		ProductCreateVM Create();
		Task<bool> CreateAsync(ProductCreateVM model);
		Task<bool> DeleteAsync(int id);
		Task<ProductUpdateVM> UpdateAsync(int id);
		Task<bool> UpdateAsync(ProductUpdateVM model, int id);
		Task<ProductListVM> GetAllWithCategory(int? id);
	}
}
