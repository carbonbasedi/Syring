﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.VisionGoal
{
	public class VisionGoalCreateVM
	{
		[Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile Photo { get; set; }

        [Display(Name ="Vision")]
        public int VisionId { get; set; }
        public List<SelectListItem>? Vision { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
