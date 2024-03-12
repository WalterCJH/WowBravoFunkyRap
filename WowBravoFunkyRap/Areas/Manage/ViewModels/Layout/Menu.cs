namespace WowBravoFunkyRap.Areas.Manage.ViewModels.Layout
{
    public class Menu
    {
        public string Title { get; set; }
        public string IconClass { get; set; }
        public int Sequence { get; set; }
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}
