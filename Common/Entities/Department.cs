using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class Department : BaseEntity
	{
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Image { get; set; }
        public bool IsFeatured { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<News> News { get; set; }
    }
}
