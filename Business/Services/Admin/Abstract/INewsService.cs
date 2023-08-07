using Business.ViewModels.Admin.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Abstract
{
	public interface INewsService
	{
		Task<NewsListVM> GetAllAsync();
		NewsCreateVM Create();
		Task<bool> CreateAsync(NewsCreateVM model);
		Task<NewsUpdateVM> UpdateAsync(int id);
		Task<bool> UpdateAsync(NewsUpdateVM model,int id);
		Task<bool> DeleteAsync(int id);
	}
}
