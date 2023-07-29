using Business.Services.Admin.Abstract;
using Business.ViewModels.Admin.ProductCategory;
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
	public class ProductCategoryService : IProductCategoryService
	{
		private readonly IProductCategoryRepository _categoryRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IProductRepository _productRepository;
		private readonly ModelStateDictionary _modelState;

		public ProductCategoryService(IProductCategoryRepository categoryRepository,
                                        IActionContextAccessor contextAccessor,
                                        IUnitOfWork unitOfWork,
                                        IProductRepository productRepository)
        {
			_categoryRepository = categoryRepository;
			_unitOfWork = unitOfWork;
			_productRepository = productRepository;
			_modelState = contextAccessor.ActionContext.ModelState;
		}

		public async Task<ProductCategoryListVM> GetAllAsync()
		{
			var model = new ProductCategoryListVM
			{
				ProductCategories = await _categoryRepository.GetAllAsync()
			};
			return model;
		}

		public async Task<bool> CreateAsync(ProductCategoryCreateVM model)
		{
			if (!_modelState.IsValid) return false;

			var category = await _categoryRepository.GetByNameAsync(model.Title);
			if (category is not null)
			{
				_modelState.AddModelError("Title", "Category under this name already exists in database");
				return false;
			}

			category = new ProductCategory
			{
				Title = model.Title,
				CreatedAt = DateTime.Now,
			};

			await _categoryRepository.CreateAsync(category);
			await _unitOfWork.CommitAsync();
			return true;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var category = await _categoryRepository.GetWithProducts(id);
			if (category is null)
			{
				_modelState.AddModelError("Title", "Vision doesn't exist");
				return false;
			}

			foreach(var product in category.Products)
			{
				_productRepository.SoftDelete(product);
			}

			_categoryRepository.SoftDelete(category);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<ProductCategoryUpdateVM> UpdateAsync(int id)
		{
			var category = await _categoryRepository.GetAsync(id);
			if (category is null)
			{
				_modelState.AddModelError("Title", "Vision doesn't exist");
				return null;
			}

			var model = new ProductCategoryUpdateVM
			{
				Title = category.Title,
			};
			return model;
		}

		public async Task<bool> UpdateAsync(ProductCategoryUpdateVM model,int id)
		{
			if (!_modelState.IsValid) return false;

			var category = await _categoryRepository.GetAsync(id);
			if (category is null)
			{
				_modelState.AddModelError("Title", "Vision doesn't exist");
				return false;
			}

			category.Title = model.Title;
			category.UpdatedAt = DateTime.Now;

			_categoryRepository.Update(category);
			await _unitOfWork.CommitAsync();
			return true;
		}
	}
}
