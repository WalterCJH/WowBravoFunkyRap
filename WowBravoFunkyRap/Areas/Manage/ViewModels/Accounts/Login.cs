using System.ComponentModel;

namespace WowBravoFunkyRap.Areas.Manage.ViewModels.Accounts
{
    public class Login
    {
        [DisplayName("帳號")]
        public string UserName { get; set; }

        [DisplayName("密碼")]
        public string Password { get; set; }

        [DisplayName("記住我")]
        public bool KeepMeLoggedIn { get; set; }
    }
}
