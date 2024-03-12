using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Reflection;

namespace WowBravoFunkyRap.Extension
{
    public static class HtmlHelperExtension
    {
        /// <summary>
        /// 取得 Table 的 DisplayName
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IHtmlContent DisplayNameForTable(this IHtmlHelper htmlHelper, Type type)
        {
            var attributes = type.GetCustomAttributes<System.ComponentModel.DisplayNameAttribute>();
            return new StringHtmlContent(attributes.FirstOrDefault()?.DisplayName);
        }
        /// <summary>
        /// 取得 Enum 下拉選單
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="optionLabel"></param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetEnumSelectList<TEnum>(this IHtmlHelper htmlHelper, string? optionLabel)
            where TEnum : struct
        {
            var selectList = htmlHelper.GetEnumSelectList<TEnum>().ToList();
            if (string.IsNullOrWhiteSpace(optionLabel))
            {
                selectList.Insert(0, new SelectListItem($"", ""));
            }
            else
            {
                selectList.Insert(0, new SelectListItem($"{optionLabel}", ""));
            }
            return selectList;
        }
    }
}
