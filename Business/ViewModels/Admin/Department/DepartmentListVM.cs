﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Department
{
	public class DepartmentListVM
	{
        public DepartmentListVM()
        {
            Departments = new List<Common.Entities.Department>();
        }
        public List<Common.Entities.Department> Departments { get; set; }

    }
}
