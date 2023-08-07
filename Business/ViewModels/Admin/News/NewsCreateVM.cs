using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.News
{
	public class NewsCreateVM
	{
		[Required]
        public string Title { get; set; }

		[Required]
        public IFormFile Image { get; set; }

		[Required]
		public DateTime PostDate { get; set; }
		public int DeptId { get; set; }
        public List<SelectListItem>? Department { get; set; }
    }
}
