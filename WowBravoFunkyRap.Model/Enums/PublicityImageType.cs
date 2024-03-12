using System.ComponentModel.DataAnnotations;

namespace WowBravoFunkyRap.Model.Enums
{
    public enum PublicityImageType
    {
        [Display(Name = "Banner")]
        Banner = 0,
        [Display(Name = "最新消息")]
        News = 1,
    }
}
