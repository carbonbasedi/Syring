using Business.Services.Admin.Abstract;
using Business.ViewModels.Admin.Product;
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
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;
		private readonly IFileService _fileService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ModelStateDictionary _modelState;

		public ProductService(IProductRepository productRepository,
                                IFileService fileService,
                                IUnitOfWork unitOfWork,
                                IActionContextAccessor contextAccessor)
        {
			_modelState = contextAccessor.ActionContext.ModelState;
			_productRepository = productRepository;
			_fileService = fileService;
			_unitOfWork = unitOfWork;
		}

		public async Task<ProductListVM> GetAllAsync()
		{
			var model = new ProductListVM
			{
				Products = await _productRepository.GetAllAsync()
			};
			return model;
		}

		public ProductCreateVM Create()
		{
			var model = new ProductCreateVM
			{
				Category = _productRepository.GetAllCategories().Select(p => new SelectListItem
				{
					Text = p.Title,
					Value = p.Id.ToString(),
				}).ToList(),
			};
			return model;
		}

		public async Task<bool> CreateAsync(ProductCreateVM model)
		{
			model.Category = _productRepository.GetAllCategories().Select(p => new SelectListItem
			{
				Text = p.Title,
				Value = p.Id.ToString(),
			}).ToList();

			if (!_modelState.IsValid) return false;

			if (!_fileService.IsImage(model.Image))
			{
				_modelState.AddModelError("Image", "Wrong file format");
				return false;
			}

			if (_fileService.IsBiggerThanSize(model.Image, 200))
			{
				_modelState.AddModelError("Image", "File size is over 200kb");
				return false;
			}

			var product = await _productRepository.GetByNameAsync(model.Name);
			if (product is not null)
			{
				_modelState.AddModelError("Name", "Product under this name already exists in database");
				return false;
			}

			var category = await _productRepository.GetCategoryAsync(model.CategoryId);
			if (category is null)
			{
				_modelState.AddModelError("Category", "Category under this name doesn's exits");
				return false;
			}

			product = new Product
			{
				Name = model.Name,
				Stock = model.Stock,
				Image = _fileService.Upload(model.Image),
				Price = model.Price,
				CategoryId = model.CategoryId,
				CreatedAt = DateTime.Now,
			};

			await _productRepository.CreateAsync(product);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<ProductUpdateVM> UpdateAsync(int id)
		{
			var product = await _productRepository.GetProductWithCategoriesAsync(id);
			if (product is null) return null;

			var model = new ProductUpdateVM
			{
				Name = product.Name,
				Stock = product.Stock,
				Image = product.Image,
				Price = product.Price,
				CategoryId = product.CategoryId,
				Category = _productRepository.GetAllCategories().Select(p => new SelectListItem
				{
					Text = p.Title,
					Value = p.Id.ToString(),
				}).ToList()
			};
			
			return model;
		}

		public async Task<bool> UpdateAsync(ProductUpdateVM model, int id)
		{
			model.Category = _productRepository.GetAllCategories().Select(p => new SelectListItem
			{
				Text = p.Title,
				Value = p.Id.ToString(),
			}).ToList();

			if (!_modelState.IsValid) return false;

			var product = await _productRepository.GetAsync(id);
			if (product is null)
			{
				_modelState.AddModelError("Name", "Product doesn't exist");
				return false;
			}

			var category = await _productRepository.GetCategoryAsync(model.CategoryId);
			if (category is null)
			{
				_modelState.AddModelError("Category", "Category under this name doesn's exits");
				return false;
			}

			product.Name = model.Name;
			product.Price = model.Price;
			product.Stock = model.Stock;
			product.CategoryId = model.CategoryId;
			product.UpdatedAt = DateTime.Now;

			if(model.NewImage is not null)
			{
				if (!_fileService.IsImage(model.NewImage))
				{
					_modelState.AddModelError("Image", "Wrong file format");
					return false;
				}

				if (_fileService.IsBiggerThanSize(model.NewImage, 200))
				{
					_modelState.AddModelError("Image", "File size is over 200kb");
					return false;
				}
				if (!string.IsNullOrEmpty(product.Image))
				{
					_fileService.Delete(product.Image);
				}
				product.Image = _fileService.Upload(model.NewImage);
			}

			_productRepository.Update(product);
			await _unitOfWork.CommitAsync();
			return true;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var product = await _productRepository.GetAsync(id);
			if (product is null)
			{
				_modelState.AddModelError("Name", "Vision Goal doesn't exist");
				return false;
			}

			_productRepository.SoftDelete(product);
			_fileService.Delete(product.Image);
			await _unitOfWork.CommitAsync();
			return true;
		}
	}
}
