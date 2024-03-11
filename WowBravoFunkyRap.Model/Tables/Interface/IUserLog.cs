namespace WowBravoFunkyRap.Model.Tables.Interface
{
    /// <summary>
    /// 更新紀錄欄位
    /// </summary>
    public interface IUserLog
    {
        public string? CreateUserId { get; set; }
        public DateTime? CreateTime { get; set; }
        public string? UpdateUserId { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
