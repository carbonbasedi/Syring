using Business.Services.Admin.Abstract;
using Business.ViewModels.Admin.AboutUs;
using Common.Entities;
using Common.Utilities.File;
using DataAccess.Repositories.Abstract;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Concrete
{
	public class AboutUsService : IAboutUsService
	{
		private readonly IAboutUsRepository _aboutUsRepository;
		private readonly IFileService _fileService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IAboutUsPhotosRepository _aboutUsPhotosRepository;
		private readonly ModelStateDictionary _modelState;

		public AboutUsService(IAboutUsRepository aboutUsRepository,
							  IActionContextAccessor contextAccessor,
							  IFileService fileService,
							  IUnitOfWork unitOfWork,
							  IAboutUsPhotosRepository aboutUsPhotosRepository)
		{
			_modelState = contextAccessor.ActionContext.ModelState;
			_aboutUsRepository = aboutUsRepository;
			_fileService = fileService;
			_unitOfWork = unitOfWork;
			_aboutUsPhotosRepository = aboutUsPhotosRepository;
		}

		public async Task<AboutUsIndexVM> GetAllAsync()
		{
			var model = new AboutUsIndexVM
			{
				AboutUs = await _aboutUsRepository.GetAboutUsWithPhotos()
			};

			return model;
		}

		public async Task<bool> CreateAsync(AboutUsCreateVM model)
		{
			if (!_modelState.IsValid) return false;

			var existingAboutUs = await _aboutUsRepository.GetByNameAsync(model.Header);
			if (existingAboutUs != null)
			{
				_modelState.AddModelError("Header", "About us component under this name already exists in the database");
				return false;
			}

			if (!_fileService.IsImage(model.SignatureImg) || _fileService.IsBiggerThanSize(model.SignatureImg, 200))
			{
				_modelState.AddModelError("SignatureImg", "Wrong file format or file size is over 200kb");
				return false;
			}

			foreach (var photo in model.Photos)
			{
				if (!_fileService.IsImage(photo) || _fileService.IsBiggerThanSize(photo, 200))
				{
					_modelState.AddModelError("Photos", "Wrong file format or file size is over 200kb");
					return false;
				}
			}

			var aboutUs = new AboutUs
			{
				SubHeader = model.SubHeader,
				Header = model.Header,
				About = model.About,
				Description = model.Description,
				SignatureImg = _fileService.Upload(model.SignatureImg),
				CreatedAt = DateTime.Now,
			};

			bool isFirst = true;
			foreach (var photo in model.Photos)
			{
				var photos = new AboutUsPhotos
				{
					Name = _fileService.Upload(photo),
					CreatedAt = DateTime.Now,
					AboutUs = aboutUs,
					IsMain = isFirst
				};
				isFirst = false;
				await _aboutUsPhotosRepository.CreateAsync(photos);
			}

			var dbAboutUs = await _aboutUsRepository.GetAboutUsWithPhotos();
			foreach (var dbAbout in dbAboutUs)
			{
				dbAbout.IsDeleted = true;
				_aboutUsRepository.Update(dbAbout);
                foreach (var photo in dbAbout.Photos)
                {
					_aboutUsPhotosRepository.SoftDelete(photo);
					_fileService.Delete(photo.Name);
					_aboutUsPhotosRepository.Update(photo);
                }
            }

			await _aboutUsRepository.CreateAsync(aboutUs);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var aboutUs = await _aboutUsRepository.GetWithPhotos(id);
			if (aboutUs == null)
			{
				_modelState.AddModelError("Header", "About us doesn't exist");
				return false;
			}

			foreach (var photo in aboutUs.Photos)
			{
				_fileService.Delete(photo.Name);
				_aboutUsPhotosRepository.Delete(photo);
			}
			_fileService.Delete(aboutUs.SignatureImg);
			_aboutUsRepository.SoftDelete(aboutUs);
			await _unitOfWork.CommitAsync();
			return true;
		}

		public async Task<AboutUsUpdateVM> UpdateAsync(int id)
		{
			var aboutUs = await _aboutUsRepository.GetWithPhotos(id);
			if (aboutUs is null)
			{
				_modelState.AddModelError("Header", "About us doesn't exist");
				return null;
			}

			var model = new AboutUsUpdateVM
			{
				SubHeader = aboutUs.SubHeader,
				Header = aboutUs.Header,
				About = aboutUs.About,
				Description = aboutUs.Description,
				SignatureImg = aboutUs.SignatureImg,
				Photos = aboutUs.Photos.ToList(),
			};

			return model;
		}

		public async Task<bool> UpdateAsync(AboutUsUpdateVM model, int id)
		{
			if (!_modelState.IsValid) return false;

			var aboutUs = await _aboutUsRepository.GetWithPhotos(id);
			if (aboutUs is null)
			{
				_modelState.AddModelError("Header", "Slider doesn't exist");
				return false;
			}

			aboutUs.SubHeader = model.SubHeader;
			aboutUs.Header = model.Header;
			aboutUs.Description = model.Description;
			aboutUs.About = model.About;
			if (model.NewSignatureImg is not null)
			{
				if (!_fileService.IsImage(model.NewSignatureImg))
				{
					_modelState.AddModelError("Photos", "Wrong file format");
					return false;
				}

				if (_fileService.IsBiggerThanSize(model.NewSignatureImg, 200))
				{
					_modelState.AddModelError("Photos", "File size is over 200kb");
					return false;
				}
				aboutUs.SignatureImg = _fileService.Upload(model.NewSignatureImg);
			}
			if (model.NewPhotos is not null)
			{
				foreach (var photo in model.NewPhotos)
				{
					if (!_fileService.IsImage(photo))
					{
						_modelState.AddModelError("Photos", "Wrong file format");
						return false;
					}

					if (_fileService.IsBiggerThanSize(photo, 200))
					{
						_modelState.AddModelError("Photos", "File size is over 200kb");
						return false;
					}
					var photos = new AboutUsPhotos
					{
						Name = _fileService.Upload(photo),
						CreatedAt = DateTime.Now,
						AboutUs = aboutUs
					};
					await _aboutUsPhotosRepository.CreateAsync(photos);

				}
			}

			_aboutUsRepository.Update(aboutUs);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<bool> SetMain(int id)
		{
			var photos = await _aboutUsPhotosRepository.GetPhotosWithCategory(id);
			if(photos is null) return false;

			var dbProductPhotos = _aboutUsPhotosRepository.GetAllPhotosWithCategory().Result.Where(p => p.Id != photos.Id && p.AboutUsId == photos.AboutUsId);

			if(!photos.IsMain)
			{
				foreach(var photo in dbProductPhotos)
				{
					photo.IsMain = false;
					_aboutUsPhotosRepository.Update(photo);
				}
				await _unitOfWork.CommitAsync();
			}

			photos.IsMain = !photos.IsMain;
			_aboutUsPhotosRepository.Update(photos);
			await _unitOfWork.CommitAsync();

			return true;
		}
	}
}
