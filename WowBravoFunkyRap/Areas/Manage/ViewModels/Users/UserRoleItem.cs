namespace WowBravoFunkyRap.Areas.Manage.ViewModels.Users
{
    public class UserRoleItem
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }
        public int DisplaySeq { get; set; }
    }
}
