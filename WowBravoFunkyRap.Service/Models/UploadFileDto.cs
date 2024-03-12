using Microsoft.AspNetCore.Http;

namespace WowBravoFunkyRap.Service.Models
{
    public class UploadFileDto
    {
        public IFormFile File { get; set; }

        public List<string> DirectoryNameList { get; set; }

        public string NewFileName { get; set; }

        public int NewWidth { get; set; }

    }
}
