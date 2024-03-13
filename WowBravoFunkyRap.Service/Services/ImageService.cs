using Microsoft.EntityFrameworkCore.Storage.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using WowBravoFunkyRap.Model.Const;
using WowBravoFunkyRap.Service.Models;

namespace WowBravoFunkyRap.Service.Services
{
    public class ImageService
    {
        public async Task<string?> SaveDownloadFileAsync(UploadFileDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
            {
                return null;
            }

            string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string directoryPath = string.Join('\\', dto.DirectoryNameList);
            string downloadPath = $"\\Download\\{directoryPath}";

            string filePath = $"{wwwrootPath}{downloadPath}";
            Directory.CreateDirectory(filePath);

            string newFileName = $"{dto.NewFileName}.webp";
            string absoluteFilePath = Path.Combine(filePath, newFileName);

            using (var image = Image.Load(dto.File.OpenReadStream()))
            {
                int newHeight = (int)(image.Height / (float)image.Width * dto.NewWidth); // 計算新的高度以保持圖片寬高比
                image.Mutate(x => x.Resize(dto.NewWidth, newHeight)); // 調整圖片大小
                await image.SaveAsWebpAsync(absoluteFilePath);
            }

            string relativeFilePath = Path.Combine(downloadPath, newFileName).Replace("\\", "/");
            return relativeFilePath;
        }

        public async Task<SaveImageResult> SaveMultipleImageFileAsync(SaveImageDto dto)
        {
            var result = new SaveImageResult();
            if (dto.File == null || dto.File.Length == 0)
            {
                return result;
            }

            string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string directoryPath = $"/{FileStr.Image}/{string.Join('/', dto.DirectoryNameList)}";

            string filePath = $"{wwwrootPath}{directoryPath}";
            Directory.CreateDirectory(filePath);

            string newFileName = $"{dto.NewFileName}.webp";
            string absoluteFilePath = Path.Combine(filePath, newFileName);
            using (var image = Image.Load(dto.File.OpenReadStream()))
            {
                int newHeight = (int)(image.Height / (float)image.Width * dto.NewWidth); // 計算新的高度以保持圖片寬高比
                image.Mutate(x => x.Resize(dto.NewWidth, newHeight)); // 調整圖片大小
                await image.SaveAsWebpAsync(absoluteFilePath);
            }

            string newFileNameSm = $"{dto.NewFileName}_sm.webp";
            string absoluteFilePathSm = Path.Combine(filePath, newFileNameSm);
            using (var image = Image.Load(dto.File.OpenReadStream()))
            {
                int newHeight = (int)(image.Height / (float)image.Width * dto.NewWidthSm);
                image.Mutate(x => x.Resize(dto.NewWidthSm, newHeight));
                await image.SaveAsWebpAsync(absoluteFilePathSm);
            }

            string newFileNameXs = $"{dto.NewFileName}_xs.webp";
            string absoluteFilePathXs = Path.Combine(filePath, newFileNameXs);
            using (var image = Image.Load(dto.File.OpenReadStream()))
            {
                int newHeight = (int)(image.Height / (float)image.Width * dto.NewWidthXs);
                image.Mutate(x => x.Resize(dto.NewWidthXs, newHeight));
                await image.SaveAsWebpAsync(absoluteFilePathXs);
            }

            result.ImageUrl = $"{directoryPath.Replace("\\", "/")}/";
            result.ImageName = newFileName;
            result.ImageNameSm = newFileNameSm;
            result.ImageNameXs = newFileNameXs;
            return result;
        }

        public async Task DeleteDownloadFileAsync(DeleteFileDto dto)
        {
            string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string absoluteFilePath = Path.Combine(wwwrootPath, dto.RelativeFileName.TrimStart('/').Replace("/", "\\"));
            if (File.Exists(absoluteFilePath))
            {
                File.Delete(absoluteFilePath);
            }
        }
    }
}
