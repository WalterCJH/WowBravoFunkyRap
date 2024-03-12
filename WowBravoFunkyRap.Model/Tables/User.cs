using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WowBravoFunkyRap.Model.Tables
{
    [Table("User")]
    public class User : UserLog
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(50)]
        [Display(Name = "帳號")]
        public string? Account { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(30)]
        [Display(Name = "姓氏")]
        public string? LastName { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(30)]
        [Display(Name = "名字")]
        public string? FirstName { get; set; }

        [Column(TypeName = "VARCHAR")]
        [MaxLength(100)]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Column(TypeName = "VARCHAR")]
        [MaxLength(1000)]
        [Display(Name = "加密密碼")]
        public string? PasswordHash { get; set; }

        [Display(Name = "啟用")]
        public bool IsEnabled { get; set; }

        [Display(Name = "顯示順序")]
        public int DisplaySeq { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();



        [NotMapped]
        [MaxLength(100)]
        [Display(Name = "密碼")]
        public string? Password { get; set; }

        [NotMapped]
        [Display(Name = "姓名")]
        public string Name { get { return $"{LastName}{FirstName}"; } }

    }
}
