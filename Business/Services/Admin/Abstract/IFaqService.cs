
using Business.ViewModels.Admin.Faq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Abstract
{
	public interface IFaqService
	{
		Task<FaqListVM> GetAllAsync(); 
		FaqCreateVM Create();
		Task<bool> CreateAsync(FaqCreateVM model);
		Task<bool> DeleteAsync(int id);
		Task<FaqUpdateVM> UpdateAsync(int id);
		Task<bool> UpdateAsync(FaqUpdateVM model,int id);
	}
}
