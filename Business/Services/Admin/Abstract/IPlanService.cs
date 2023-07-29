using Business.ViewModels.Admin.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Abstract
{
	public interface IPlanService
	{
		Task<PlanListVM> GetAllAsync();
		Task<bool> CreateAsync(PlanCreateVM model);
		Task<bool> DeleteAsync(int id);
		Task<PlanUpdateVM> UpdateAsync(int id);
		Task<bool> UpdateAsync(PlanUpdateVM model,int id);
		Task<PlanDetailsVM> DetailsAsync(int id);

	}
}
