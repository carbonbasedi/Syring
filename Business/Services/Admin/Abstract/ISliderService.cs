﻿using Business.ViewModels.Admin.Slider;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Abstract
{
    public interface ISliderService
    {
        Task<List<SliderListItemVM>> GelAllASync();
        Task<bool> CreateAsync(SliderCreateVM model);
        Task<bool> DeleteAsync(int id);
        Task<SliderUpdateVM> UpdateAsync(int id);
        Task<bool> UpdateAsync(SliderUpdateVM model, int id);
    }
}
