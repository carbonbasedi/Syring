using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
	public class VisionGoalRepository : Repository<VisionGoal>, IVisionGoalRepositiory
	{
		private readonly AppDbContext _context;
		public VisionGoalRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public List<Vision> GetAllVisions()
		{
			return _context.Visions.Where(v => !v.IsDeleted).ToList();
		}

		public async Task<VisionGoal> GetByNameAsync(string name)
		{
			return await _context.VisionGoals.FirstOrDefaultAsync(x => x.Title.ToLower().Trim() == name.ToLower().Trim());
		}

		public async Task<Vision> GetVision(int id)
		{
			return await _context.Visions.FindAsync(id);
		}

		public async Task<VisionGoal> GetVisionGoalWithVisions(int id)
		{
			return await _context.VisionGoals.Include(v => v.Vision).FirstOrDefaultAsync(v => !v.IsDeleted);
		}
	}
}
