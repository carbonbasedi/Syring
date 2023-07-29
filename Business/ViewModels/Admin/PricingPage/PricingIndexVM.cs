using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.PricingPage
{
    public class PricingIndexVM
    {
        public PricingIndexVM()
        {
            PricingPages = new List<Common.Entities.PricingPage>();
        }
        public List<Common.Entities.PricingPage> PricingPages { get; set; }
    }
}
