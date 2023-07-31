using Business.ViewModels.User.Cart;
using Common.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.User.Abstract
{
	public interface ICartService
	{
		Task<List<CartProductVM>> Index(IdentityUser user);
		Task<bool> AddAsync(IdentityUser user, int id);
		Task<bool> IncreaseAsync(IdentityUser user, int id);
		Task<bool> DecreaseAsync(IdentityUser user, int id);
		Task<bool> DeleteAsync(IdentityUser user, int id);
	}
}
