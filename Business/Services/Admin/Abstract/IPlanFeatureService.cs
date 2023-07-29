using Business.ViewModels.Admin.PlanFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Abstract
{
	public interface IPlanFeatureService
	{
		Task<PlanFeatureListVM> GetAllAsync();
		PlanFeatureCreateVM Create();
		Task<bool> CreateAsync(PlanFeatureCreateVM model);
		Task<bool> DeleteAsync(int id);
		Task<PlanFeatureUpdateVM> UpdateAsync(int id);
		Task<bool> UpdateAsync(PlanFeatureUpdateVM model,int id);
	}
}
