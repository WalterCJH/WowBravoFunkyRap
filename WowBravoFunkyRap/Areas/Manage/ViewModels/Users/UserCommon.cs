using WowBravoFunkyRap.Model.Tables;

namespace WowBravoFunkyRap.Areas.Manage.ViewModels.Users
{
    public class UserCommon : User
    {
        public List<UserRoleItem> UserRoleItems { get; set; } = new List<UserRoleItem>();
    }
}
