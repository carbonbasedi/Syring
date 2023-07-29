using Common.Entities;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
	public interface IProductRepository : IRepository<Product>
	{
		List<ProductCategory> GetAllCategories();
		Task<Product> GetByNameAsync(string name);
		Task<ProductCategory> GetCategoryAsync(int id);
		Task<Product> GetProductWithCategoriesAsync(int id);
		Task<List<Product>> GetAllWithCategoryAsync(int? id);
	}
}
