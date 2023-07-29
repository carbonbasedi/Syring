using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.User.Pricing
{
	public class PricingPageIndexVM
	{
        public PricingPageIndexVM()
        {
            Plans = new List<Plan>();
            Pages = new List<PricingPage>();
        }
        public List<Plan> Plans { get; set; }
        public List<PricingPage> Pages { get; set; }
    }
}
