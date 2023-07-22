using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities.File
{
	public class FileService : IFileService
	{
		private readonly IWebHostEnvironment _webHostEnvironment;

		public FileService(IWebHostEnvironment webHostEnvironment)
        {
			_webHostEnvironment = webHostEnvironment;
		}

        public void Delete(string photoName)
		{
			var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets/img", photoName);

			if (System.IO.File.Exists(filePath))
				System.IO.File.Delete(filePath);
		}

		public bool IsBiggerThanSize(IFormFile file, int size = 500)
		{
			if (file.Length / 1024 > size) return true;
			return false;
		}

		public bool IsImage(IFormFile file)
		{
			if (file.ContentType.Contains("image/")) return true;
			return false;
		}

		public string Upload(IFormFile file)
		{
			var fileName = Guid.NewGuid() + " " + file.FileName;
			var path = Path.Combine(_webHostEnvironment.WebRootPath, "assets/img", fileName);

			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
				file.CopyTo(fileStream);

			return fileName;
		}
	}
}
