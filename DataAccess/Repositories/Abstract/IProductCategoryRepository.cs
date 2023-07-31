using Common.Entities;
using DataAccess.Repositories.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
	public interface IProductCategoryRepository : IRepository<ProductCategory>
	{
		Task<ProductCategory> GetByNameAsync(string name);
		Task<ProductCategory> GetWithProducts(int id);
		Task<List<SelectListItem>> GetCategorySelectList();
	}
}
