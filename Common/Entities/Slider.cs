﻿using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class Slider : BaseEntity
	{
        public string Title { get; set; }
		public string Subtitle { get; set; }
		public string Photo { get; set; }
    }
}
