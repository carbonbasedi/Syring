using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.User.Shop
{
	public class ShopIndexVM
	{
		public ShopIndexVM()
		{
			ProductCategories = new List<ProductCategory>();
		}
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
