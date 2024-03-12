using System.ComponentModel.DataAnnotations;

namespace WowBravoFunkyRap.Attributes
{
    public class Excel : ValidationAttribute
    {
        /// <summary>
        /// 允許的副檔名
        /// <code>".xlsx|.xls"</code>
        /// </summary>
        public string? Allow;

        /// <summary>
        /// 是否需要上傳檔案
        /// </summary>
        public bool IsRequired { get; set; } = false;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null && IsRequired)
            {
                return new ValidationResult("請選擇檔案");
            }

            if (Allow == null)
            {
                Allow = ".xlsx|.xls";
            }

            var list = new List<IFormFile>();

            var type = value.GetType();
            if (type.FullName.Contains("System.Collections.Generic.List"))
            {
                list = value as List<IFormFile>;
            }
            else
            {
                list.Add((IFormFile)value);
            }

            foreach (var item in list)
            {
                string extension = Path.GetExtension(item.FileName).ToLower();
                if (Allow.Contains(extension) == false)
                {
                    if (ErrorMessage == null)
                    {
                        ErrorMessage = "請上傳.xlsx、.xls";
                    }
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
