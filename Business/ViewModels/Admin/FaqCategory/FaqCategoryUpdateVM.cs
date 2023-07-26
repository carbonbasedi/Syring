using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.FaqCategory
{
	public class FaqCategoryUpdateVM
	{
		[Required]
		public string Title { get; set; }

		[Required]
		public string About { get; set; }
	}
}
