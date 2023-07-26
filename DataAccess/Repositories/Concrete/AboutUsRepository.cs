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
	public class AboutUsRepository : Repository<AboutUs>, IAboutUsRepository
	{
		private readonly AppDbContext _context;

		public AboutUsRepository(AppDbContext context) : base(context) 
        {
			_context = context;
		}
		public async Task<AboutUs> GetByNameAsync(string name)
		{
			return await _context.AboutUs.FirstOrDefaultAsync(v => v.Header.ToLower().Trim() == name.ToLower().Trim());
		}

		public async Task<AboutUs> GetWithPhotos(int id)
		{
			return await _context.AboutUs.Include(v => v.Photos).FirstOrDefaultAsync(v => v.Id == id && !v.IsDeleted);
		}
	}
}
