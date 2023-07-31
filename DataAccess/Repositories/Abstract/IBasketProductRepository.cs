using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
	public interface IBasketProductRepository
	{
		Task<BasketProduct> GetProduct(int id);
		Task<BasketProduct> GetUserBasket(string user, int id);
		Task<BasketProduct> GetBasketProduct(int basketId, int id);
		Task CreateAsync(BasketProduct product);
		void Delete(BasketProduct product);
		void Update(BasketProduct product);
	}
}
