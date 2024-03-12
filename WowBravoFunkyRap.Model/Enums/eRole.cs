using System.ComponentModel.DataAnnotations;

namespace WowBravoFunkyRap.Model.Enums
{
    /// <summary>
    /// 自訂角色
    /// </summary>
    public enum eRole
    {
        [Display(Name = "縣市")]
        CityRead = 1,
        CityWrite = 2,
        PublicityImageRead = 3,
        PublicityImageWrite = 4,

        OrderCancelReasonRead = 31,
        OrderCancelReasonWrite = 32,
        ShipMethodRead = 33,
        ShipMethodWrite = 34,
        ProductCategoryRead = 35,
        ProductCategoryWrite = 36,
        ProductRead = 37,
        ProductWrite = 38,
        ProductPriceRead = 39,
        ProductPriceWrite = 40,

        ReportRead = 51,
        ReportWrite = 52,

        UserRead = 91,
        UserWrite = 92,
        RoleRead = 93,
        RoleWrite = 94,
    }
}
