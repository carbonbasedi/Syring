using Common.Entities;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
	public interface IFaqRepository : IRepository<Faq>
	{
		List<FaqCategory> GetAllFaqCategories();
		Task<Faq> GetByNameAsync(string name);
		Task<FaqCategory> GetCategory(int id);
		Task<Faq> GetFaqWithCategories(int id);
	}
}
