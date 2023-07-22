﻿using Common.Constants;
using Common.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class DbIntializer
	{
		public async static Task SeedAsync(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
		{
			await SeedRolesAsync(roleManager);
			await SeedUsersAsync(userManager);
		}
		private async static Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
		{
			foreach (var role in Enum.GetValues<UserRoles>())
			{
				if (!await roleManager.RoleExistsAsync(role.ToString()))
				{
					await roleManager.CreateAsync(new IdentityRole
					{
						Name = role.ToString(),
					});
				}
			}
		}
		private async static Task SeedUsersAsync(UserManager<User> userManager)
		{
			var user = await userManager.FindByNameAsync("Admin");
			if (user is null)
			{
				user = new User
				{
					UserName = "Admin",
					Email = "admin@gmail.com",
				};

				var result = await userManager.CreateAsync(user, "Admin1234!");

				if (!result.Succeeded)
				{
					foreach (var error in result.Errors)
						throw new Exception(error.Description);
				}

				await userManager.AddToRoleAsync(user, UserRoles.SuperAdmin.ToString());
			}
		}
	}
}
