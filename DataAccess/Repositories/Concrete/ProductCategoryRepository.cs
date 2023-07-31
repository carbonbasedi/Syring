using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
	public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
	{
		private readonly AppDbContext _context;

		public ProductCategoryRepository(AppDbContext context) : base(context) 
		{
			_context = context;
		}

		public async Task<ProductCategory> GetByNameAsync(string name)
		{
			return await _context.ProductCategories.Where(p => !p.IsDeleted).FirstOrDefaultAsync(p => p.Title.ToLower().Trim() == name.ToLower().Trim());
		}

		public async Task<List<SelectListItem>> GetCategorySelectList()
		{
			return await _context.ProductCategories.Where(_ => !_.IsDeleted).Select(p => new SelectListItem
			{
				Text = p.Title,
				Value = p.Id.ToString(),
			}).ToListAsync();
		}

		public async Task<ProductCategory> GetWithProducts(int id)
		{
			return await _context.ProductCategories.Where(p => !p.IsDeleted).Include(p => p.Products).FirstOrDefaultAsync(p => p.Id == id);
		}
	}
}
