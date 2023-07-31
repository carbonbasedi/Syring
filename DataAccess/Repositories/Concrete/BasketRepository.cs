using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
	public class BasketRepository : IBasketRepository
	{
		private readonly AppDbContext _context;
		public BasketRepository(AppDbContext context)
        {
			_context = context;
		}
		public async Task<Basket> GetBasket(string id)
		{
			return await _context.Baskets.FirstOrDefaultAsync(b => b.UserId == id);
		}
		public async Task<Basket> GetUserBasketWithProducts(string id)
		{
			return await _context.Baskets
									.Include(b => b.BasketProducts)
									.ThenInclude(b => b.Product)
									.FirstOrDefaultAsync(b => b.UserId == id);
		}
		public async Task Create(Basket basket)
		{
			await _context.Baskets.AddAsync(basket);
		}
		public void Delete(Basket basket)
		{
			_context.Baskets.Remove(basket);
		}
		public void Update(Basket basket)
		{
			_context.Baskets.Update(basket);
		}
	}
}
