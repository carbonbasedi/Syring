using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Faq
{
	public class FaqCreateVM
	{
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        public List<SelectListItem>? Category { get; set; }
        public int CategoryId { get; set; }
    }
}
