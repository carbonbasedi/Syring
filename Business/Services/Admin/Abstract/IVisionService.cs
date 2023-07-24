using Business.ViewModels.Admin.Vision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Abstract
{
	public interface IVisionService
	{
		Task<VisionListVM> GetAllAsync();
		Task<bool> CreateAsync(VisionCreateVM model);
		Task<bool> DeleteAsync(int id);
		Task<VisionUpdateVM> UpdateAsync(int id);
		Task<bool> UpdateAsync(VisionUpdateVM model, int id);
		Task<VisionDetailsVM> DetailsAsync(int id);
	}
}
