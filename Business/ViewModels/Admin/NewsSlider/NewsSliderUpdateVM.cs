using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.NewsSlider
{
	public class NewsSliderUpdateVM
	{
		[Required]
        public string Title { get; set; }
    }
}
