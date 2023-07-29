using Business.ViewModels.Admin.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Abstract
{
	public interface IProductCategoryService 
	{
		Task<ProductCategoryListVM> GetAllAsync();
		Task<bool> CreateAsync(ProductCategoryCreateVM model);
		Task<bool> DeleteAsync(int id);
		Task<ProductCategoryUpdateVM> UpdateAsync(int id);
		Task<bool> UpdateAsync(ProductCategoryUpdateVM model, int id);
	}
}
