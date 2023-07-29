using Business.Services.Admin.Abstract;
using Business.ViewModels.Admin.PricingPage;
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
    public class PricingPageService : IPricingPageService
    {
        private readonly IPricingPageRepository _pageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ModelStateDictionary _modelState;

        public PricingPageService(IPricingPageRepository pageRepository,
                                    IActionContextAccessor contextAccessor,
                                    IUnitOfWork unitOfWork)
        {
            _modelState = contextAccessor.ActionContext.ModelState;
            _pageRepository = pageRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<PricingIndexVM> GetAsync()
        {
            var model = new PricingIndexVM
            {
                PricingPages = await _pageRepository.GetAllAsync()
            };
            return model;
        }
        public async Task<bool> CreateAsync(PricingCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var page = new PricingPage
            {
                Header = model.Header,
                SubHeader = model.SubHeader,
                CreatedAt = DateTime.Now
            };

            var dbPages = await _pageRepository.GetAllAsync();
            foreach(var dbPpage in dbPages)
            {
                dbPpage.IsDeleted = true;
                _pageRepository.Update(dbPpage);
            }

            await _pageRepository.CreateAsync(page);
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var page = await _pageRepository.GetAsync(id);
            if (page is null)
            {
                _modelState.AddModelError("Header", "Vision doesn't exist");
                return false;
            }

            _pageRepository.SoftDelete(page);
            await _unitOfWork.CommitAsync();

            return true;
        }
        public async Task<PricingUpdateVM> UpdateAsync(int id)
        {
            var page = await _pageRepository.GetAsync(id);
            if (page is null)
            {
                _modelState.AddModelError("Header", "Vision doesn't exist");
                return null;
            }

            var model = new PricingUpdateVM
            {
                Header = page.Header,
                SubHeader = page.SubHeader,
            };

            return model;
        }
        public async Task<bool> UpdateAsync(PricingUpdateVM model, int id)
        {
            if (!_modelState.IsValid) return false;

            var page = await _pageRepository.GetAsync(id);
            if (page is null)
            {
                _modelState.AddModelError("Header", "Slider doesn't exist");
                return false;
            }

            page.Header = model.Header;
            page.SubHeader = model.SubHeader;
            page.UpdatedAt = DateTime.Now;

            _pageRepository.Update(page);
            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}
