using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class News : BaseEntity
	{
        public string Photo { get; set; }
        public DateTime PostDate { get; set; }
        public string Title { get; set; }
        public int DeptId { get; set; }
        public Department Department { get; set; }
    }
}
