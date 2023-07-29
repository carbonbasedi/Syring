using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.PlanFeature
{
	public class PlanFeatureListVM
	{
        public PlanFeatureListVM()
        {
            PlanFeatures = new List<Common.Entities.PlanFeature>();
        }
        public List<Common.Entities.PlanFeature> PlanFeatures { get; set; }
    }
}
