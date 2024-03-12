using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WowBravoFunkyRap.Model.Tables
{
    [Table("Role")]
    public class Role : UserLog
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        [Display(Name = "名稱")]
        public string? Name { get; set; }

        [Display(Name = "顯示順序")]
        public int DisplaySeq { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public virtual ICollection<RoleItem> RoleItems { get; set; } = new List<RoleItem>();

    }
}
