using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.VisionGoal
{
	public class VisionGoalDetailsVM
	{
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
