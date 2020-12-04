using FinancePortal.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FinancePortal.Services
{
    public class ImageService : IImageService
    {

        public async Task<byte[]> AssignAvatarAsync(string avatar)
        {
            var defaultAvatarPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images", avatar);
            return await File.ReadAllBytesAsync(defaultAvatarPath);
        }

        public byte[] ConvertFileToByteArray(IFormFile file)
        {
            var ms = new MemoryStream();
            file.CopyTo(ms);
            var output = ms.ToArray();

            ms.Close();
            ms.Dispose();

            return output;
        }

        public string ConvertByteArrayToFile(byte[] fileData, string fileName)
        {
            var binary = Convert.ToBase64String(fileData);

            var ext = Path.GetExtension(fileName);

            string imageDataURL = $"data:image/{ext};base64,{binary}";

            return imageDataURL;
        }
    }
}
