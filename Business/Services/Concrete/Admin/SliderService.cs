using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.Slider;
using Common.Entities;
using Common.Utilities.File;
using DataAccess.Repositories.Abstract;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Admin
{
	public class SliderService : ISliderService
	{
		private readonly ISliderRepository _sliderRepository;
		private readonly IFileService _fileService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ModelStateDictionary _modelState;

		public SliderService(ISliderRepository sliderRepository,
							  IActionContextAccessor contextAccessor,
							  IFileService fileService,
							  IUnitOfWork unitOfWork)
		{
			_modelState = contextAccessor.ActionContext.ModelState;
			_sliderRepository = sliderRepository;
			_fileService = fileService;
			_unitOfWork = unitOfWork;
		}
		public async Task<bool> CreateAsync(SliderCreateVM model)
		{
			if (!_modelState.IsValid) return false;

			if (!_fileService.IsImage(model.Photo))
			{
				_modelState.AddModelError("Photo", "Wrong file format");
				return false;
			}

			if (_fileService.IsBiggerThanSize(model.Photo, 200))
			{
				_modelState.AddModelError("Photo", "File size is over 200kb");
				return false;
			}

			var dbSlider = _sliderRepository.GetSlider();
			if(dbSlider is not null)
			{
				dbSlider.IsDeleted = true;
				_fileService.Delete(dbSlider.Photo);
			}

			var slider = new Slider
			{
				Title = model.Title,
				Subtitle = model.Subtitle,
				Photo = _fileService.Upload(model.Photo),
				CreatedAt = DateTime.Now,
			};

			await _sliderRepository.CreateAsync(slider);
			await _unitOfWork.CommitAsync();
			return true;
		}
		public async Task<bool> DeleteAsync(int id)
		{
			var slider = await _sliderRepository.GetAsync(id);
			if (slider is null)
			{
				_modelState.AddModelError("Title", "Slider doesn't exist");
				return false;
			}

			_sliderRepository.SoftDelete(slider);
			_fileService.Delete(slider.Photo);
			await _unitOfWork.CommitAsync();
			return true;
		}

		public async Task<List<SliderListItemVM>> GelAllASync()
		{
			var dbSliders = await _sliderRepository.GetAllAsync();

			var model = new List<SliderListItemVM>();
			foreach (var dbSlider in dbSliders)
			{
				model.Add(new SliderListItemVM
				{
					Id = dbSlider.Id,
					Title = dbSlider.Title,
					Photo = dbSlider.Photo,
					CreatedAt = dbSlider.CreatedAt,
					ModifiedAt = dbSlider.UpdatedAt,
				});
			}
			return model;
		}

		public async Task<SliderUpdateVM> UpdateAsync(int id)
		{
			var slider = await _sliderRepository.GetAsync(id);
			if (slider is null) return null;

			var model = new SliderUpdateVM
			{
				Title = slider.Title,
				Subtitle = slider.Subtitle,
				Photo = slider.Photo,
			};
			return model;
		}

		public async Task<bool> UpdateAsync(SliderUpdateVM model, int id)
		{
			if (!_modelState.IsValid) return false;

			var slider = await _sliderRepository.GetAsync(id);
			if (slider is null)
			{
				_modelState.AddModelError("Title", "Slider doesn't exist");
				return false;
			}

			slider.Title = model.Title;
			slider.Subtitle = model.Subtitle;
			slider.UpdatedAt = DateTime.Now;
			if (model.NewPhoto != null)
			{
				if (!_fileService.IsImage(model.NewPhoto))
				{
					_modelState.AddModelError("NewPhoto", "Wrong file format");
					return false;
				}

				if (_fileService.IsBiggerThanSize(model.NewPhoto, 200))
				{
					_modelState.AddModelError("NewPhoto", "File size is over 200kb");
					return false;
				}

				if (!string.IsNullOrEmpty(slider.Photo))
				{
					_fileService.Delete(slider.Photo);
				}
				slider.Photo = _fileService.Upload(model.NewPhoto);
			}
			_sliderRepository.Update(slider);
			await _unitOfWork.CommitAsync();
			return true;
		}
	}
}
