using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using WowBravoFunkyRap.Attributes;

namespace WowBravoFunkyRap.Areas.Manage.ViewModels.Common
{
    public class UploadImg
    {
        [Image]
        [Required(ErrorMessage = "請選擇檔案")]
        [Display(Name = "上傳圖片")]
        public IFormFile? FormFile { get; set; }
    }
}
