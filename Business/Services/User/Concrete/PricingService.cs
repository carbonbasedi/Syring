using Business.Services.User.Abstract;
using Business.ViewModels.User.Pricing;
using DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.User.Concrete
{
	public class PricingService : IPricingService
	{
		private readonly IPlanRepository _planRepository;
		private readonly IPricingPageRepository _pageRepository;

		public PricingService(IPlanRepository planRepository,
								IPricingPageRepository pageRepository)
        {
			_planRepository = planRepository;
			_pageRepository = pageRepository;
		}

		public async Task<PricingPageIndexVM> GetAllAsync()
		{
			var model = new PricingPageIndexVM
			{
				Pages = await _pageRepository.GetAllAsync(),
				Plans = await _planRepository.GetAllAsync()
			};

			return model;
		}
    }
}
