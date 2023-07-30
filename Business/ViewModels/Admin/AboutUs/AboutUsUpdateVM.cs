using Common.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.AboutUs
{
	public class AboutUsUpdateVM
	{
        public AboutUsUpdateVM()
        {
			NewPhotos = new List<IFormFile>();
        }
        [Required]
		[Display(Name = "Sub Header")]
		public string SubHeader { get; set; }

		[Required]
		public string Header { get; set; }

		[Required]
		public string About { get; set; }

		[Required]
		public string Description { get; set; }

		[Display(Name = "Signature")]
		public IFormFile? NewSignatureImg { get; set; }
        public string? SignatureImg { get; set; }

		[Display(Name = "Photos")]
		public List<IFormFile>? NewPhotos { get; set; }
        public List<AboutUsPhotos>? Photos { get; set; }
		public bool IsMainPhoto { get; set; }
	}
}
