﻿using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class Vision : BaseEntity
	{
        public string SubHeader { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public ICollection<VisionGoal> VisionGoals { get; set; }
    }
}
