using Business.Services.Admin.Abstract;
using Business.ViewModels.Admin.Faq;
using Common.Entities;
using Common.Utilities.File;
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
	public class FaqService : IFaqService
	{
		private readonly IFaqRepository _faqRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ModelStateDictionary _modelState;

		public FaqService(IFaqRepository faqRepository,
							  IActionContextAccessor contextAccessor,
							  IUnitOfWork unitOfWork)
		{
			_modelState = contextAccessor.ActionContext.ModelState;
			_faqRepository = faqRepository;
			_unitOfWork = unitOfWork;
		}
		public async Task<FaqListVM> GetAllAsync()
		{
			var model = new FaqListVM
			{
				Faqs = await _faqRepository.GetAllAsync()
			};

			return model;
		}

		public FaqCreateVM Create()
		{
			var model = new FaqCreateVM
			{
				Category = _faqRepository.GetAllFaqCategories().Select(fc => new SelectListItem
				{
					Text = fc.Title,
					Value = fc.Id.ToString()
				}).ToList(),
			};

			return model;
		}

		public async Task<bool> CreateAsync(FaqCreateVM model)
		{
			model.Category = _faqRepository.GetAllFaqCategories().Select(fc => new SelectListItem
			{
				Text = fc.Title,
				Value = fc.Id.ToString()
			}).ToList();

			if (!_modelState.IsValid) return false;

			var faq = await _faqRepository.GetByNameAsync(model.Title);
			if (faq is not null)
			{
				_modelState.AddModelError("Title", "Faq under this name already exists in database");
				return false;
			}

			var faqCategory = _faqRepository.GetCategory(model.CategoryId);
			if (faqCategory is null)
			{
				_modelState.AddModelError("Category", "Vision under this name doesn's exits");
				return false;
			}

			faq = new Faq
			{
				Title = model.Title,
				Description = model.Description,
				CategoryId = model.CategoryId,
				CreatedAt = DateTime.Now,
			};

			await _faqRepository.CreateAsync(faq);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var faq = await _faqRepository.GetAsync(id);
			if (faq is null)
			{
				_modelState.AddModelError("Title", "Vision Goal doesn't exist");
				return false;
			}

			_faqRepository.SoftDelete(faq);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<FaqUpdateVM> UpdateAsync(int id)
		{
			var faq = await _faqRepository.GetFaqWithCategories(id);
			if (faq is null) return null;

			var model = new FaqUpdateVM
			{
				Title = faq.Title,
				Description = faq.Description,
				CategoryId = faq.CategoryId,
				Category = _faqRepository.GetAllFaqCategories().Select(fc => new SelectListItem
				{
					Text = fc.Title,
					Value = fc.Id.ToString()
				}).ToList()
			};
			return model;
		}

		public async Task<bool> UpdateAsync(FaqUpdateVM model,int id)
		{
			model.Category = _faqRepository.GetAllFaqCategories().Select(fc => new SelectListItem
			{
				Text = fc.Title,
				Value = fc.Id.ToString()
			}).ToList();

			if (!_modelState.IsValid) return false;

			var faq = await _faqRepository.GetAsync(id);
			if (faq is null)
			{
				_modelState.AddModelError("Title", "Vision Goal doesn't exist");
				return false;
			}

			var category = _faqRepository.GetCategory(model.CategoryId);
			if (category is null)
			{
				_modelState.AddModelError("Category", "Vision under this name doesn's exits");
				return false;
			}

			faq.Title = model.Title;
			faq.Description = model.Description;
			faq.UpdatedAt = DateTime.Now;
			faq.CategoryId = model.CategoryId;
			faq.Category = await category;

			_faqRepository.Update(faq);
			await _unitOfWork.CommitAsync();

			return true;
		}
	}
}
