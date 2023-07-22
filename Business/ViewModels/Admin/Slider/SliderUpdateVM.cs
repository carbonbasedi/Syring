using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Slider
{
	public class SliderUpdateVM
	{
		[Required]
		[MaxLength(20)]
		public string Title { get; set; }

		[Required]
		[MaxLength(50)]
		public string Subtitle { get; set; }

		[Required]
		public IFormFile? NewPhoto { get; set; }
        public string? Photo { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
