using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WowBravoFunkyRap.Model.Tables.Interface;

namespace WowBravoFunkyRap.Model.Tables
{
    public class UserLog : IUserLog
    {
        [Column("CreateUserId", TypeName = "VARCHAR")]
        [MaxLength(50)]
        [Display(Name = "新增者")]
        public string? CreateUserId { get; set; }

        [Column("CreateTime")]
        [Display(Name = "新增時間")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? CreateTime { get; set; }

        [Column("UpdateUserId", TypeName = "VARCHAR")]
        [MaxLength(50)]
        [Display(Name = "修改者")]
        public string? UpdateUserId { get; set; }

        [Column("UpdateTime")]
        [Display(Name = "修改時間")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? UpdateTime { get; set; }
    }
}
