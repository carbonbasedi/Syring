using Business.ViewModels.Admin.NewsSlider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Abstract
{
    public interface INewsSliderService
	{
		Task<NewsSliderIndexVM> GetAsync();
		Task<NewsSliderUpdateVM> UpdateAsync(int id);
		Task<bool> UpdateAsync(NewsSliderUpdateVM model, int id);
	}
}
