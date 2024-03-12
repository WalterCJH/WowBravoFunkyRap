using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WowBravoFunkyRap.Model.Tables
{
    [PrimaryKey(nameof(UserId), nameof(RoleId))]
    [Table("UserRole")]
    public class UserRole
    {
        public Guid? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        public Guid? RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }
    }
}
