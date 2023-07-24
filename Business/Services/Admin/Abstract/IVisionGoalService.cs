using Business.ViewModels.Admin.VisionGoal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Abstract
{
	public interface IVisionGoalService
	{
		Task<VisionGoalListVM> GetAllAsync();
		VisionGoalCreateVM Create();
		Task<bool> CreateAsync(VisionGoalCreateVM model);
		Task<bool> DeleteAsync(int id);
		Task<VisionGoalUpdateVM> UpdateAsync(int id);
		Task<bool> UpdateAsync(VisionGoalUpdateVM model, int id);
	}
}
