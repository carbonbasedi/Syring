using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.FaqCategory
{
	public class FaqCategoryListVM
	{
		public FaqCategoryListVM()
		{
			FaqCategories = new List<Common.Entities.FaqCategory>();
		}
        public List<Common.Entities.FaqCategory> FaqCategories { get; set; }
    }
}
