namespace WowBravoFunkyRap.Areas.Manage.ViewModels.Common
{
    public class ButtonModel
    {
        public Guid? Id { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public string? Text { get; set; }

        public ButtonModel(Guid? id, string? controllerPath, string? actionName, string? text)
        {
            Id = id;
            ControllerName = controllerPath;
            ActionName = actionName;
            Text = text;
        }
    }
}
