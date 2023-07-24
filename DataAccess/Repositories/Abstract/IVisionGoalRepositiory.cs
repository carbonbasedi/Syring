using Common.Entities;
using DataAccess.Repositories.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
	public interface IVisionGoalRepositiory : IRepository<VisionGoal>
	{
		Task<VisionGoal> GetByNameAsync(string name);
		Task<VisionGoal> GetVisionGoalWithVisions(int id);
		List<Vision> GetAllVisions();
		Task<Vision> GetVision(int id);
	}
}
