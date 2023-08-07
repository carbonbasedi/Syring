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
	public class NewsRepository : Repository<News>, INewsRepository
	{
		private readonly AppDbContext _context;

		public NewsRepository(AppDbContext context) : base(context) 
        {
			_context = context;
		}
		public List<Department> GetAllDepartments()
		{
			return _context.Departments.Where(x => !x.IsDeleted).ToList();
		}
		public async Task<News> GetByName(string name)
		{
			return await _context.News.FirstOrDefaultAsync(x => x.Title.ToLower().Trim() == name.ToLower().Trim() && !x.IsDeleted);
		}
		public async Task<Department> GetDepartment(int id)
		{
			return await _context.Departments.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
		}
	}
}
