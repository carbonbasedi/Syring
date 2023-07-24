using Business.Services.Admin.Abstract;
using Business.ViewModels.Admin.Slider;
using Business.ViewModels.Admin.Vision;
using Common.Entities;
using Common.Utilities.File;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
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
	public class VisionService : IVisionService
	{
		private readonly IVisionRepository _visionRepository;
		private readonly IFileService _fileService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IVisionGoalRepositiory _visionGoalRepositiory;
		private readonly ModelStateDictionary _modelState;

		public VisionService(IVisionRepository visionRepository,
							  IActionContextAccessor contextAccessor,
							  IFileService fileService,
							  IUnitOfWork unitOfWork,
							  IVisionGoalRepositiory visionGoalRepositiory)
		{
			_modelState = contextAccessor.ActionContext.ModelState;
			_visionRepository = visionRepository;
			_fileService = fileService;
			_unitOfWork = unitOfWork;
			_visionGoalRepositiory = visionGoalRepositiory;
		}

		public async Task<VisionListVM> GetAllAsync()
		{
			var model = new VisionListVM
			{
				Visions = await _visionRepository.GetAllAsync()
			};

			return model;
		}
		public async Task<bool> CreateAsync(VisionCreateVM model)
		{
			if (!_modelState.IsValid) return false;

			var vision = await _visionRepository.GetByNameAsync(model.Header);
			if (vision is not null)
			{
				_modelState.AddModelError("Name", "Category under this name already exists in database");
				return false;
			}

			vision = new Vision
			{
				SubHeader = model.SubHeader,
				Header = model.Header,
				Description = model.Description,
				CreatedAt = DateTime.Now,
			};

			var dbVisions = await _visionRepository.GetAllAsync();
			foreach (var dbVision in dbVisions)
			{
				dbVision.IsDeleted = true;
				_visionRepository.Update(dbVision);
			}

			await _visionRepository.CreateAsync(vision);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var vision = await _visionRepository.GetWithGoalsAsync(id);
			if (vision is null)
			{
				_modelState.AddModelError("Header", "Vision doesn't exist");
				return false;
			}

			foreach (var goal in vision.VisionGoals)
            {
				_visionGoalRepositiory.SoftDelete(goal);
            }

            _visionRepository.SoftDelete(vision);
			await _unitOfWork.CommitAsync();
			return true;
		}

		public async Task<VisionUpdateVM> UpdateAsync(int id)
		{
			var vision = await _visionRepository.GetAsync(id);
			if (vision is null)
			{
				_modelState.AddModelError("Header", "Vision doesn't exist");
				return null;
			}

			var model = new VisionUpdateVM
			{
				SubHeader = vision.SubHeader,
				Header = vision.Header,
				Description = vision.Description,
			};
			return model;
		}

		public async Task<bool> UpdateAsync(VisionUpdateVM model, int id)
		{
			if (!_modelState.IsValid) return false;

			var vision = await _visionRepository.GetAsync(id);
			if (vision is null)
			{
				_modelState.AddModelError("Header", "Slider doesn't exist");
				return false;
			}

			vision.SubHeader = model.SubHeader;
			vision.Header = model.Header;
			vision.Description = model.Description;
			vision.UpdatedAt = DateTime.Now;

			_visionRepository.Update(vision);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<VisionDetailsVM> DetailsAsync(int id)
		{
			var vision = await _visionRepository.GetWithGoalsAsync(id);
			if (vision is null)
			{
				_modelState.AddModelError(string.Empty, "Slider doesn't exist");
				return null;
			}

			var model = new VisionDetailsVM
			{
				SubHeader = vision.SubHeader,
				Header = vision.Header,
				Description = vision.Description,
				CreatedAt = vision.CreatedAt,
				ModifiedAt = vision.UpdatedAt,
				VisionGoals = vision.VisionGoals.ToList(),
			};

			return model;
		}
	}
}
