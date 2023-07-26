using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.User.Home
{
	public class HomeIndexVM
	{
        public HomeIndexVM()
        {
            Sliders = new List<Slider>();
            Vision = new List<Vision>();
            AboutUs = new List<AboutUs>();
        }
        public List<Slider> Sliders { get; set; }
        public List<Vision> Vision { get; set; }
        public List<AboutUs> AboutUs { get; set; }
    }
}
