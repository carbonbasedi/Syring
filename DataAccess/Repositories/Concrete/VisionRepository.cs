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
	public class VisionRepository : Repository<Vision>, IVisionRepository
	{
		private readonly AppDbContext _context;

		public VisionRepository(AppDbContext context) : base(context) 
        {
			_context = context;
		}

		public async Task<List<Vision>> GetAllWithGoals()
		{
			return await _context.Visions.Include(v => v.VisionGoals.Where(vg => !vg.IsDeleted)).Where(v => !v.IsDeleted).ToListAsync();
		}

		public async Task<Vision> GetByNameAsync(string name)
		{
			return await _context.Visions.FirstOrDefaultAsync(v => v.Header.ToLower().Trim() == name.ToLower().Trim());
		}

		public async Task<Vision> GetWithGoalsAsync(int id)
		{
			return await _context.Visions.Include(v => v.VisionGoals.Where(vg => !vg.IsDeleted)).FirstOrDefaultAsync(v => v.Id == id);
		}
	}
}
