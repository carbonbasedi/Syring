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
	public class FaqCategoryRepository : Repository<FaqCategory>, IFaqCategoryRepository
	{
		private readonly AppDbContext _context;

		public FaqCategoryRepository(AppDbContext context) : base(context)
        {
			_context = context;
		}

		public async Task<FaqCategory> GetByNameAsync(string name)
		{
			return await _context.FaqCategories.FirstOrDefaultAsync(v => v.Title.ToLower().Trim() == name.ToLower().Trim() && !v.IsDeleted);
		}

		public async Task<FaqCategory> GetWithFaqsAsync(int id)
		{
			return await _context.FaqCategories.Include(v => v.Faqs.Where(vg => !vg.IsDeleted)).FirstOrDefaultAsync(v => v.Id == id);
		}
	}
}
