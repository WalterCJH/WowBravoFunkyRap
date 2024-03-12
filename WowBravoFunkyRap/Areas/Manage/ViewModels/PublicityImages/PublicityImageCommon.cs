using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WowBravoFunkyRap.Model;
using WowBravoFunkyRap.Model.Const;
using WowBravoFunkyRap.Model.Enums;
using WowBravoFunkyRap.Model.Tables;

namespace WowBravoFunkyRap.Areas.Manage.ViewModels.PublicityImages
{
    public class PublicityImageCommon : UserLog, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var db = validationContext.GetService<WowBravoFunkyRapDbContext>();

            var data = db.PublicityImages.AsQueryable().AsNoTracking();
            if (Id != Guid.Empty)
            {
                data = data.Where(p => p.Id != Id);
            }

            if (PublicityImageType == Model.Enums.PublicityImageType.News)
            {
                var entity = data.FirstOrDefault(a => a.PublicityImageType == PublicityImageType && a.EndTime >= StartTime);
                if (entity != null)
                {
                    yield return new ValidationResult($"日期區間重複：{entity.StartTime:yyyy/MM/dd HH:mm}~{entity.EndTime:yyyy/MM/dd HH:mm}", new string[] { nameof(StartTime), nameof(EndTime) });
                }
            }
        }

        public Guid Id { get; set; }

        [Display(Name = "圖片類型")]
        public PublicityImageType? PublicityImageType { get; set; }

        [Display(Name = "上傳圖片")]
        public IFormFile? UploadImage { get; set; }

        [Display(Name = "圖片")]
        public string? ImageFullUrl { get; set; }

        [MaxLength(50)]
        [Display(Name = "圖片標題")]
        public string ImageTitle { get; set; }

        [MaxLength(100)]
        [Display(Name = "圖片說明")]
        public string? ImageAlt { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "開始時間")]
        public DateTime? StartTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "結束時間")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "顯示順序")]
        public int DisplaySeq { get; set; }

    }
}
