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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Plan>()
                .Property(p => p.Value)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Slider> Sliders { get; set; }
		public DbSet<Vision> Visions { get; set; }
		public DbSet<VisionGoal> VisionGoals { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<AboutUsPhotos> AboutUsPhotos { get; set; }
        public DbSet<FaqCategory> FaqCategories { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<PricingPage> PricingPages { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanFeature> PlanFeatures { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }
        public DbSet<NewsSlider> NewsSliders { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
    }
}
