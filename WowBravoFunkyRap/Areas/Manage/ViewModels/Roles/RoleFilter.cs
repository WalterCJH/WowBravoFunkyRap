using System.ComponentModel.DataAnnotations;
using WowBravoFunkyRap.Areas.Manage.ViewModels.Common;
using WowBravoFunkyRap.Model.Tables;

namespace WowBravoFunkyRap.Areas.Manage.ViewModels.Roles
{
    public class RoleFilter : Filter<Role>
    {
        [RegularExpression(@"(Name|DisplaySeq)")]
        public override string SortBy { get; set; } = "DisplaySeq";
    }
}
