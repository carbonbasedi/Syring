using Business.Services.User.Abstract;
using Business.ViewModels.User.Faq;
using DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.User.Concrete
{
	public class FaqPageService : IFaqPageService
	{
		private readonly IFaqCategoryRepository _faqCategoryRepository;
		private readonly IFaqRepository _faqRepository;

		public FaqPageService(IFaqCategoryRepository faqCategoryRepository,
								IFaqRepository faqRepository)
        {
			_faqCategoryRepository = faqCategoryRepository;
			_faqRepository = faqRepository;
		}
        public async Task<FaqIndexVM> GetAllAsync()
		{
			var model = new FaqIndexVM
			{
				Categories = await _faqCategoryRepository.GetAllAsync(),
				Faqs = await _faqRepository.GetAllAsync()
			};

			return model;
		}
	}
}
