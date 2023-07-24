using Business.Services.Admin.Abstract;
using Business.ViewModels.Admin.VisionGoal;
using Common.Entities;
using Common.Utilities.File;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Concrete
{
	public class VisionGoalService : IVisionGoalService
	{
		private readonly IVisionGoalRepositiory _visionGoalRepository;
		private readonly IFileService _fileService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ModelStateDictionary _modelState;

		public VisionGoalService(IVisionGoalRepositiory visionGoalRepository,
							  IActionContextAccessor contextAccessor,
							  IFileService fileService,
							  IUnitOfWork unitOfWork)
		{
			_modelState = contextAccessor.ActionContext.ModelState;
			_visionGoalRepository = visionGoalRepository;
			_fileService = fileService;
			_unitOfWork = unitOfWork;
		}

		public async Task<VisionGoalListVM> GetAllAsync()
		{
			var model = new VisionGoalListVM
			{
				VisionGoals = await _visionGoalRepository.GetAllAsync()
			};

			return model;
		}

		public VisionGoalCreateVM Create()
		{
			var model = new VisionGoalCreateVM
			{
				Vision = _visionGoalRepository.GetAllVisions().Select(v => new SelectListItem
				{
					Text = v.Header,
					Value = v.Id.ToString(),
				}).ToList(),
			};

			return model;
		}

		public async Task<bool> CreateAsync(VisionGoalCreateVM model)
		{
			model.Vision = _visionGoalRepository.GetAllVisions().Select(v => new SelectListItem
			{
				Text = v.Header,
				Value = v.Id.ToString(),
			}).ToList();

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

			var visionGoal = await _visionGoalRepository.GetByNameAsync(model.Title);
			if (visionGoal is not null)
			{
				_modelState.AddModelError("Title", "Category under this name already exists in database");
				return false;
			}

			var vision = _visionGoalRepository.GetVision(model.VisionId);
			if (vision is null)
			{
				_modelState.AddModelError("VisionId", "Vision under this name doesn's exits");
				return false;
			}

			visionGoal = new VisionGoal
			{
				Title = model.Title,
				Description = model.Description,
				Photo = _fileService.Upload(model.Photo),
				VisionId = model.VisionId,
				CreatedAt = DateTime.Now,
			};

			await _visionGoalRepository.CreateAsync(visionGoal);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var visionGoal = await _visionGoalRepository.GetAsync(id);
			if (visionGoal is null)
			{
				_modelState.AddModelError("Title", "Vision Goal doesn't exist");
				return false;
			}

			_visionGoalRepository.SoftDelete(visionGoal);
			_fileService.Delete(visionGoal.Photo);
			await _unitOfWork.CommitAsync();
			return true;
		}

		public async Task<VisionGoalUpdateVM> UpdateAsync(int id)
		{
			var visionGoal = await _visionGoalRepository.GetVisionGoalWithVisions(id);
			if (visionGoal is null) return null;

			var model = new VisionGoalUpdateVM
			{
				Title = visionGoal.Title,
				Description = visionGoal.Description,
				Photo = visionGoal.Photo,
				Vision = _visionGoalRepository.GetAllVisions().Select(v => new SelectListItem
				{
					Text = v.Header,
					Value = v.Id.ToString(),
				}).ToList(),
				VisionId = visionGoal.Id,
			};

			return model;
		}

		public async Task<bool> UpdateAsync(VisionGoalUpdateVM model, int id)
		{
			model.Vision = _visionGoalRepository.GetAllVisions().Select(v => new SelectListItem
			{
				Text = v.Header,
				Value = v.Id.ToString(),
			}).ToList();

			if (!_modelState.IsValid) return false;

			var visionGoal = await _visionGoalRepository.GetAsync(id);
			if (visionGoal is null)
			{
				_modelState.AddModelError("Title", "Vision Goal doesn't exist");
				return false;
			}

			var vision = _visionGoalRepository.GetVision(model.VisionId);
			if (vision is null)
			{
				_modelState.AddModelError("VisionId", "Vision under this name doesn's exits");
				return false;
			}

			visionGoal.Title = model.Title;
			visionGoal.Description = model.Description;
			visionGoal.UpdatedAt = DateTime.Now;
			visionGoal.VisionId = model.VisionId;
			visionGoal.Vision = await vision;
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

				if (!string.IsNullOrEmpty(visionGoal.Photo))
				{
					_fileService.Delete(visionGoal.Photo);
				}
				visionGoal.Photo = _fileService.Upload(model.NewPhoto);
			}

			_visionGoalRepository.Update(visionGoal);
			await _unitOfWork.CommitAsync();

			return true;
		}
	}
}
