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
	public class PlanRepository :Repository<Plan>, IPlanRepository
	{
		private readonly AppDbContext _context;

		public PlanRepository(AppDbContext context) : base(context)
        {
			_context = context;
		}

		public async Task<List<Plan>> GetAllWithFeatures()
		{
			return await _context.Plans.Include(p => p.Features).ToListAsync();
		}

		public async Task<Plan> GetByNameAsync(string name)
		{
			return await _context.Plans.FirstOrDefaultAsync(v => v.Title.ToLower().Trim() == name.ToLower().Trim() && !v.IsDeleted);
		}

		public async Task<Plan> GetWithFeatures(int id)
		{
			return await _context.Plans.Include(v => v.Features.Where(vg => !vg.IsDeleted)).FirstOrDefaultAsync(v => v.Id == id);
		}
	}
}
