using Business.ViewModels.Admin.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin.Abstract
{
	public interface IAccountService
	{
		Task<bool> Login(AccountLoginVM model);
		Task<bool> Logout();
	}
}
