using Business.Services.Admin.Abstract;
using Business.ViewModels.Admin.News;
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
	public class NewsService : INewsService
	{
		private readonly INewsRepository _newsRepository;
		private readonly IFileService _fileService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ModelStateDictionary _modelState;

		public NewsService(INewsRepository newsRepository,
							IFileService fileService,
							IUnitOfWork unitOfWork,
							IActionContextAccessor contextAccessor)
        {
			_modelState = contextAccessor.ActionContext.ModelState;
			_newsRepository = newsRepository;
			_fileService = fileService;
			_unitOfWork = unitOfWork;
		}
        public async Task<NewsListVM> GetAllAsync()
		{
			var model = new NewsListVM
			{
				News = await _newsRepository.GetAllAsync(),
			};
			return model;
		}
		public NewsCreateVM Create()
		{
			var model = new NewsCreateVM
			{
				Department = _newsRepository.GetAllDepartments().Select(x => new SelectListItem
				{
					Text = x.Title,
					Value = x.Id.ToString(),
				}).ToList(),
			};

			return model;
		}
		public async Task<bool> CreateAsync(NewsCreateVM model)
		{
			model.Department = _newsRepository.GetAllDepartments().Select(x => new SelectListItem
			{
				Text = x.Title,
				Value = x.Id.ToString(),
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

			var news = await _newsRepository.GetByName(model.Title);
			if (news is not null)
			{
				_modelState.AddModelError("Title", "News under this title already exists");
				return false;
			}

			var dept = await _newsRepository.GetDepartment(model.DeptId);
			if (dept is null)
			{
				_modelState.AddModelError("Department", "Department under this name doesn's exits");
				return false;
			}

			news = new News
			{
				Title = model.Title,
				Photo = _fileService.Upload(model.Image),
				PostDate = model.PostDate,
				DeptId = model.DeptId,	
				CreatedAt = DateTime.Now,
			};

			await _newsRepository.CreateAsync(news);
			await _unitOfWork.CommitAsync();

			return true;
		}
		public Task<NewsUpdateVM> UpdateAsync(int id)
		{
			throw new NotImplementedException();
		}
		public Task<bool> UpdateAsync(NewsUpdateVM model, int id)
		{
			throw new NotImplementedException();
		}
		public Task<bool> DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}
