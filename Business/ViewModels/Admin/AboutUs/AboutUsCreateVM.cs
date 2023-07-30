using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.AboutUs
{
    public class AboutUsCreateVM
    {
        public AboutUsCreateVM()
        {
            Photos = new List<IFormFile>();
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

        [Required]
        [Display(Name = "Signature")]
        public IFormFile SignatureImg { get; set; }

        [Required]
        public List<IFormFile> Photos { get; set; }
    }
}
