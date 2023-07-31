using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.User.Cart
{
	public class CartProductVM
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int Count { get; set; }
		public decimal Price { get; set; }
		public int Stock { get; set; }
		public string Image { get; set; }
	}
}
