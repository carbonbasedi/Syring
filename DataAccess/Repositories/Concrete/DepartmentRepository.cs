using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
	public class DepartmentRepository : Repository<Department>, IDepartmentRepository
	{
		private readonly AppDbContext _context;

		public DepartmentRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<Department> GetByNameAsync(string name)
		{
			return await _context.Departments.FirstOrDefaultAsync(v => v.Title.ToLower().Trim() == name.ToLower().Trim() && !v.IsDeleted);
		}

		public async Task<Department> GetWithAllCollections(int id)
		{
			return await _context.Departments
											.Include(x => x.Doctors)
											.Include(x => x.News).Where(x => !x.IsDeleted)
											.FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}
