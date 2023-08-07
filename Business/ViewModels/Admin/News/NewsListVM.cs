using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.News
{
	public class NewsListVM
	{
        public NewsListVM()
        {
            News = new List<Common.Entities.News>();
        }
        public List<Common.Entities.News> News { get; set; }
    }
}
