using Business.ViewModels.Admin.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Abstract
{
	public interface IDepartmentService
	{
		Task<DepartmentListVM> GetAllAsync();
		Task<bool> CreateAsync(DepartmentCreateVM model);
		Task<bool> DeleteAsync(int id);
		Task<DepartmentUpdateVM> UpdateAsync(int id);
		Task<bool> UpdateAsync(DepartmentUpdateVM model, int id);
	}
}
