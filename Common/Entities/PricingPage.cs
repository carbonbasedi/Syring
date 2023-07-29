using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class PricingPage : BaseEntity
    {
        public string SubHeader { get; set; }
        public string Header { get; set; }
    }
}
