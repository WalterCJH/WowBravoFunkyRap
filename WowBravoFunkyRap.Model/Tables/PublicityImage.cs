using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WowBravoFunkyRap.Model.Enums;

namespace WowBravoFunkyRap.Model.Tables
{
    public class PublicityImage
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "圖片類型")]
        public PublicityImageType PublicityImageType { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        [Display(Name = "圖片路徑")]
        public string ImageUrl { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(50)]
        [Display(Name = "圖片標題")]
        public string ImageTitle { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        [Display(Name = "圖片說明")]
        public string ImageAlt { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        [Display(Name = "圖片名稱")]
        public string ImageName { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        [Display(Name = "圖片名稱_sm_576")]
        public string ImageNameSm { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        [Display(Name = "圖片名稱_xs_414")]
        public string ImageNameXs { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "DateTime")]
        [Display(Name = "開始時間")]
        public DateTime StartTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "DateTime")]
        [Display(Name = "結束時間")]
        public DateTime EndTime { get; set; }

        [Display(Name = "顯示順序")]
        public int DisplaySeq { get; set; }

        [Display(Name = "圖片")]
        public string ImageFullUrl { get { return $"{ImageUrl}{ImageName}"; } }
        public string ImageFullUrlSm { get { return $"{ImageUrl}{ImageNameSm}"; } }
        public string ImageFullUrlXs { get { return $"{ImageUrl}{ImageNameXs}"; } }

        [Display(Name = "有效時間")]
        public string EffectiveTime { get { return $"{StartTime:yyyy/MM/dd HH:mm}~{EndTime:yyyy/MM/dd HH:mm}"; } }
    }
}
