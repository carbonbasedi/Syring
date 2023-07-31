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
	public class AboutUsPhotosRepository : Repository<AboutUsPhotos>, IAboutUsPhotosRepository
	{
		private readonly AppDbContext _context;

		public AboutUsPhotosRepository(AppDbContext context) : base(context)
        {
			_context = context;
		}

		public async Task<List<AboutUsPhotos>> GetAllPhotosWithCategory()
		{
			return await _context.AboutUsPhotos.Include(p => p.AboutUs).ToListAsync();
		}

		public async Task<AboutUsPhotos> GetPhotosWithCategory(int id)
		{
			return await _context.AboutUsPhotos.Include(p => p.AboutUs).FirstOrDefaultAsync(p => p.Id == id);
		}
	}
}
