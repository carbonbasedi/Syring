using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class PlanFeature : BaseEntity
    {
        public string Feature { get; set; }
        public int PlanId { get; set; }
        public Plan Plan { get; set; }
    }
}
