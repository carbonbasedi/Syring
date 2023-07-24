﻿using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class VisionGoal : BaseEntity
	{
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public int VisionId { get; set; }
        public Vision Vision { get; set; }
    }
}
