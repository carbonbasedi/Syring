using Business.Services.Admin.Abstract;
using Business.Services.Admin.Concrete;
using Business.Services.User.Abstract;
using Business.Services.User.Concrete;
using Common.Entities;
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
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequiredUniqueChars = 0;
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.User.RequireUniqueEmail = true;
})
	.AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

#region Repositories
builder.Services.AddScoped<ISliderRepository, SliderRepository>();
builder.Services.AddScoped<IVisionRepository, VisionRepository>();
builder.Services.AddScoped<IVisionGoalRepositiory, VisionGoalRepository>();
#endregion

#region Services
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IVisionService, VisionService>();
builder.Services.AddScoped<IVisionGoalService, VisionGoalService>();
builder.Services.AddScoped<IHomeService, HomeService>();
#endregion

#region app
var app = builder.Build();
app.MapControllerRoute(
	name: "areas",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
app.MapDefaultControllerRoute();

app.UseStaticFiles();

app.Run();
#endregion