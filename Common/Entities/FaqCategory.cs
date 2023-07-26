using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class FaqCategory : BaseEntity
	{
        public string Title { get; set; }
        public string About { get; set; }
        public ICollection<Faq> Faqs { get; set; }
    }
}
