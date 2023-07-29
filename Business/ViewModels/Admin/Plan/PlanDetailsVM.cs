using Common.Constants;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Plan
{
	public class PlanDetailsVM
	{
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public PriceUnit PriceUnit { get; set; }
        public decimal Value { get; set; }
        public Period Period { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public List<Common.Entities.PlanFeature>? PlanFeatures { get; set; }
    }
}
