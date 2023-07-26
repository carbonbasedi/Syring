using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
	public class AboutUs : BaseEntity
	{
		public string SubHeader { get; set; }
		public string Header { get; set; }
		public string About { get; set; }
		public string Description { get; set; }
		public string SignatureImg { get; set; }

		[MaxLength(2)]
		public ICollection<AboutUsPhotos> Photos { get; set; }
	}
}
