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
	public class NewsSliderRepository : Repository<NewsSlider> , INewsSliderRepository
	{
		private readonly AppDbContext _context;

		public NewsSliderRepository(AppDbContext context) : base(context) 
        {
			_context = context;
		}

		public async Task<NewsSlider> GetSlider()
		{
			return await _context.NewsSliders.FirstOrDefaultAsync(x => !x.IsDeleted);
		}
	}
}
