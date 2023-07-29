using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Product
{
	public class ProductCreateVM
	{
		[Required]
		public string Name { get; set; }

		[Required]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public IFormFile Image { get; set; }
        public List<SelectListItem>? Category { get; set; }
        public int CategoryId { get; set; }
    }
}
