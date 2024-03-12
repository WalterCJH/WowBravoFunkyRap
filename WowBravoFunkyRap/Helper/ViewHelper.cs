using WowBravoFunkyRap.Areas.Manage.ViewModels.Common;

namespace WowBravoFunkyRap.Helper
{
    public class ViewHelper
    {
        public static ButtonModel GetBtn(Guid? id = null, string? controllerPath = null, string? actionName = null, string? text = null)
        {
            return new ButtonModel(id, controllerPath, actionName, text);
        }
    }
}
