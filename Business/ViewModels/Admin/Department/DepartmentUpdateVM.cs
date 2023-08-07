using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Department
{
	public class DepartmentUpdateVM
	{
		[Required]
		public string Title { get; set; }

		[Required]
		public string Subtitle { get; set; }

        public string? Image { get; set; }

        public IFormFile? NewImage { get; set; }

		public bool IsFeatured { get; set; }
	}
}
