using Business.Services.Admin.Abstract;
using Business.ViewModels.Admin.Department;
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
	public class DepartmentService : IDepartmentService
	{
		private readonly IDepartmentRepository _departmentRepository;
		private readonly IFileService _fileService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly INewsRepository _newsRepository;
		private readonly IDoctorRepository _doctorRepository;
		private readonly ModelStateDictionary _modelState;

		public DepartmentService(IDepartmentRepository departmentRepository,
									IFileService fileService,
									IUnitOfWork unitOfWork,
									IActionContextAccessor contextAccessor,
									INewsRepository newsRepository,
									IDoctorRepository doctorRepository)
        {
			_departmentRepository = departmentRepository;
			_fileService = fileService;
			_unitOfWork = unitOfWork;
			_newsRepository = newsRepository;
			_doctorRepository = doctorRepository;
			_modelState = contextAccessor.ActionContext.ModelState;
		}
		public async Task<DepartmentListVM> GetAllAsync()
		{
			var model = new DepartmentListVM
			{
				Departments = await _departmentRepository.GetAllAsync()
			};
			return model;
		}
		public async Task<bool> CreateAsync(DepartmentCreateVM model)
		{
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

			var dept = await _departmentRepository.GetByNameAsync(model.Title);
			if (dept is not null)
			{
				_modelState.AddModelError("Title", "Department under this name already exists in database");
				return false;
			}

			dept = new Department
			{
				Title = model.Title,
				Subtitle = model.Subtitle,
				Image = _fileService.Upload(model.Image),
				IsFeatured = model.IsFeatured,
				CreatedAt = DateTime.Now,
			};

			await _departmentRepository.CreateAsync(dept);
			await _unitOfWork.CommitAsync();

			return true;
		}
		public async Task<DepartmentUpdateVM> UpdateAsync(int id)
		{
			var dept = await _departmentRepository.GetAsync(id);
			if (dept is null) return null;

			var model = new DepartmentUpdateVM
			{
				Title = dept.Title,
				Image = dept.Image,
				Subtitle = dept.Subtitle,
				IsFeatured=dept.IsFeatured,
			};

			return model;
		}
		public async Task<bool> UpdateAsync(DepartmentUpdateVM model, int id)
		{
			if (!_modelState.IsValid) return false;

			var dept = await _departmentRepository.GetAsync(id);
			if (dept is null)
			{
				_modelState.AddModelError("Title", "Department doesn't exist");
				return false;
			}

			dept.Subtitle = model.Subtitle;
			dept.Title = model.Title;
			dept.IsFeatured = model.IsFeatured;
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

				_fileService.Delete(dept.Image);
				dept.Image = _fileService.Upload(model.NewImage);
			};
			dept.UpdatedAt = DateTime.Now;

			_departmentRepository.Update(dept);
			await _unitOfWork.CommitAsync();
			return true;
		}
		public async Task<bool> DeleteAsync(int id)
		{
			var dept = await _departmentRepository.GetWithAllCollections(id);
			if (dept is null)
			{
				_modelState.AddModelError("Title", "Vision Goal doesn't exist");
				return false;
			}

			_departmentRepository.SoftDelete(dept);
			foreach(var news in dept.News)
			{
				_newsRepository.SoftDelete(news);
			}
			foreach(var doc in dept.Doctors)
			{
				_doctorRepository.SoftDelete(doc);
			}

			await _unitOfWork.CommitAsync();
			return true;
		}
	}
}
