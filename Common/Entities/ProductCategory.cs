﻿using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class ProductCategory : BaseEntity
	{
        public string Title { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
