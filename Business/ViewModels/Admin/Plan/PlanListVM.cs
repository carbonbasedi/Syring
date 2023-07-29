using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Plan
{
	public class PlanListVM
	{
        public PlanListVM()
        {
            Plans = new List<Common.Entities.Plan>();
        }
        public List<Common.Entities.Plan> Plans { get; set; }
    }
}
