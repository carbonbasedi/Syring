using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Vision
{
	public class VisionDetailsVM
	{
        public string SubHeader { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public List<Common.Entities.VisionGoal> VisionGoals { get; set; }
    }
}
