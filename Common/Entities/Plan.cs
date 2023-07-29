using Common.Constants;
using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Plan : BaseEntity
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public PriceUnit PriceUnit { get; set; }
        public decimal Value { get; set; }
        public Period Period { get; set; }
        public ICollection<PlanFeature> Features { get; set; }
    }
}
