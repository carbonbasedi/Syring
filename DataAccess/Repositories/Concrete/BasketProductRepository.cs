using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
	public class BasketProductRepository : IBasketProductRepository
	{
		private readonly AppDbContext _context;

		public BasketProductRepository(AppDbContext context)
        {
			_context = context;
		}
        public async Task<BasketProduct> GetProduct(int id)
		{
			return await _context.BasketProducts.FirstOrDefaultAsync(b => b.Id == id);
		}
		public async Task<BasketProduct> GetUserBasket(string user, int id)
		{
			return await _context.BasketProducts.FirstOrDefaultAsync(b => b.ProductId == id && b.Basket.UserId == user);
		}
		public Task<BasketProduct> GetBasketProduct(int basketId, int id)
		{
			return _context.BasketProducts.FirstOrDefaultAsync(b => b.Id == id && b.BasketId == basketId);
		}
		public async Task CreateAsync(BasketProduct product)
		{
			await _context.BasketProducts.AddAsync(product);
		}
		public void Delete(BasketProduct product)
		{
			_context.BasketProducts.Remove(product);
		}
		public void Update(BasketProduct product)
		{
			_context.BasketProducts.Update(product);
		}
	}
}
