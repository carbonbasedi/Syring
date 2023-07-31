using Common.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.User.Shop
{
	public class ShopIndexVM
	{
		public ShopIndexVM()
		{
			Products = new List<Product>();
			CategoryIds = new List<int>();
		}
        public List<Product> Products { get; set; }
		public List<SelectListItem> Categories { get; set; }

		[Display(Name =("Category"))]
		public List<int> CategoryIds { get; set; }
        public string? Title { get; set; }
        public int CurrentPage { get; set; }
        public int Take { get; set; }
        public int TotalPage { get; set; }
    }
}
