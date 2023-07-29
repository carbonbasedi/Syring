using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.ProductCategory
{
	public class ProductCategoryListVM
	{
		public ProductCategoryListVM()
		{
			ProductCategories = new List<Common.Entities.ProductCategory>();
		}
        public List<Common.Entities.ProductCategory> ProductCategories { get; set; }
    }
}
