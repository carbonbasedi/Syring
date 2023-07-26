using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Base;
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
	}
}
