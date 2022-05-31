using System.Globalization;
using System.Windows.Controls;
using WPFBakeryShopAdminV2.Utilities;
using LangStr = WPFBakeryShopAdminV2.Utilities.Language;

namespace WPFBakeryShopAdminV2.LocalValidationRules
{
    public class EmailFormatValidationRule : ValidationRule
    {
        public string Message { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (StringUtils.IsValidEmail(value?.ToString()))
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, Message ?? LangStr.Get("Validation_EmailFormat"));
        }
    }
}
