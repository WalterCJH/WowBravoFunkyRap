using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using WowBravoFunkyRap.Attributes;

namespace WowBravoFunkyRap.Areas.Manage.ViewModels.Common
{
    public class ImportExcel
    {
        [Excel]
        [Required(ErrorMessage = "請選擇檔案")]
        [Display(Name = "Excel檔案")]
        public IFormFile? FormFile { get; set; }
    }
}
