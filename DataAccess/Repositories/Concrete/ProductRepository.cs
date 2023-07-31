using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		private readonly AppDbContext _context;

		public ProductRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IQueryable<Product>> FilterProductsByTitle(string? title)
		{
			return !string.IsNullOrEmpty(title) ?
							   _context.Products.Include(p => p.Category).Where(p => p.Name.Contains(title) && !p.IsDeleted) :
							   _context.Products.Include(p => p.Category).Where(p => !p.IsDeleted);
		}
		public async Task<IQueryable<Product>> FilterProductsByCategory(IQueryable<Product> products,List<int> ids)
		{
			return products.Where(p => ids.Count == 0 ? true : ids.Contains(p.CategoryId));
		}
		public List<ProductCategory> GetAllCategories()
		{
			return _context.ProductCategories.Where(p => !p.IsDeleted).ToList();
		}
		public async Task<Product> GetByNameAsync(string name)
		{
			return await _context.Products.Where(p => !p.IsDeleted).FirstOrDefaultAsync(p => p.Name == name);
		}
		public async Task<ProductCategory> GetCategoryAsync(int id)
		{
			return await _context.ProductCategories.Where(p => !p.IsDeleted).FirstOrDefaultAsync(p => p.Id == id);
		}
		public async Task<Product> GetProductWithCategoriesAsync(int id)
		{
			return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => !p.IsDeleted && p.Id == id);
		}


	}
}
