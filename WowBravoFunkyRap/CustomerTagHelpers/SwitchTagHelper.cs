using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WowBravoFunkyRap.CustomerTagHelpers
{
    /// <summary>
    /// <see cref="ITagHelper"/> implementation targeting &lt;input&gt; elements with <c>asp-for</c> and <c>asp-label</c> attribute.
    /// </summary>
    [HtmlTargetElement("switch", TagStructure = TagStructure.WithoutEndTag)]
    public class SwitchTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")]
        public ModelExpression aspFor { get; set; }

        [HtmlAttributeName("asp-label")]
        public bool ShowLabel { get; set; } = true;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "custom-control custom-switch");

            var propMetadata = aspFor.Metadata;
            string name = propMetadata.PropertyName;

            #region input checkbox

            var input = new TagBuilder("input");
            input.Attributes.Add("type", "checkbox");
            input.Attributes.Add("id", name);
            input.Attributes.Add("name", name);

            var disabled = context.AllAttributes.ContainsName("disabled") ? "disabled" : "";
            if (!string.IsNullOrEmpty(disabled))
            {
                input.Attributes.Add("disabled", null);
            }

            var _readonly = context.AllAttributes.ContainsName("readonly") ? "readonly" : "";
            if (!string.IsNullOrEmpty(_readonly))
            {
                input.Attributes.Add("readonly", null);
            }

            if (aspFor.Model is bool boolean)
            {
                if (boolean) input.Attributes.Add("checked", "checked");
            }

            input.Attributes.Add("value", "true");

            input.AddCssClass("custom-control-input");
            output.Content.AppendHtml(input);

            #endregion

            #region label
            var label = new TagBuilder("label");
            label.Attributes.Add("for", name);
            label.AddCssClass("custom-control-label");
            if (ShowLabel)
            {
                label.InnerHtml.Append(propMetadata.DisplayName);
            }
            output.Content.AppendHtml(label);
            #endregion

            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
