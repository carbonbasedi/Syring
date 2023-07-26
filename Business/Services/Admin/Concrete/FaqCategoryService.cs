using Business.Services.Admin.Abstract;
using Business.ViewModels.Admin.FaqCategory;
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

namespace Business.Services.Admin.Concrete
{
	public class FaqCategoryService : IFaqCategoryService
	{
		private readonly IFaqCategoryRepository _faqCategoryRepository;
		private readonly IFaqRepository _faqRepository;
		private readonly IActionContextAccessor _contextAccessor;
		private readonly IFileService _fileService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ModelStateDictionary _modelState;

		public FaqCategoryService(IFaqCategoryRepository faqCategoryRepository,
									IFaqRepository faqRepository,
									IActionContextAccessor contextAccessor,
									IFileService fileService,
									IUnitOfWork unitOfWork)
        {
			_modelState = contextAccessor.ActionContext.ModelState;
			_faqCategoryRepository = faqCategoryRepository;
			_faqRepository = faqRepository;
			_contextAccessor = contextAccessor;
			_fileService = fileService;
			_unitOfWork = unitOfWork;
		}
        public async Task<FaqCategoryListVM> GetAllAsync()
		{
			var model = new FaqCategoryListVM
			{
				FaqCategories = await _faqCategoryRepository.GetAllAsync()
			};

			return model;
		}

		public async Task<bool> CreateAsync(FaqCategoryCreateVM model)
		{
			if(!_modelState.IsValid) return false;

			var faqCategory = await _faqCategoryRepository.GetByNameAsync(model.Title);
			if (faqCategory is not null)
			{
				_modelState.AddModelError("Title", "Category under this name already exists in database");
				return false;
			}

			faqCategory = new FaqCategory
			{
				Title = model.Title,
				About = model.About,
				CreatedAt = DateTime.Now,
			};

			await _faqCategoryRepository.CreateAsync(faqCategory);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var faqCategory = await _faqCategoryRepository.GetWithFaqsAsync(id);
			if (faqCategory is null)
			{
				_modelState.AddModelError("Title", "Faq Category doesn't exist");
				return false;
			}

            foreach(var faq in faqCategory.Faqs)
            {
				_faqRepository.SoftDelete(faq);
            }

			_faqCategoryRepository.SoftDelete(faqCategory);
			await _unitOfWork.CommitAsync();
			return true;
        }

		public async Task<FaqCategoryUpdateVM> UpdateAsync(int id)
		{
			var faqCategory = await _faqCategoryRepository.GetAsync(id);
			if (faqCategory is null)
			{
				_modelState.AddModelError("Title", "Faq Category doesn't exist");
				return null;
			}

			var model = new FaqCategoryUpdateVM
			{
				Title = faqCategory.Title,
				About = faqCategory.About,
			};

			return model;
		}

		public async Task<bool> UpdateAsync(FaqCategoryUpdateVM model,int id)
		{
			if(!_modelState.IsValid) return false;

			var faqCategory = await _faqCategoryRepository.GetAsync(id);
			if (faqCategory is null)
			{
				_modelState.AddModelError("Title", "FaqCategory doesn't exist");
				return false;
			}

			faqCategory.Title = model.Title;
			faqCategory.About = model.About;
			faqCategory.UpdatedAt = DateTime.Now;

			_faqCategoryRepository.Update(faqCategory);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<FaqCategoryDetailsVM> DetailsAsync(int id)
		{
			var faqCategory = await _faqCategoryRepository.GetWithFaqsAsync(id);
			if (faqCategory is null)
			{
				_modelState.AddModelError("Title", "FaqCategory doesn't exist");
				return null;
			}

			var model = new FaqCategoryDetailsVM
			{
				Title = faqCategory.Title,
				About = faqCategory.About,
				Faqs = faqCategory.Faqs.ToList(),
				CreatedAt = faqCategory.CreatedAt,
				ModifiedAt = faqCategory.UpdatedAt
			};

			return model;
		}
	}
}
