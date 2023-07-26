using Business.Services.User.Abstract;
using Business.ViewModels.User.Home;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.User.Concrete
{
    public class HomeService : IHomeService
	{
		private readonly ISliderRepository _sliderRepository;
		private readonly IVisionRepository _visionRepository;
		private readonly IAboutUsRepository _aboutUsRepository;

		public HomeService(ISliderRepository sliderRepository,
							IVisionRepository visionRepository,
							IAboutUsRepository aboutUsRepository)
        {
			_sliderRepository = sliderRepository;
			_visionRepository = visionRepository;
			_aboutUsRepository = aboutUsRepository;
		}
        public async Task<HomeIndexVM> GelAllASync()
		{
			var model = new HomeIndexVM
			{
				Sliders = await _sliderRepository.GetAllAsync(),
				Vision = await _visionRepository.GetAllWithGoals()
			};

			return model;
		}
	}
}
