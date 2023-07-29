using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class PricingPageRepository : Repository<PricingPage>, IPricingPageRepository
    {
        public PricingPageRepository(AppDbContext context) : base(context)
        {

        }
    }
}
