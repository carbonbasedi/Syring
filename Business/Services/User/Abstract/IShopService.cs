using Business.ViewModels.User.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.User.Abstract
{
	public interface IShopService
	{
		Task<ShopIndexVM> GetAllAsync();
	}
}
