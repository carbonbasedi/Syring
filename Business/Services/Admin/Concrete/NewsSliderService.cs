using Business.Services.Admin.Abstract;
using Business.ViewModels.Admin.NewsSlider;
using DataAccess.Repositories.Abstract;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Concrete
{
	public class NewsSliderService : INewsSliderService
	{
		private readonly INewsSliderRepository _sliderRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ModelStateDictionary _modelState;

		public NewsSliderService(INewsSliderRepository sliderRepository,
									IActionContextAccessor contextAccessor,
									IUnitOfWork unitOfWork)
        {
			_modelState = contextAccessor.ActionContext.ModelState;
			_sliderRepository = sliderRepository;
			_unitOfWork = unitOfWork;
		}
        public async Task<NewsSliderIndexVM> GetAsync()
		{
			var slider = await _sliderRepository.GetSlider();

			var model = new NewsSliderIndexVM
			{
				Id = slider.Id,
				Title = slider.Title,
				UpdatedAt = slider.UpdatedAt,
			};

			return model;
		}

		public async Task<NewsSliderUpdateVM> UpdateAsync(int id)
		{
			var slider = await _sliderRepository.GetAsync(id);
			if (slider == null) return null;

			var model = new NewsSliderUpdateVM
			{
				Title = slider.Title 
			};

			return model;
		}

		public async Task<bool> UpdateAsync(NewsSliderUpdateVM model, int id)
		{
			if (!_modelState.IsValid) return false;

			var slider = await _sliderRepository.GetAsync(id);
			if (slider is null)
			{
				_modelState.AddModelError("Title", "Slider doesn't exist");
				return false;
			}

			slider.Title = model.Title;
			slider.UpdatedAt = DateTime.Now;

			_sliderRepository.Update(slider);
			await _unitOfWork.CommitAsync();

			return true;
		}
	}
}

