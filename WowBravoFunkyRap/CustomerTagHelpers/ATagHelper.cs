using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WowBravoFunkyRap.CustomerTagHelpers
{
    /// <summary>
    /// <see cref="ITagHelper"/> implementation targeting &lt;input&gt; elements with <c>asp-for</c> and <c>asp-label</c> attribute.
    /// </summary>
    /// 
    [HtmlTargetElement("a", Attributes = "asp-controller")]
    public class ATagHelper : TagHelper
    {
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName("asp-controller")]
        public string AspController { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (AspController == string.Empty)
            {
                var controller = ViewContext.RouteData.Values["controller"]?.ToString();
                if (output.Attributes.TryGetAttribute("href", out var href))
                {
                    string value = href.Value.ToString().Replace("/Home/", $"/{controller}/");
                    output.Attributes.SetAttribute("href", value);
                }
            }
        }
    }
}
