using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class Doctor : BaseEntity
	{
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public string Qualification { get; set; }
        public string Skills { get; set; }
        public string Schedule { get; set; }
        public int DeptId { get; set; }
        public Department Department { get; set; }
    }
}
