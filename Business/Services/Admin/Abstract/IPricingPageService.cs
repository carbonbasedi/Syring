using Business.ViewModels.Admin.PricingPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Abstract
{
    public interface IPricingPageService
    {
        Task<PricingIndexVM> GetAsync();
        Task<bool> CreateAsync(PricingCreateVM model);
        Task<bool> DeleteAsync(int id);
        Task<PricingUpdateVM> UpdateAsync(int id);
        Task<bool> UpdateAsync(PricingUpdateVM model,int id);
    }
}
