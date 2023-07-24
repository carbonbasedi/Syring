using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Vision
{
	public class VisionListVM
	{
        public VisionListVM()
        {
            Visions = new List<Common.Entities.Vision>();
        }
        public List<Common.Entities.Vision> Visions { get; set; }
    }
}
