using Common.Entities;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
	public interface IVisionRepository : IRepository<Vision>
	{
		Task<Vision> GetByNameAsync(string name);
		Task<Vision> GetWithGoalsAsync(int id);
		Task<List<Vision>> GetAllWithGoals();
	}
}
