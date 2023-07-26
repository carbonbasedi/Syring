using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.FaqCategory
{
	public class FaqCategoryDetailsVM
	{
        public string Title { get; set; }
        public string About { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public List<Common.Entities.Faq> Faqs { get; set; }
    }
}
