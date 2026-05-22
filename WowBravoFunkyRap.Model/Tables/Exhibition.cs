using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WowBravoFunkyRap.Model.Enums;

namespace WowBravoFunkyRap.Model.Tables
{
    public class Exhibition
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        [Display(Name = "中文名稱")]
        public string? TitleTc { get; set; }

        [Column(TypeName = "VARCHAR")]
        [MaxLength(300)]
        [Display(Name = "英文名稱")]
        public string? TitleEn { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "DateTime")]
        [Display(Name = "開始時間")]
        public DateTime StartTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "DateTime")]
        [Display(Name = "結束時間")]
        public DateTime EndTime { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        [Display(Name = "Banner路徑")]
        public string BannerUrl { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(50)]
        [Display(Name = "Banner標題")]
        public string BannerTitle { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        [Display(Name = "Banner說明")]
        public string BannerAlt { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        [Display(Name = "Banner名稱")]
        public string BannerName { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        [Display(Name = "Banner名稱_sm_576")]
        public string BannerNameSm { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        [Display(Name = "Banner名稱_xs_414")]
        public string BannerNameXs { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        [Display(Name = "海報路徑")]
        public string PostersUrl { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(50)]
        [Display(Name = "海報標題")]
        public string PostersTitle { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        [Display(Name = "海報說明")]
        public string PostersAlt { get; set; }

        [Display(Name = "顯示順序")]
        public int DisplaySeq { get; set; }

        [Display(Name = "圖片")]
        public string BannerFullUrl { get { return $"{BannerUrl}{BannerName}"; } }
        public string BannerFullUrlSm { get { return $"{BannerUrl}{BannerNameSm}"; } }
        public string BannerFullUrlXs { get { return $"{BannerUrl}{BannerNameXs}"; } }

        [Display(Name = "有效時間")]
        public string EffectiveTime { get { return $"{StartTime:yyyy/MM/dd HH:mm}~{EndTime:yyyy/MM/dd HH:mm}"; } }
    }
}
