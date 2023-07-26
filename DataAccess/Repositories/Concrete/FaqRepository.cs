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
	public class FaqRepository : Repository<Faq>, IFaqRepository
	{
		private readonly AppDbContext _context;

		public FaqRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}
		public List<FaqCategory> GetAllFaqCategories()
		{
			return _context.FaqCategories.Where(fc => !fc.IsDeleted).ToList();
		}

		public async Task<Faq> GetByNameAsync(string name)
		{
			return await _context.Faqs.FirstOrDefaultAsync(f => f.Title.ToLower().Trim() == name.ToLower().Trim() && !f.IsDeleted);
		}
		public async Task<FaqCategory> GetCategory(int id)
		{
			return await _context.FaqCategories.FindAsync(id);
		}

		public async Task<Faq> GetFaqWithCategories(int id)
		{
			return await _context.Faqs.Include(f => f.Category).FirstOrDefaultAsync(f => !f.IsDeleted);
		}
	}
}
