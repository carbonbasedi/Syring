using Common.Entities;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
	public interface IPlanFeatureRepository : IRepository<PlanFeature>
	{
		List<Plan> GetAllPlans();
		Task<Plan> GetPlan(int id);
		Task<PlanFeature> GetFeatureWithPlan(int id);
	}
}
