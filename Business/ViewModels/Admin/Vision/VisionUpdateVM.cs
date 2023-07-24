﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Vision
{
	public class VisionUpdateVM
	{
		[Required]
		public string SubHeader { get; set; }

		[Required]
		public string Header { get; set; }

		[Required]
		public string Description { get; set; }
	}
}
