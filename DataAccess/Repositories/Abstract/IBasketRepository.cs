using Common.Entities;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
	public interface IBasketRepository
	{
		Task<Basket> GetUserBasketWithProducts(string id);
		Task<Basket> GetBasket(string id);
		Task Create(Basket basket);
		void Update(Basket basket);
		void Delete(Basket basket);
	}
}
