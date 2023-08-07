using Common.Entities;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
	public interface IDepartmentRepository : IRepository<Department>
	{
		Task<Department> GetByNameAsync(string name);
		Task<Department> GetWithAllCollections(int id);
	}
}
