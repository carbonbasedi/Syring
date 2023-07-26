using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Faq
{
	public class FaqListVM
	{
        public FaqListVM()
        {
            Faqs = new List<Common.Entities.Faq>();
        }
        public List<Common.Entities.Faq> Faqs { get; set; }
    }
}
