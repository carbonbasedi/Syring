using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class AboutUsPhotos : BaseEntity
	{
        public string Name { get; set; }
        public bool IsMain { get; set; }
        public int AboutUsId { get; set; }
        public AboutUs AboutUs { get; set; }
    }
}
