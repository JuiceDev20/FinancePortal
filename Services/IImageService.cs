using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancePortal.Models
{
    public interface IImageService
    {
        public byte[] ConvertFileToByteArray(IFormFile file);

        public string ConvertByteArrayToFile(byte[] fileData, string fileName);

        public Task<byte[]> AssignAvatarAsync(string avatar);

    }

}
