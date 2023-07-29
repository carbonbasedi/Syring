using Common.Constants;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Plan
{
	public class PlanUpdateVM
	{
		[Required]
		public string Title { get; set; }

		[Required]
		public string SubTitle { get; set; }

		[Required]
		public PriceUnit PriceUnit { get; set; }

		[Required]
		public decimal Value { get; set; }

		[Required]
		public Period Period { get; set; }

        public List<Common.Entities.PlanFeature>? PlanFeatures { get; set; }
    }
}
