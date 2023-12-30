using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace MVC_Project.PL.Helpers
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);
            
            string fileName= $"{Guid.NewGuid()} {file?.FileName}";
           
            string filePath= Path.Combine(folderPath, fileName);
           
            using  var fs = new FileStream(filePath,FileMode.Create);

            file?.CopyTo(fs);
            return fileName;

        }
        public static void DeleteFile(string fileName, string folderName)
        {
            var folderPath= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName,fileName); 
            if(File.Exists(folderPath))
                File.Delete(folderPath);    
        }
    }
}
