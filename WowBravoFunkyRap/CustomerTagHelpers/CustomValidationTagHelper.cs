using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WowBravoFunkyRap.CustomerTagHelpers
{
    [HtmlTargetElement("span", Attributes = ValidationForAttributeName)]
    public class CustomValidationTagHelper : TagHelper
    {
        private const string ValidationForAttributeName = "asp-validation-for";

        [HtmlAttributeName(ValidationForAttributeName)]
        public ModelExpression For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (For != null)
            {
                // 加上 class="invalid-feedback"
                var currentClass = output.Attributes["class"]?.Value ?? string.Empty;
                output.Attributes.SetAttribute("class", $"{currentClass} invalid-feedback");
            }
        }
    }

}
