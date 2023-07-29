using Business.Services.Admin.Abstract;
using Business.ViewModels.Admin.PlanFeature;
using Common.Entities;
using DataAccess.Repositories.Abstract;
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
	public class PlanFeatureService : IPlanFeatureService
	{
		private readonly IPlanFeatureRepository _featureRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ModelStateDictionary _modelState;

		public PlanFeatureService(IPlanFeatureRepository featureRepository,
									IActionContextAccessor contextAccessor,
									IUnitOfWork unitOfWork)
		{
			_modelState = contextAccessor.ActionContext.ModelState;
			_featureRepository = featureRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<PlanFeatureListVM> GetAllAsync()
		{
			var model = new PlanFeatureListVM
			{
				PlanFeatures = await _featureRepository.GetAllAsync()
			};

			return model;
		}

		public PlanFeatureCreateVM Create()
		{
			var model = new PlanFeatureCreateVM
			{
				Plans = _featureRepository.GetAllPlans().Select(p => new SelectListItem
				{
					Text = p.Title,
					Value = p.Id.ToString(),
				}).ToList()
			};

			return model;
		}

		public async Task<bool> CreateAsync(PlanFeatureCreateVM model)
		{
			model.Plans = _featureRepository.GetAllPlans().Select(p => new SelectListItem
			{
				Text = p.Title,
				Value = p.Id.ToString(),
			}).ToList();

			if (!_modelState.IsValid) return false;

			var plan = await _featureRepository.GetPlan(model.PlanId);
			if (plan is null)
			{
				_modelState.AddModelError("Plan", "Vision under this name doesn's exits");
				return false;
			}

			var feature = new PlanFeature
			{
				Feature = model.Title,
				PlanId = model.PlanId,
				CreatedAt = DateTime.Now,
			};

			await _featureRepository.CreateAsync(feature);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var feature = await _featureRepository.GetAsync(id);
			if (feature is null)
			{
				_modelState.AddModelError("Title", "Vision Goal doesn't exist");
				return false;
			}

			_featureRepository.SoftDelete(feature);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<PlanFeatureUpdateVM> UpdateAsync(int id)
		{
			var feature = await _featureRepository.GetFeatureWithPlan(id);
			if (feature is null) return null;

			var model = new PlanFeatureUpdateVM
			{
				Title = feature.Feature,
				PlanId = feature.PlanId,
				Plans = _featureRepository.GetAllPlans().Select(p => new SelectListItem
				{
					Text = p.Title,
					Value = p.Id.ToString(),
				}).ToList()
			};

			return model;
		}

		public async Task<bool> UpdateAsync(PlanFeatureUpdateVM model,int id)
		{
			model.Plans = _featureRepository.GetAllPlans().Select(p => new SelectListItem
			{
				Text = p.Title,
				Value = p.Id.ToString(),
			}).ToList();

			if (!_modelState.IsValid) return false;

			var feature = await _featureRepository.GetAsync(id);
			if (feature is null)
			{
				_modelState.AddModelError("Title", "Vision Goal doesn't exist");
				return false;
			}

			var plan = await _featureRepository.GetPlan(model.PlanId);
			if (plan is null)
			{
				_modelState.AddModelError("Plan", "Vision under this name doesn's exits");
				return false;
			}

			feature.Feature = model.Title;
			feature.PlanId = model.PlanId;
			feature.Plan = plan;
			feature.UpdatedAt = DateTime.Now;

			_featureRepository.Update(feature);
			await _unitOfWork.CommitAsync();

			return true;
		}
	}
}
