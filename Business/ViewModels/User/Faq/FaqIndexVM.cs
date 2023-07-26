using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.User.Faq
{
	public class FaqIndexVM
	{
        public FaqIndexVM()
        {
            Categories = new List<FaqCategory>();
            Faqs = new List<Common.Entities.Faq>(); 
        }
        public List<FaqCategory> Categories { get; set; }
        public List<Common.Entities.Faq> Faqs { get; set; }
    }
}
