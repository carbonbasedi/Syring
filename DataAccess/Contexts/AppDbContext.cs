using Common.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
	public class AppDbContext : IdentityDbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
			
		}

		public DbSet<Slider> Sliders { get; set; }
		public DbSet<Vision> Visions { get; set; }
		public DbSet<VisionGoal> VisionGoals { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<AboutUsPhotos> AboutUsPhotos { get; set; }
        public DbSet<FaqCategory> FaqCategories { get; set; }
        public DbSet<Faq> Faqs { get; set; }
    }
}
