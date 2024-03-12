using WowBravoFunkyRap.Model.Const;

namespace WowBravoFunkyRap.Helper
{
    public class AuthorizeHelper
    {
        /// <summary>
        /// Email是否為系統管理員
        /// </summary>
        /// <param name="email">使用者Email</param>
        /// <returns></returns>
        public static bool IsAdmin(string email)
        {
            return DataList.AdminEmailList.Contains(email.ToLower());
        }

        /// <summary>
        /// 檢查列表的使用者Email是否可以被登入使用者修改
        /// </summary>
        /// <param name="email">列表的使用者Email</param>
        /// <param name="isAdmin">登入的使用者是否為系統管理員</param>
        /// <returns></returns>
        public static bool CheckUserChange(string email, bool? isAdmin)
        {
            if (IsAdmin(email) == false) // user list email is admin
                return true;

            if (isAdmin == true) // 登入使用者為系統管理員
                return true;
            return false;
        }
    }
}
