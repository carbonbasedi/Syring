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
        public int MyProperty { get; set; }
        [Required]
		public string SubHeader { get; set; }

		[Required]
		public string Header { get; set; }

		[Required]
		public string About { get; set; }

		[Required]
		public string Description { get; set; }
		public IFormFile? NewSignatureImg { get; set; }
        public string? SignatureImg { get; set; }
		public List<IFormFile>? NewPhotos { get; set; }
        public List<AboutUsPhotos>? Photos { get; set; }
		public bool IsMainPhoto { get; set; }
	}
}
