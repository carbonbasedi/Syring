using Business.ViewModels.Admin.AboutUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Abstract
{
	public interface IAboutUsService
	{
		Task<AboutUsIndexVM> GetAllAsync();
		Task<bool> CreateAsync(AboutUsCreateVM model);
		Task<bool> DeleteAsync(int id);
		Task<AboutUsUpdateVM> UpdateAsync(int id);
		Task<bool> UpdateAsync(AboutUsUpdateVM model,int id);
		Task<bool> SetMain(int id);
	}
}
