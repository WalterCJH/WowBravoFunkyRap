using System.ComponentModel.DataAnnotations;

namespace WowBravoFunkyRap.Areas.Manage.ViewModels.Accounts
{
    public class Register : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password != PasswordConfirm)
            {
                yield return new ValidationResult("密碼驗證失敗", new string[] { nameof(Password), nameof(PasswordConfirm) });
            }
        }

        [Required]
        [Display(Name = "帳號")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "姓氏")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "名字")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "電子信箱")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "確認密碼")]
        public string PasswordConfirm { get; set; }
    }
}
