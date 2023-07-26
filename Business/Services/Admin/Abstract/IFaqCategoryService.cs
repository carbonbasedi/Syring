using Business.ViewModels.Admin.FaqCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Abstract
{
	public interface IFaqCategoryService
	{
		Task<FaqCategoryListVM> GetAllAsync();
		Task<bool> CreateAsync(FaqCategoryCreateVM model);
		Task<bool> DeleteAsync(int id);
		Task<FaqCategoryUpdateVM> UpdateAsync(int id);
		Task<bool> UpdateAsync(FaqCategoryUpdateVM model, int id);
		Task<FaqCategoryDetailsVM> DetailsAsync(int id);
	}
}
