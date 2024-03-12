using System.ComponentModel.DataAnnotations;
using WowBravoFunkyRap.Models;
using System.ComponentModel.DataAnnotations.Schema;
using WowBravoFunkyRap.Areas.Manage.ViewModels.Common;
using WowBravoFunkyRap.Model.Tables;
using WowBravoFunkyRap.Model.Enums;

namespace WowBravoFunkyRap.Areas.Manage.ViewModels.PublicityImages
{
    public class PublicityImageFilter : Filter<PublicityImage>
    {
        //[RegularExpression(@"(StartTime|DisplaySeq)")]
        public override string SortBy { get; set; } = "DisplaySeq";

        [Display(Name = "圖片類型")]
        public PublicityImageType? PublicityImageType { get; set; }

        [Display(Name = "圖片標題")]
        public string? ImageTitle { get; set; }

    }
}
