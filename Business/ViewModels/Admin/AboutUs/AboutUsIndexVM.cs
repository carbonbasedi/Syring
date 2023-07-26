using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.AboutUs
{
	public class AboutUsIndexVM
	{
        public AboutUsIndexVM()
        {
            AboutUs = new List<Common.Entities.AboutUs>();
        }
        public List<Common.Entities.AboutUs> AboutUs { get; set; }
    }
}
