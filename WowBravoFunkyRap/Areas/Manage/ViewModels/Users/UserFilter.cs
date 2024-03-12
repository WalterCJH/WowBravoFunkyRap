using System.ComponentModel.DataAnnotations;
using WowBravoFunkyRap.Areas.Manage.ViewModels.Common;
using WowBravoFunkyRap.Model.Tables;

namespace WowBravoFunkyRap.Areas.Manage.ViewModels.Users
{
    public class UserFilter : Filter<User>
    {
        [RegularExpression(@"(UserName|Email|IsEnabled|DisplaySeq)")]
        public override string SortBy { get; set; } = "DisplaySeq";
    }
}
