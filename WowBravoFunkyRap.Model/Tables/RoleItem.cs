using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WowBravoFunkyRap.Model.Tables
{
    [PrimaryKey(nameof(Id), nameof(RoleId))]
    [Table("RoleItem")]
    public class RoleItem
    {
        [Key]
        [Required]
        public string Id { get; set; }

        public Guid? RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }

        [NotMapped]
        [DisplayName("已勾選")]
        public bool Selected { get; set; }
    }
}
