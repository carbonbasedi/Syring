using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.PlanFeature
{
	public class PlanFeatureCreateVM
	{
        [Required]
        public string Title { get; set; }
        public int PlanId { get; set; }
        public List<SelectListItem>? Plans { get; set; }
    }
}
