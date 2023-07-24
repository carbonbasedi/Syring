using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.VisionGoal
{
	public class VisionGoalListVM
	{
        public VisionGoalListVM()
        {
            VisionGoals = new List<Common.Entities.VisionGoal>();
        }
        public List<Common.Entities.VisionGoal> VisionGoals { get; set; }
        public List<SelectListItem> Visions { get; set; }
    }
}
