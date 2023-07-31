using Business.Services.Admin.Abstract;
using Business.Services.Admin.Concrete;
using Business.Services.User.Abstract;
using Business.Services.User.Concrete;
using Common;
using Common.Entities;
using Common.Utilities.EmailService;
using Common.Utilities.EmailService.EmailSender.Abstract;
using Common.Utilities.EmailService.EmailSender.Concrete;
using Common.Utilities.File;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;


#region builder

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("DataAccess")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequiredUniqueChars = 0;
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.User.RequireUniqueEmail = true;
	options.SignIn.RequireConfirmedEmail = true;
})
	.AddEntityFrameworkStores<AppDbContext>()
	.AddDefaultTokenProviders();

builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var configuration = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(configuration);
builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.ConfigureApplicationCookie(options =>
{
	options.Events.OnRedirectToLogin = options.Events.OnRedirectToAccessDenied = context =>
	{
		if (context.HttpContext.Request.Path.Value.StartsWith("/admin") || context.HttpContext.Request.Path.Value.StartsWith("/Admin"))
		{
			var redirectPath = new Uri(context.RedirectUri);
			context.Response.Redirect("/admin/account/login" + redirectPath.Query);
		}
		else
		{
			var redirectPath = new Uri(context.RedirectUri);
			context.Response.Redirect("/account/login" + redirectPath.Query);
		}
		return Task.CompletedTask;
	};
});
#endregion

#region Repositories
builder.Services.AddScoped<ISliderRepository, SliderRepository>();
builder.Services.AddScoped<IVisionRepository, VisionRepository>();
builder.Services.AddScoped<IVisionGoalRepositiory, VisionGoalRepository>();
builder.Services.AddScoped<IAboutUsRepository, AboutUsRepository>();
builder.Services.AddScoped<IAboutUsPhotosRepository, AboutUsPhotosRepository>();
builder.Services.AddScoped<IFaqCategoryRepository, FaqCategoryRepository>();
builder.Services.AddScoped<IFaqRepository, FaqRepository>();
builder.Services.AddScoped<IPricingPageRepository, PricingPageRepository>();
builder.Services.AddScoped<IPlanRepository, PlanRepository>();
builder.Services.AddScoped<IPlanFeatureRepository, PlanFeatureRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IBasketProductRepository, BasketProductRepository>();
#endregion

#region AdminServices
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IVisionService, VisionService>();
builder.Services.AddScoped<IVisionGoalService, VisionGoalService>();
builder.Services.AddScoped<IAboutUsService, AboutUsService>();
builder.Services.AddScoped<IFaqCategoryService, FaqCategoryService>();
builder.Services.AddScoped<IFaqService, FaqService>();
builder.Services.AddScoped<IPricingPageService, PricingPageService>();
builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<IPlanFeatureService, PlanFeatureService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAccountService, AccountService>();
#endregion

#region UserServices
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IFaqPageService, FaqPageService>();
builder.Services.AddScoped<IPricingService, PricingService>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<ICartService, CartService>();
#endregion

#region app
var app = builder.Build();
app.MapControllerRoute(
	name: "areas",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
app.MapDefaultControllerRoute();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
	var userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
	await DbInitializer.SeedAsync(roleManager, userManager);
}

app.Run();
#endregion