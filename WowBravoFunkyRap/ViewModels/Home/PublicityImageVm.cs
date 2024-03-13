using System.ComponentModel.DataAnnotations;

namespace WowBravoFunkyRap.ViewModels.Home
{
    public class PublicityImageVm
    {
        [Display(Name = "圖片連結")]
        public string ImageLink { get; set; }

        public string ImageUrl { get; set; }
        public string ImageUrlSm { get; set; }
        public string ImageUrlXs { get; set; }
        public string ImageTitle { get; set; }
        public string ImageAlt { get; set; }
    }
}
