using Business.Services.Admin.Abstract;
using Business.ViewModels.Admin.Plan;
using Common.Entities;
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
	public class PlanService : IPlanService
	{
		private readonly IPlanRepository _planRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPlanFeatureRepository _featureRepository;
		private readonly ModelStateDictionary _modelState;

		public PlanService(IPlanRepository planRepository,
							IUnitOfWork unitOfWork,
							IActionContextAccessor contextAccessor,
							IPlanFeatureRepository featureRepository)
		{
			_modelState = contextAccessor.ActionContext.ModelState;
			_planRepository = planRepository;
			_unitOfWork = unitOfWork;
			_featureRepository = featureRepository;
		}

		public async Task<PlanListVM> GetAllAsync()
		{
			var model = new PlanListVM
			{
				Plans = await _planRepository.GetAllAsync()
			};
			return model;
		}

		public async Task<bool> CreateAsync(PlanCreateVM model)
		{
			if (!_modelState.IsValid) return false;

			var plan = await _planRepository.GetByNameAsync(model.Title);
			if (plan is not null)
			{
				_modelState.AddModelError("Title", "Plan under this title already exists in database");
				return false;
			}

			plan = new Plan
			{
				Title = model.Title,
				SubTitle = model.SubTitle,
				PriceUnit = model.PriceUnit,
				Value = model.Value,
				Period = model.Period,
				CreatedAt = DateTime.Now,
			};

			await _planRepository.CreateAsync(plan);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var plan = await _planRepository.GetWithFeatures(id);
			if (plan is null)
			{
				_modelState.AddModelError("Title", "Plan doesn't exist");
				return false;
			}

			foreach (var feature in plan.Features)
			{
				_featureRepository.SoftDelete(feature);
			}

			_planRepository.SoftDelete(plan);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<PlanUpdateVM> UpdateAsync(int id)
		{
			var plan = await _planRepository.GetWithFeatures(id);
			if (plan is null)
			{
				_modelState.AddModelError("Title", "Vision doesn't exist");
				return null;
			}

			var model = new PlanUpdateVM
			{
				Title = plan.Title,
				SubTitle = plan.SubTitle,
				Value = plan.Value,
				Period = plan.Period,
				PriceUnit = plan.PriceUnit,
				PlanFeatures = plan.Features.ToList(),
			};

			return model;
		}

		public async Task<bool> UpdateAsync(PlanUpdateVM model, int id)
		{
			if (!_modelState.IsValid) return false;

			var plan = await _planRepository.GetWithFeatures(id);
			if (plan is null)
			{
				_modelState.AddModelError("Title", "Slider doesn't exist");
				return false;
			}

			plan.Title = model.Title;
			plan.SubTitle = model.SubTitle;
			plan.Value = model.Value;
			plan.Period = model.Period;
			plan.PriceUnit = model.PriceUnit;
			plan.UpdatedAt = DateTime.Now;

			_planRepository.Update(plan);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<PlanDetailsVM> DetailsAsync(int id)
		{
			var plan = await _planRepository.GetWithFeatures(id);
			if (plan is null)
			{
				_modelState.AddModelError("Title", "Slider doesn't exist");
				return null;
			}

			var model = new PlanDetailsVM
			{
				Title = plan.Title,
				SubTitle = plan.SubTitle,
				Value = plan.Value,
				Period = plan.Period,
				PriceUnit = plan.PriceUnit,
				CreatedAt = plan.CreatedAt,
				ModifiedAt = plan.UpdatedAt,
				PlanFeatures = plan.Features.ToList(),
			};

			return model;
		}
	}
}
