using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ergonomiks.Common.Utils
{
    public static class Image
    {
        public static string Upload(IFormFile file, Guid id)
        {
            var folderName = Path.Combine("Resources", "Images");

            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);                     

            if (file.Length > 0)
            {
                var mimeType = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"').Split('.').Last();

                if (mimeType == "png")
                {

                    var fileName = $"{id}.{mimeType}";

                    var fullPath = Path.Combine(pathToSave, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return fileName;
                }
                return null;
            }
            else
            {
                return null;
            }
        }
    }
}
