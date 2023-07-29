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
	public class PlanFeatureRepository : Repository<PlanFeature>, IPlanFeatureRepository
	{
		private readonly AppDbContext _context;

		public PlanFeatureRepository(AppDbContext context) : base(context)
        {
			_context = context;
		}

		public List<Plan> GetAllPlans()
		{
			return _context.Plans.Where(p => !p.IsDeleted).ToList();
		}

		public async Task<PlanFeature> GetFeatureWithPlan(int id)
		{
			return await _context.PlanFeatures.Include(pf => pf.Plan).Where(p => !p.IsDeleted).FirstOrDefaultAsync(p => !p.IsDeleted);
		}

		public async Task<Plan> GetPlan(int id)
		{
			return await _context.Plans.Where(p => !p.IsDeleted).FirstOrDefaultAsync(p => p.Id == id);
		}
	}
}
